using System;
using TMPro;
using UnityEngine;

public class RenameSign : MonoBehaviour
{
    public TextMeshProUGUI destination;
    public string text;

    void Start()
    {
        destination.text = text;
    }
}
