using Player;
using TMPro;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{

    public int expectedData;
    private Data data;
    private Animator thisAnimator;
    public VarForm expectedVarForm;
    public TextMeshProUGUI heightText;
    [HideInInspector] public bool wasActivated = false;

    void Awake()
    {
        data = gameObject.GetComponent<Data>();
        thisAnimator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        data.canReceive = !wasActivated;
        heightText.text = "ALTURA\n 6px";
    }

    void Update()
    {
        if (!wasActivated && data.content == expectedData)
        {
            heightText.text = "ALTURA\n"+expectedData+"px";
            thisAnimator.SetTrigger("tActivate");
            data.canReceive = false;
        }
    }

    public void DeleteObstacle()
    {
        Destroy(this);
    }
}
