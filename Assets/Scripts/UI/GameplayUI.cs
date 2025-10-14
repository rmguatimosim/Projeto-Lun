using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Canvas thisCanvas;

    [Header("Objects")]
    private PlayerController pc;
    [SerializeField] private GameObject objectContainer;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI currentType;
    [SerializeField] private TextMeshProUGUI value;

    //private List<ObjectEntry> entries = new();

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


    // public void AddObjects(ItemType type)
    // {
    //     if (type != ItemType.Key && type != ItemType.BossKey) return;

    //     //create widget
    //     var template = type == ItemType.Key ? keyTemplate : bossKeyTemplate;
    //     var widget = Instantiate(template, template.transform);
    //     widget.SetActive(true);
    //     widget.transform.SetParent(objectContainer.transform);

    //     //create entry
    //     ObjectEntry entry = new ObjectEntry
    //     {
    //         type = type,
    //         widget = widget
    //     };
    //     entries.Add(entry);
    // }

    // public void RemoveObject(ItemType type)
    // {
    //     for (int i = 0; i < entries.Count; i++)
    //     {
    //         var entry = entries[i];
    //         if (entry.type == type)
    //         {
    //             Destroy(entry.widget);
    //             entries.RemoveAt(i);
    //             return;
    //         }
    //     }
    // }




}
