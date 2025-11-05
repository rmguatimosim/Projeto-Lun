

using UnityEngine;

namespace Behaviors.MeleeCreature.States
{
    public class Hurt : State
    {
        private readonly MeleeCreatureController controller;
        private readonly MeleeCreatureHelper helper;

        private float timePassed;
        public Hurt(MeleeCreatureController controller) : base("Hurt")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            //reset stuff
            timePassed = 0;


            //update animator
            controller.thisAnimator.SetTrigger("tImpact");

            //play sound
            controller.audioSource.PlayOneShot(controller.hitWallSound, 1.5f);

        }

        public override void Exit()
        {
            base.Exit();

        }

        public override void Update()
        {
            base.Update();

            if (controller.defeated)
            {
                controller.stateMachine.ChangeState(controller.deadState);
            }
            //update timer
            timePassed += Time.deltaTime;

            //Switch states
            if (timePassed >= controller.hurtDuration)
            {
                controller.stateMachine.ChangeState(controller.idleState);
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
