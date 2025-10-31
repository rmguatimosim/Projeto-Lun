using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public int objectiveIndex;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameplayUI ui = GameManager.Instance.gameplayUI;
            ui.SetObjectiveText(objectiveIndex);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Destroy(this);
    }
}
