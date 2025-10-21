using UnityEngine;

public class RechargingStation : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
        PlayerController pc = GameManager.Instance.player.GetComponent<PlayerController>();
        pc.thisHealth.Heal();
    }
}
