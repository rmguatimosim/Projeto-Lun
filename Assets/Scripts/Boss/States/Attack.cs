
using UnityEngine;

namespace Behaviors.MeleeCreature.States
{
    public class Attack : State
    {
        private MeleeCreatureController controller;
        private MeleeCreatureHelper helper;

        private Vector3 attackDirection;
        private float chargingTime;
        
        public Attack(MeleeCreatureController controller) : base("Attack")
        {
            this.controller = controller;
            this.helper = controller.helper;
        }

        public override void Enter()
        {
            base.Enter();

            //reset timer
            chargingTime = 0;

            //update animator
            controller.thisAnimator.SetTrigger("tAttack");
            controller.isAttacking = true;

            //Get player direction
            var player = GameManager.Instance.player;
            attackDirection = (player.transform.position - controller.transform.position).normalized;

            //Disable navMeshAgent auto pathing
            controller.thisAgent.isStopped = true;
            controller.thisAgent.updatePosition = false;
            controller.thisAgent.updateRotation = false;

            //play sound
            controller.audioSource.PlayOneShot(controller.detectPlayerSound);
           

        }

        public override void Exit()
        {
            base.Exit();
            //Enable navMeshAgent auto pathing
            controller.thisAgent.isStopped = false;
            controller.thisAgent.updatePosition = true;
            controller.thisAgent.updateRotation = true;
        }

        public override void Update()
        {
            base.Update();

            chargingTime += Time.deltaTime;

            //Face player
            //helper.FacePlayer();

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (chargingTime >= controller.chargeDuration)
            {
                float speed = controller.attackSpeed;
                Vector3 movement = speed * Time.deltaTime * attackDirection;
                controller.thisAgent.Warp(controller.transform.position + movement);
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();
        }

    }

}
