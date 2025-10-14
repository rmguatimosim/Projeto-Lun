using Player;
using TMPro;
using UnityEngine;

public class DoorPasswordScript : MonoBehaviour
{

    public TextMeshProUGUI hintText;
    public DoorScript doorScript;
    public GameObject puzzleText;
    private char hint;
    private float cooldown;
    private readonly float timer = 1f;



    void Start()
    {
        cooldown = timer;        
    }

    // Update is called once per frame
    void Update()
    {
        if (doorScript.varForm.varType == VarType.Int)
        {
            if ((cooldown -= Time.deltaTime) <= 0)
            {
                hint = (char)doorScript.expectedData;
                hintText.text = hint.ToString();
                cooldown = timer;
            }
        }
        else
        {
            puzzleText.SetActive(false);
        }
    }
}
