using UnityEngine;

public enum DialogType
{
    Default = 0,
    SelfTalk = 1,
}

[System.Serializable]
public struct DialogSentence
{
    [field: SerializeField] public DialogType Type { get; set; }
    [field: SerializeField] public string Text { get; set; }
}