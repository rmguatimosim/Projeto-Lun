using Player;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    //opening mechanism
    [Header("Main")]
    public GameObject door;
    private MeshRenderer[] doorRenderers;
    private Animator thisAnimator;
    private bool isClosed = true;
    public VarForm varForm;
    private bool isSolved;

    //panel control
    public Canvas display;
    public CanvasGroup puzzleDisplay;

    //puzzle 
    [Header("Puzzle")]
    private Data data;
    [HideInInspector]public bool isActive;
    public int expectedData = -1;


    void Awake()
    {
        doorRenderers = door.GetComponentsInChildren<MeshRenderer>();
        data = GetComponent<Data>();
        thisAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        //change door color
        if (varForm.varType != VarType.Null)
        {
            foreach (MeshRenderer renderer in doorRenderers)
            {
                renderer.material = varForm.material;
            }
        }
        isActive = expectedData == data.content;
        GetComponent<BoxCollider>().enabled = isActive;
        puzzleDisplay.alpha = !isActive ? 1 : 0;
        
    }

    void Update()
    {
        if (!isActive)
        {
            isActive = expectedData == data.content;
            if (isActive)
            {
                data.canReceive = !isActive;
                GetComponent<BoxCollider>().enabled = isActive;
                isSolved = true;
                puzzleDisplay.alpha = 0;    
                
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (varForm.varType == VarType.Null || isSolved)
        {
            thisAnimator.SetBool("bDoorOpen", true);
            isClosed = false;
            return;
        }
        if (other.CompareTag("Player"))
        {
            PlayerController controller = other.GetComponent<PlayerController>();
            if (controller.currentVarForm.varType == varForm.varType)
            {
                thisAnimator.SetBool("bDoorOpen", true);
                isClosed = false;
                isSolved = true;
            }
            else
            {
                Debug.Log("Forma errada");
            }
        }
    }
    

    void OnTriggerExit(Collider other)
    {
        if (!isClosed)
        {
            isClosed = true;
            thisAnimator.SetBool("bDoorOpen", false);
        }
    }
}
