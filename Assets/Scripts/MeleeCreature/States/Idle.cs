
using UnityEngine;

namespace Behaviors.MeleeCreature.States
{
    public class Idle : State
    {
        private readonly MeleeCreatureController controller;
        private readonly MeleeCreatureHelper helper;

        private float searchCooldown;
        public Idle(MeleeCreatureController controller) : base("Idle")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            //reset stuff
            searchCooldown = controller.targetSearchInterval;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            //ignore if game is over
            if (GameManager.Instance.isGameOver) return;

            // update cooldown
            searchCooldown -= Time.deltaTime;
            if (searchCooldown <= 0)
            {
                searchCooldown = controller.targetSearchInterval;

                //search for player
                if (helper.IsPlayerOnSight())
                {
                    controller.stateMachine.ChangeState(controller.followState);
                    return;
                }
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
