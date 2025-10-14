using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetCheckpoint(other.transform.position);
            Debug.Log("Checkpoint reached!");
        }
    }
}