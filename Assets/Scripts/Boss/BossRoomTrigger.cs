using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{

    public BoxCollider doorTrigger;
    private GameManager gm;

    void Awake()
    {
        gm = GameManager.Instance;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorTrigger.enabled = false;
            var player = gm.player;
            var rb = player.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionY |
                            RigidbodyConstraints.FreezeRotationX |
                            RigidbodyConstraints.FreezeRotationY |
                            RigidbodyConstraints.FreezeRotationZ;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }
}
