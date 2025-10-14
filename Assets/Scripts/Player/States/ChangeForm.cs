using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YourNamespaceHere;


namespace Player.States
{
    public class ChangeForm : State
    {
        private readonly PlayerController controller;
        private readonly GameManager gm;
        private bool hasChanged;
        private float cooldown;
        public ChangeForm(PlayerController controller) : base("ChangeForm")
        {
            this.controller = controller;
            this.gm = GameManager.Instance;
        }

        public override void Enter()
        {
            base.Enter();

            //reset timer
            hasChanged = false;
            cooldown = 0.9f;

            if(controller.currentVarForm == controller.selectedVarForm)
            {
                controller.selectedVarForm = controller.forms[0];
            }

            // //change var type
            // TypeChange();
            // controller.currentVarForm = controller.selectedVarForm;

            //update animator
            controller.thisAnimator.SetBool("bChangeForm", true);
        }

        public override void Exit()
        {
            base.Exit();
            //change var type
            TypeChange();
            controller.currentVarForm = controller.selectedVarForm;
            controller.content = 0;
            controller.UpdateUI();

            //update animator
            controller.thisAnimator.SetBool("bChangeForm", false);
            controller.hasChangeFormInput = false;
        }

        public override void Update()
        {
            base.Update();

            //update timers
            cooldown -= Time.deltaTime;

            //switch to idle
            if (cooldown <= 0 && hasChanged)
            {
                controller.stateMachine.ChangeState(controller.idleState);
                return;
            }

            if (!hasChanged)
            {
                hasChanged = true;
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
        
        public void TypeChange()
        {
            foreach (GameObject part in controller.parts)
            {
                if (part != null)
                {
                    Material newMaterial = controller.selectedVarForm.material;
                    SkinnedMeshRenderer renderer = part.GetComponent<SkinnedMeshRenderer>();
                    if (renderer != null)
                    {
                        Material[] materials = renderer.materials;
                        materials[1] = newMaterial;
                        renderer.materials = materials;
                    }
                }
            }
        }


    }
}