using Player;
using TMPro;
using UnityEngine;

public class PylonScript : MonoBehaviour
{
    [Header("Interação")]
    public VarForm varForm;
    public VarForm backUp;
    public int expectedData;
    private Data data;
    [HideInInspector] public bool wasActivated;
    [HideInInspector] public bool canDamageBoss;

    [Header("Display")]
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI energyText;


    void Awake()
    {
        data = gameObject.GetComponent<Data>();
    }

    void Start()
    {
        //gameObject.GetComponentInChildren<MeshRenderer>().material = varForm.material;
        ChangeColor();
        data.canReceive = !wasActivated;
        SetTexts();
    }

    // Update is called once per frame
    void Update()
    {
        if (!wasActivated && data.content == expectedData)
        {
            if (varForm.varType == VarType.Char)
            {
                varForm = backUp;
                data.content = 0;
                expectedData = 69;
                SetTexts();
                ChangeColor();
                return;
            }
            wasActivated = true;
            canDamageBoss = true;
            SetTexts();
        }

    }

    private void SetTexts()
    {
        bool isChar = varForm.varType == VarType.Char;
        var error = (char)expectedData;
        if (!isChar || (isChar && wasActivated))
        {
            statusText.text = "ATIVO";
            if (!wasActivated)
            {
                var energy = 100 - expectedData;
                energyText.text = energy.ToString() + "%";
            }
            if (wasActivated)
            {
                energyText.text = "100%";
            }
        }
        if (isChar && !wasActivated)
        {
            string sign = "ATIVO";
            if (sign.Contains(error))
            {
                string result = sign.Replace(error, '#');
                statusText.text = result;
            }
            energyText.text = "##%";
        }
    }

    private void ChangeColor()
    {
        gameObject.GetComponentInChildren<MeshRenderer>().material = varForm.material;
    }

    public void SetTextBroken()
    {
        statusText.text = "######";
        energyText.text = "###";
    }
}
