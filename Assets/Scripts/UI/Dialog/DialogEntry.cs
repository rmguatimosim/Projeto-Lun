using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogEntry", menuName = "Dialog/Dialog Entry")]
public class DialogEntry : ScriptableObject
{
    public string key; // e.g. "start"
    [TextArea(3, 15)]
    public string message;
}
