using UnityEngine;

public class AssignToolHolder : MonoBehaviour
{
    private PlayerController pc;
    public GameObject assignTool;

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
        }
    }
}
