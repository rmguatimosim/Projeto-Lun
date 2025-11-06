using System.Collections.Generic;
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
    public List<MeshRenderer> meshs;
    public GameObject dialogChangeForm;

    [Header("Display")]
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI energyText;

    [Header("FX")]
    public GameObject electricityFX;
    public GameObject sphere;


    void Awake()
    {
        data = gameObject.GetComponent<Data>();
    }

    void Start()
    {
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
                if(dialogChangeForm != null)
                {
                    dialogChangeForm.SetActive(true);   
                }
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
        foreach(MeshRenderer m in meshs)
        {
            m.material = varForm.material;
        }
    }

    public void SetTextBroken()
    {
        statusText.text = "######";
        energyText.text = "###";
    }

    public void StartElectricity()
    {
        var fxPosition = sphere.transform.position;
        var fxRotation = electricityFX.transform.rotation;
        Instantiate(electricityFX, fxPosition, fxRotation);
    }
}
