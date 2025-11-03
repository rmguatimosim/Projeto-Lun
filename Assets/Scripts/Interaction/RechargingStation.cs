using UnityEngine;

public class RechargingStation : MonoBehaviour
{
    //dialog
    public DialogEntry dialog;
    //audio
    [Header("Audio")]
    public AudioClip rechargeSound;
    public AudioSource stationAudioSource;

    //check if the station has been used before
    private bool hasBeenUsed;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !hasBeenUsed)
        {
            GameplayUI ui = GameManager.Instance.gameplayUI;
            PlayerController pc = GameManager.Instance.player.GetComponent<PlayerController>();
            pc.thisHealth.Heal();
            hasBeenUsed = true;
            stationAudioSource.PlayOneShot(rechargeSound, 1);
            ui.SetDialogText(dialog);
        }
    }
}
