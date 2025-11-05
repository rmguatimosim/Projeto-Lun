
using System;
using EventArgs;
using Player;
using UnityEngine;


public class Data : MonoBehaviour
{
    //components
    private PlayerController pc;
    private GameManager gm;
    private GameplayUI ui;

    //interaction
    [Header("Interaction")]
    public Interaction interaction;
    public int content;
    [HideInInspector] public bool canReceive = false;
    private bool canCopy;

    //dialog
    [Header("Diálogo")]
    public DialogEntry copyDamage;
    public DialogEntry storeDamage;

    public event EventHandler<SaveContentArgs> OnStoring;

    void Awake()
    {
        gm = GameManager.Instance;
        pc = gm.player.GetComponent<PlayerController>();
        ui = gm.gameplayUI;
    }

    void Start()
    {
        interaction.OnInteraction += OnInteraction;
        pc.OnCopy += OnCopy;
        this.OnStoring += OnStore;

    }

    private void OnInteraction(object sender, InteractionEventArgs args)
    {
        if (pc.hasInteracted) return;
        if (pc.currentVarForm.varType == VarType.Null)
        {
            return;
        }
        if (args.action == InteractionType.copy)
        {
            //Debug.Log("entrou no copy");
            canCopy = true;
            pc.Copy(content, gameObject);
            pc.hasInteracted = true;
            return;
            
        }
        if (args.action == InteractionType.store && canReceive)
        {
            Store();
            //Debug.Log("entrou no store");
            pc.hasInteracted = true;
            return;
        }
    }

    private void Store()
    {
        //Invoke event
        OnStoring?.Invoke(this, new SaveContentArgs
        {
            content = pc.content,
            type = pc.currentVarForm.varType
        });
        pc.assignToolAudioSource.PlayOneShot(pc.storeSFX);
    }

    private void OnCopy(object sender, SaveContentArgs args)
    {
        if (!canCopy) return;
        if (args.target.GetComponent<DoorScript>())
        {
            var door = args.target.GetComponent<DoorScript>();
            if (door.varForm.varType == pc.currentVarForm.varType)
            {
                pc.content = args.content;
            }
            else
            {
                CopyDamageWithDialog(1);


            }
        }
        else if (args.target.GetComponent<Switch>())
        {
            var button = args.target.GetComponent<Switch>();
            if (button.expectedVarForm.varType == pc.currentVarForm.varType)
            {
                pc.content = args.content;
            }
            else
            {
                CopyDamageWithDialog(1);
            }
        }
        else
        {
            pc.content = args.content;
        }
        canCopy = false;
    }
    private void OnStore(object sender, SaveContentArgs args)
    {
        GameManager gm = GameManager.Instance;
        if (gameObject.GetComponent<DoorScript>() != null)
        {
            var door = gameObject.GetComponent<DoorScript>();
            if (door.varForm.varType == args.type && door.expectedData == args.content)
            {
                content = args.content;
                gm.IncreaseScore();

            }
            else
            {
                StoreDamageWithDialog(1);
            }
        }
        if (gameObject.GetComponent<Switch>() != null)
        {
            var pressedSwitch = gameObject.GetComponent<Switch>();
            if (pressedSwitch.expectedVarForm.varType == args.type)
            {
                content = args.content;
                gm.IncreaseScore();
            }
            else
            {
                //Debug.Log("Tipo inválido para abrir a porta");
                //pc.thisHealth.Damage(1);
                StoreDamageWithDialog(1);

            }
        }
        if (gameObject.GetComponent<ObstacleScript>() != null)
        {
            var obstacle = gameObject.GetComponent<ObstacleScript>();
            if (obstacle.expectedVarForm.varType == args.type && args.content == obstacle.expectedData)
            {
                content = args.content;
                gm.IncreaseScore();
            }
            else
            {
                // //Debug.Log("Tipo inválido");
                // pc.thisHealth.Damage(1);
                StoreDamageWithDialog(1);

            }
        }
        if (gameObject.GetComponent<PylonScript>() != null)
        {
            var pylon = gameObject.GetComponent<PylonScript>();
            if (pylon.varForm.varType == args.type)
            {
                content = args.content;
                gm.IncreaseScore();
            }
            else
            {
                // //Debug.Log("Tipo inválido");
                // pc.thisHealth.Damage(1);
                StoreDamageWithDialog(1);

            }
        }

        pc.UpdateUI();
    }

    private void CopyDamageWithDialog(int amount)
    {
        pc.thisHealth.Damage(amount);
        if (copyDamage != null)
        {
            ui.SetDialogText(copyDamage);
        }
    }
    
    private void StoreDamageWithDialog(int amount)
    {
        pc.thisHealth.Damage(amount);
        if (storeDamage != null)
        {
            ui.SetDialogText(storeDamage);
        }
    }
    



}

