using System.Collections;
using TMPro;
using UnityEngine;

public class DataTextScript : MonoBehaviour
{
    private TextMeshProUGUI text;
    public Data data;
    private PlayerController pc;


    void Awake()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        pc = GameManager.Instance.player.GetComponent<PlayerController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetValueText();
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.hasChangeFormInput)
        {
            StartCoroutine(UpdateText());
            //SetValueText();
        }
    }

    public void SetValueText()
    {

        if (pc.selectedVarForm.varType == Player.VarType.Bool)
        {
            text.text = data.content == 0 ? "false" : "true";
        }
        else if (pc.selectedVarForm.varType == Player.VarType.Char)
        {
            char cont = (char)data.content;
            text.text = cont.ToString();
        }
        else
        {
            text.text = data.content.ToString();
        }

    }
    private IEnumerator UpdateText()
    {
        yield return new WaitForSeconds(0.6f);
        SetValueText();
    }
}
