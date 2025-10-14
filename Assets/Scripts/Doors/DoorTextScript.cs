using Player;
using TMPro;
using UnityEngine;

public class DoorTextScript : MonoBehaviour
{

    private TextMeshProUGUI text;
    public Data data;
    public DoorScript doorScript;
    private char error;
    private float cooldown;
    private readonly float timer = 1f;


    void Awake()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        SetValueText("PORTA");
        cooldown = timer;
        error = (char)doorScript.expectedData;
    }

    void Update()
    {
        if ((cooldown -= Time.deltaTime) <= 0) 
        {
            if (!doorScript.isActive)
            {
                if (doorScript.varForm.varType == VarType.Char)
                {
                    string sign = text.text;
                    if (sign.Contains(error))
                    {
                        string result = sign.Replace(error, '#');
                        if (text.text != result)
                        {
                            SetValueText(result);
                        }
                    }
                }
                else if (doorScript.varForm.varType == VarType.Int)
                {
                    SetValueText("senha");
                }
                else
                {
                    SetValueText("PORTA");
                }
            }
            if (doorScript.isActive)
            {
                SetValueText("PORTA");
            }
            cooldown = timer;
        }
    }

    public void SetValueText(string msg)
    {
        text.text = msg;
    }
}
