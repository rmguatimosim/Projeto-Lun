

using UnityEngine;

namespace Behaviors.MeleeCreature.States
{
    public class Dead : State
    {
        private MeleeCreatureController controller;
        private MeleeCreatureHelper helper;
        public Dead(MeleeCreatureController controller) : base("Dead")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Boss is dead");

            //update animator
            controller.thisAnimator.SetTrigger("tDead");
            GameManager.Instance.Endgame();

        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

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
