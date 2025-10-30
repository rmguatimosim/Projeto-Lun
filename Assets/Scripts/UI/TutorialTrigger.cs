
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public bool hasNextPage;
    public TutorialTrigger nextPage;
    public int msgIndex;

    void Awake()
    {
        if (!hasNextPage)
        {
            nextPage = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameplayUI ui = GameManager.Instance.gameplayUI;
            ui.ToggleTutorial(this, msgIndex);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Destroy(this);
    }
}
