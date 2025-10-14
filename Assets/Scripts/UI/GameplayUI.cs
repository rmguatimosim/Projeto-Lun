
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
    [SerializeField] private GameObject objectContainer;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI currentType;
    [SerializeField] private TextMeshProUGUI value;

    //energy bar
    [Header("Energia")]
    public Image energyContainer;
    public List<Sprite> energyIcons;



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
            char cont = (char)pc.content;
            value.text = cont.ToString();
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


}
