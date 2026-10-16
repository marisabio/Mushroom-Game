using Unity.Cinemachine;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string actor;
    [TextArea(2, 5)] public string text;
    public CinemachineCamera camera;
}

[System.Serializable]
public class DialogueSet
{
    public string label;
    public DialogueLine[] lines;
}