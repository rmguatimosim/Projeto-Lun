
using UnityEngine;

namespace Behaviors.MeleeCreature.States
{
    public class Follow : State
    {
        private readonly MeleeCreatureController controller;
        private readonly MeleeCreatureHelper helper;

        private readonly float updateInterval = 1;
        private float updateCooldown;

        private float footstepInterval = 0.5f;
        private float footstepCooldown;

        public Follow(MeleeCreatureController controller) : base("Follow")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            //reset stuff
            updateCooldown = 0;

        }

        public override void Exit()
        {
            base.Exit();

        }

        public override void Update()
        {
            base.Update();

            //update destination
            if ((updateCooldown -= Time.deltaTime) <= 0)
            {
                updateCooldown = updateInterval;
                var player = GameManager.Instance.player;
                var playerPosition = player.transform.position;
                controller.thisAgent.SetDestination(playerPosition);
            }

                // Footstep sound logic
            if (controller.thisAgent.velocity.magnitude > 0.1f)
            {
                if ((footstepCooldown -= Time.deltaTime) <= 0)
                {
                    footstepCooldown = footstepInterval;
                    controller.audioSource.PlayOneShot(controller.footStepSound);
                }
            }


            //Attempt to attack
            if (helper.GetDistanceToPlayer() <= controller.distanceToAttack)
            {
                helper.FacePlayer();
                controller.stateMachine.ChangeState(controller.attackState);
                return;
            }

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }
    }

}
