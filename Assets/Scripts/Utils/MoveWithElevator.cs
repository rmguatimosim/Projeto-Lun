using UnityEngine;

public class MoveWithElevator : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
            //other.GetComponent<Animator>().SetBool("bCutscene", true);
            other.GetComponent<PlayerController>().isInCutscene = true;

        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            //other.GetComponent<Animator>().SetBool("bCutscene", false);
            other.GetComponent<PlayerController>().isInCutscene = false;
        }
    }
}
