using System.Collections;
using UnityEngine;

public class TriggerNextDialog : MonoBehaviour
{
    public GameObject dialogTrigger;
    public float timer;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TriggerDialog());
        }
    }

    private IEnumerator TriggerDialog()
    {
        yield return new WaitForSeconds(timer);      
        if(dialogTrigger != null)
        {
            dialogTrigger.SetActive(!dialogTrigger.activeSelf); 
        }  
        Destroy(gameObject);
    }
}
