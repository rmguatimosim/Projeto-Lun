using UnityEngine;

public class GasBlockadePushBack : MonoBehaviour
{
    //sets  the strengh of the  knockback
    public int knockback;
    public DialogEntry dialog;

    //pushs the player back
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var other = collision.gameObject;

            //knockback
            var positionDiff = other.transform.position - gameObject.transform.position;
            var impulseVector = new Vector3(positionDiff.normalized.x, 0, positionDiff.normalized.z);
            impulseVector *= knockback;
            other.GetComponent<HealthScript>().Damage(1);
            GameManager.Instance.gameplayUI.SetDialogText(dialog);
            other.GetComponent<Rigidbody>().AddForce(impulseVector, ForceMode.Impulse);
        }
    }
}
