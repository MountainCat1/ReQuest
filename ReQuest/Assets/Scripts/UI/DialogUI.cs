using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class DialogUI : MonoBehaviour
{
    [Inject] private IGameConfiguration _configuration;
    
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private GameObject dialogPanel;
    private Coroutine _typingCoroutine;
    private bool _dialogPanelShown;

    private void Start()
    {
        dialogPanel.SetActive(false);
    }

    public void ShowSentence(DialogSentence sentence)
    {
        if(_typingCoroutine != null)
            StopCoroutine(_typingCoroutine);
        
        _typingCoroutine = StartCoroutine(TypeSentenceCoroutine(sentence.Text));
    }
    
    private IEnumerator TypeSentenceCoroutine(string sentence)
    {
        dialogText.text = "";
        foreach (var letter in sentence)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(_configuration.TypingDelay);
        }
    }
    
    public void HideDialogPanel(bool force = false)
    {
        if(!_dialogPanelShown && !force)
            return;
        
        dialogPanel.SetActive(false);
        _dialogPanelShown = false;
    }
        
    public void ShowDialogPanel()
    {
        if(_dialogPanelShown)
            return;
        
        dialogPanel.SetActive(true);
        _dialogPanelShown = true;
    }
}