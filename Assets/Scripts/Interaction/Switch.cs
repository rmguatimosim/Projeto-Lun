
using Player;
using TMPro;
using UnityEngine;

public class Switch : MonoBehaviour
{
    //get components
    private Animator thisAnimator;

    //object affected by the switch
    [Header("Puzzle")]
    public GameObject controlledObject;
    public SwitchType type;
    [HideInInspector] public bool expectedData;
    public VarForm expectedVarForm;
    private Data data;
    public TextMeshProUGUI text;
    [HideInInspector] public bool wasActivated = false;

    void Awake()
    {
        data = gameObject.GetComponent<Data>();
        thisAnimator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        data.canReceive = !wasActivated;
        expectedData = type == SwitchType.on;
        thisAnimator.SetBool("bPress", !expectedData);
        SetText(!expectedData ? "Ligado" : "Desligado");
    }

    // Update is called once per frame
    void Update()
    {
        if (!wasActivated && data.content != 0 == expectedData)
        {
            wasActivated = true;
            thisAnimator.SetBool("bPress", expectedData);
            controlledObject.GetComponent<Animator>().SetTrigger("tActivate");
            SetText(expectedData ? "Ligado" : "Desligado");
        }
    }

    private void SetText(string msg)
    {
        text.text = msg;
    }
}
