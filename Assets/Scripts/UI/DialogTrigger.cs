
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public DialogEntry dialogKey;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameplayUI ui = GameManager.Instance.gameplayUI;
            ui.SetDialogText(dialogKey);
            Destroy(gameObject);        
        }
    }


}
