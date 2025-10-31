using UnityEngine;

public class AssignToolHolder : MonoBehaviour
{
    private PlayerController pc;
    public GameObject assignTool;
    
    
    //triggers that this interaction enable
    public GameObject tutorialTrigger;
    public GameObject charDoorTutorialTrigger;
    public GameObject objectiveTrigger;

    void Awake()
    {
        pc = GameManager.Instance.player.GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pc.hasAssignTool = true;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            assignTool.SetActive(false);
            tutorialTrigger.SetActive(true);
            charDoorTutorialTrigger.SetActive(true);
            objectiveTrigger.SetActive(true);
        }
    }
}
