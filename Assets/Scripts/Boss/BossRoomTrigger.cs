using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    private GameManager gm;


    //triggers
    public DoorScript doorTrigger;
    public GameObject boss;
    public GameObject dialogTriggers;

    //timer for the dialog triggers to appear
    public float displayDialogsCooldown;

    //camera
    public CinemachineCamera bossIntro;
    public float bossIntroTimer = 2f;

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
            StartCoroutine(EndIntroScene());
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (bossIntro.enabled)
        {
            bossIntro.enabled = false;
        }
        Destroy(gameObject);

    }

    private IEnumerator ActivateDialogTriggers()
    {
        yield return new WaitForSeconds(displayDialogsCooldown);
        dialogTriggers.SetActive(true);
    }

    private IEnumerator EndIntroScene()
    {
        yield return new WaitForSeconds(bossIntroTimer);
        bossIntro.enabled = false;
    }


}
