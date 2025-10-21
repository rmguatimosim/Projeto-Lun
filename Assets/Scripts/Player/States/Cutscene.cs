using UnityEngine;


namespace Player.States
{
    public class Cutscene : State
    {
        private readonly PlayerController controller;

        //private float timePassed;

        public Cutscene(PlayerController controller) : base("Cutscene")
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