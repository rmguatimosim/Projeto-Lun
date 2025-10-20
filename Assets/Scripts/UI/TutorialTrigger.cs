using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
        void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameplayUI ui = GameManager.Instance.gameplayUI;
            ui.ToggleTutorial();
        }
    }
}
