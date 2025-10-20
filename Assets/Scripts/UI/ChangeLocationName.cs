using UnityEngine;

public class ChangeLocationName : MonoBehaviour
{
    public string locationName;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameplayUI ui = GameManager.Instance.gameplayUI;
            ui.SetLocationText(locationName);
        }
    }
}
