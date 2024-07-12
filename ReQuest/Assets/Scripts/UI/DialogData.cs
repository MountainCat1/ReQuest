using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "Dialog Data")]
public class DialogData : ScriptableObject
{
    [field: SerializeField] public List<DialogSentence> Sentences { get; set; }
}