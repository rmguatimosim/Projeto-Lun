using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera playerCamera; 

    void Awake()
    {
        playerCamera = GameManager.Instance.playerCamera;
    }

    // Update is called once per frame
    void Update()
    {

        //face camera
        transform.rotation = playerCamera.transform.rotation;
        
    }
}
