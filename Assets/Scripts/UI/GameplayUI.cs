
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Canvas thisCanvas;

    [Header("Objects")]
    private PlayerController pc;

    //display form and content
    [Header("Forma e conteúdo")]
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI currentType;
    [SerializeField] private TextMeshProUGUI value;

    //tutorial
    [Header("Tutorial")]
    [SerializeField] private GameObject tutorial;
    private bool hasPressedKey;

    //energy bar
    [Header("Energia")]
    public Image energyContainer;
    public List<Sprite> energyIcons;

    //display  location
    [Header("Localização")]
    [SerializeField] private TextMeshProUGUI location;

    //display objective
    [Header("Objetivo")]
    [SerializeField] private TextMeshProUGUI objectiveText;



    void Awake()
    {
        pc = GameManager.Instance.player.GetComponent<PlayerController>();

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasPressedKey && pc.anyKeyAction.ReadValue<float>() == 1)
        {
            hasPressedKey = true;
            ToggleTutorial(false);
            //pc.stateMachine.ChangeState(pc.idleState);
        }

    }

    public void SetTypeText()
    {
        typeText.text = pc.selectedVarForm.varType.ToString();

    }
    public void SetCurrentTypeText()
    {
        currentType.text = pc.currentVarForm.varType.ToString();
    }
    public void SetValueText()
    {
        if (pc.currentVarForm.varType == Player.VarType.Null)
        {
            value.text = "null";
        }
        else if (pc.currentVarForm.varType == Player.VarType.Bool)
        {
            value.text = pc.content == 0 ? "false" : "true";
        }
        else if (pc.currentVarForm.varType == Player.VarType.Char)
        {
            if(pc.content == 0)
            {
                value.text = "' '";
                return;
            }
            char cont = (char)pc.content;
            value.text = "'" + cont.ToString() + "'";
        }
        else
        {
            value.text = pc.content.ToString();
        }
    }

    public void SetEnergyBar()
    {
        energyContainer.sprite = energyIcons[pc.thisHealth.health];
    }

    public void SetLocationText(string txt)
    {
        location.text = txt;
    }

    public void ToggleTutorial(bool status)
    {
        if (hasPressedKey)
        {
            hasPressedKey = false;
        }
        tutorial.SetActive(status);
        //pc.stateMachine.ChangeState(pc.cutsceneState);
    }

    public void SetObjectiveText(string txt)
    {
        objectiveText.text = txt;
    }




}
