using UnityEngine;


namespace Player.States
{
    public class Hurt : State
    {
        private readonly PlayerController controller;

        private float timePassed;

        public Hurt(PlayerController controller) : base("Hurt")
        {
            this.controller = controller;
        }

        public override void Enter()
        {
            base.Enter();

            // //reset timer
            // timePassed = 0;

            // //pause damage
            // controller.thisLife.isVulnerable = false;

            // //update animator
            // controller.thisAnimator.SetTrigger("tHurt");

            // //update UI
            // var gameplayUI = GameManager.Instance.gameplayUI;
            // gameplayUI.playerHealthBar.SetHealth(controller.thisLife.health);
        }

        public override void Exit()
        {
            base.Exit();
            // //resume damage
            // controller.thisLife.isVulnerable = true;
        }

        public override void Update()
        {
            base.Update();

            // //switch to dead
            // if (controller.thisLife.IsDead())
            // {
            //     controller.stateMachine.ChangeState(controller.deadState);
            //     return;
            // }

            // //update timer
            // timePassed += Time.deltaTime;

            // //switch to idle
            // if (timePassed >= controller.hurtDuration)
            // {
            //     controller.stateMachine.ChangeState(controller.idleState);
            //     return;
            // }
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