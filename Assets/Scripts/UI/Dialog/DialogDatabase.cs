using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogDatabase", menuName = "Dialog/Dialog Database")]
public class DialogDatabase : ScriptableObject
{
    public List<DialogEntry> entries;

    private Dictionary<string, string> dialogMap;

    public void Initialize()
    {
        dialogMap = new Dictionary<string, string>();
        foreach (var entry in entries)
        {
            if (!dialogMap.ContainsKey(entry.key))
                dialogMap.Add(entry.key, entry.message);
        }
    }

    public string GetDialog(string key)
    {
        if (dialogMap == null) Initialize();

        if (dialogMap.TryGetValue(key, out string message))
            return message;

        return $"Mensagem não encontrada: {key}";
    }



    public float CalculateTimeToClose(string key)
    {
        if (dialogMap == null) Initialize();

        if (!dialogMap.ContainsKey(key)) return 4f;

        float displayTime = dialogMap[key].Length / 50f;
        return Mathf.Max(displayTime, 4f);
    }
}

// bool = #55FF56
// char = #C53333
// int = #052D9C
