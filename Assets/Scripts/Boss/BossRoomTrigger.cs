using System.Collections;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{

    public DoorScript doorTrigger;
    private GameManager gm;
    public GameObject boss;
    public GameObject dialogTriggers;
    public float displayDialogsCooldown;

    void Awake()
    {
        gm = GameManager.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorTrigger.DisableDoor();
            boss.SetActive(true);
            gm.StartBossBattle();
            StartCoroutine(ActivateDialogTriggers());

        }

    }

    void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }

    private IEnumerator ActivateDialogTriggers()
    {
        yield return new WaitForSeconds(displayDialogsCooldown);
        dialogTriggers.SetActive(true);
    }
}
