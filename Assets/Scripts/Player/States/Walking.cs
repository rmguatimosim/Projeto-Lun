using UnityEngine;

namespace Player.States
{
    public class Walking : State
    {
        private readonly PlayerController controller;
        private float footstepCooldown;
        public Walking(PlayerController controller) : base("Walking")
        {
            this.controller = controller;

        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (controller.isInCutscene) return;

            //Switch to Jump
            if (controller.hasJumpInput)
            {
                controller.stateMachine.ChangeState(controller.jumpState);
                return;
            }
            //Switch to Idle
            if (controller.movementVector.IsZero())
            {
                controller.stateMachine.ChangeState(controller.idleState);
                return;
            }
            //switch to Change Form
            if (controller.hasChangeFormInput)
            {
                controller.stateMachine.ChangeState(controller.changeFormState);
                return;
            }
            //switch to Assign
            if (controller.hasCopyInput || controller.hasStoreInput)
            {
                controller.stateMachine.ChangeState(controller.assignState);
                return;
            }

            // Footstep!
            float velocity = controller.thisRigidBody.linearVelocity.magnitude;
            float velocityRate = velocity / controller.maxSpeed;
            footstepCooldown -= Time.deltaTime * velocityRate;
            if (footstepCooldown <= 0f)
            {
                footstepCooldown = controller.footstepInterval;
                var audioClip = controller.footstepSounds;
                var volumeScale = Random.Range(0.6f, 0.8f);
                controller.footstepAudioSource.PlayOneShot(audioClip, volumeScale);
            }

        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (controller.isInCutscene) return;
            
            //Create Vector
            Vector3 walkVector = new(controller.movementVector.x, 0, controller.movementVector.y);
            walkVector = controller.GetFoward() * walkVector;
            walkVector *= controller.movementSpeed;
            
            //Apply input to character
            //controller.thisRigidBody.AddForce(walkVector, ForceMode.Force);
            controller.thisRigidBody.AddForce(walkVector, ForceMode.Impulse);

            //Rotate character
            controller.RotateBodyToFaceInput();
        }

    }
}