using UnityEngine;


namespace Player.States
{
    public class Idle : State
    {
        private readonly PlayerController controller;
        public Idle(PlayerController controller) : base("Idle")
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

            // //ignore if in cutscene
            // var bossBattleHandler = GameManager.Instance.bossBattleHandler;
            if (controller.isInCutscene) return;

            if (GameManager.Instance.isGameOver)
            {
                controller.stateMachine.ChangeState(controller.deadState);
                return;
            }

            //Switch to jump
            if (controller.hasJumpInput)
            {
                controller.stateMachine.ChangeState(controller.jumpState);
                return;
            }

            //Switch to walking
            if (!controller.movementVector.IsZero())
            {
                controller.stateMachine.ChangeState(controller.walkingState);
                return;
            }

            //switch to changeForm
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