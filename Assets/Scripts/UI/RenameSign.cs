
using TMPro;
using UnityEngine;

public class RenameSign : MonoBehaviour
{
    //text update
    public TextMeshProUGUI destination;
    public string text;

    //arrow direction control
    public GameObject leftArrow;
    public GameObject rightArrow;
    [Header("Marcar se aponta pra esquerda")]
    public bool pointLeft;

    void Start()
    {
        destination.text = text;
        leftArrow.SetActive(pointLeft);
        rightArrow.SetActive(!pointLeft);
    }
}
