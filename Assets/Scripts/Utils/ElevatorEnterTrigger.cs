using UnityEngine;

public class ElevatorEnterTrigger : MonoBehaviour
{
    public GameObject elevator;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var status = other.GetComponent<PlayerController>().isInCutscene;
            other.GetComponent<PlayerController>().isInCutscene = !status;
            if (!status)
            {
                other.transform.SetParent(elevator.transform);
            }
            else
            {
                other.transform.SetParent(null);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
    }
}
