using UnityEngine;


namespace Player.States
{
    public class Hurt : State
    {
        private readonly PlayerController controller;

        //private float timePassed;

        public Hurt(PlayerController controller) : base("Hurt")
        {
            this.controller = controller;
        }

        public override void Enter()
        {
            base.Enter();

            // //update UI
            // var gameplayUI = GameManager.Instance.gameplayUI;
            // gameplayUI.playerHealthBar.SetHealth(controller.thisLife.health);
        }

        public override void Exit()
        {
            base.Exit();
            
        }

        public override void Update()
        {
            base.Update();

            //switch to dead
            if (controller.thisHealth.IsDead())
            {
                controller.stateMachine.ChangeState(controller.deadState);
                GameManager.Instance.isGameOver = true;
                return;
            }
            //switch to change form
            controller.selectedVarForm = controller.forms[0];
            controller.formsIndex = 0;
            controller.hasChangeFormInput = true;
            controller.stateMachine.ChangeState(controller.changeFormState);           

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