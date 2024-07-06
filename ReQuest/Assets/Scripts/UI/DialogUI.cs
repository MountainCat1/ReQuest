using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogText;


    public void ShowSentence(DialogSentence sentence)
    {
        dialogText.text = sentence.Text;
    }
}


[CreateAssetMenu(fileName = "DialogData", menuName = "Dialog Data")]
public class DialogData : ScriptableObject
{
    [field: SerializeField] public List<DialogSentence> Sentences { get; set; }
}

[System.Serializable]
public struct DialogSentence
{
    [field: SerializeField] public string Text { get; set; }
}