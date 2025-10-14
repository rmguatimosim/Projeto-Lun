using UnityEngine;


namespace Player.States
{
    public class Assign : State
    {
        private readonly PlayerController controller;
        private bool actionDone;
        private float cooldown;
        

        public Assign(PlayerController controller) : base("Assign")
        {
            this.controller = controller;
        }

        public override void Enter()
        {
            base.Enter();
            //reset timer
            actionDone = false;
            cooldown = 0.5f;

            //perform interaction
            if (controller.hasCopyInput)
            {
                controller.thisAnimator.SetTrigger("tCopy");
            }
            if (controller.hasStoreInput)
            {
                controller.thisAnimator.SetTrigger("tStore");
            }


        }

        public override void Exit()
        {
            base.Exit();

        }

        public override void Update()
        {
            base.Update();

            //update timer
            cooldown -= Time.deltaTime;

            //switch to idle
            if (cooldown <= 0 && actionDone)
            {
                controller.stateMachine.ChangeState(controller.idleState);
                return;
            }
            if (!actionDone)
            {
                actionDone = true;
            }

        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

        }

    }
}