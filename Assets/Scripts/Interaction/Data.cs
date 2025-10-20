
using System;
using EventArgs;
using Player;
using UnityEngine;


public class Data : MonoBehaviour
{
    //components
    private PlayerController pc;

    //interaction
    [Header("Interaction")]
    public Interaction interaction;
    public int content;
    [HideInInspector]public bool canReceive = false;

    public event EventHandler<SaveContentArgs> OnStoring;

    void Awake()
    {
        pc = GameManager.Instance.player.GetComponent<PlayerController>();
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
            Debug.Log("entrou no copy");
            pc.Copy(content, gameObject);
            pc.hasInteracted = true;
            return;
            
        }
        if (args.action == InteractionType.store && canReceive)
        {
            Store();
            Debug.Log("entrou no store");
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
    }

    private void OnCopy(object sender, SaveContentArgs args)
    {
        if (args.target.GetComponent<DoorScript>())
        {
            var door = args.target.GetComponent<DoorScript>();
            if (door.varForm.varType == pc.currentVarForm.varType)
            {
                pc.content = args.content;
            }
            else
            {
                Debug.Log("Impossível receber valor de variável de tipo diferente");
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
                Debug.Log("Impossível receber valor de variável de tipo diferente");
            }
        }
        else
        {
            pc.content = args.content;
        }
    }
    private void OnStore(object sender, SaveContentArgs args)
    {
        if (gameObject.GetComponent<DoorScript>() != null)
        {
            var door = gameObject.GetComponent<DoorScript>();
            if (door.varForm.varType == args.type && door.expectedData == args.content)
            {
                content = args.content;
            }
            else
            {
                Debug.Log("Tipo inválido");
                pc.thisHealth.Damage(1);
            }
        }
        if (gameObject.GetComponent<Switch>() != null)
        {
            var pressedSwitch = gameObject.GetComponent<Switch>();
            if (pressedSwitch.expectedVarForm.varType == args.type)
            {
                content = args.content;
            }
            else
            {
                Debug.Log("Tipo inválido para abrir a porta");
                pc.thisHealth.Damage(1);
            }
        }
        if (gameObject.GetComponent<ObstacleScript>() != null)
        {
            var obstacle = gameObject.GetComponent<ObstacleScript>();
            if (obstacle.expectedVarForm.varType == args.type)
            {
                content = args.content;
            }
            else
            {
                Debug.Log("Tipo inválido");
                pc.thisHealth.Damage(1);
            }
        }
        if (gameObject.GetComponent<PylonScript>() != null)
        {
            var pylon = gameObject.GetComponent<PylonScript>();
            if (pylon.varForm.varType == args.type)
            {
                content = args.content;
            }
            else
            {
                Debug.Log("Tipo inválido");
                pc.thisHealth.Damage(1);
            }
        }

        pc.UpdateUI();
    }
    



}

