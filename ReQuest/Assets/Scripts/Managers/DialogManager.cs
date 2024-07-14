using System;
using UnityEngine;
using Zenject;

namespace Managers
{
    public interface IDialogManager
    {
        public void ShowDialog(DialogData dialogData);
    }

    public class DialogManager : MonoBehaviour, IDialogManager
    {
        [Inject] private IInputManager _inputManager;

        private DialogUI _dialogUI;
        
        private DialogData _currentDialogData;
        private int _currentSentenceIndex;

        private void Awake()
        {
            _dialogUI = FindObjectOfType<DialogUI>() ?? throw new NullReferenceException("DialogUI not found");
        }

        void Start()
        {
            _inputManager.OnSkip += NextDialog;
        }

        private void NextDialog()
        {
            if (_currentDialogData == null)
                return;

            if (_currentSentenceIndex < _currentDialogData.Sentences.Count - 1)
            {
                _currentSentenceIndex++;
                ShowSentence(_currentDialogData.Sentences[_currentSentenceIndex]);
            }
            else
            {
                _currentDialogData = null;
                _currentSentenceIndex = 0;
                _dialogUI.HideDialogPanel();
            }
        }

        public void ShowDialog(DialogData dialogData)
        {
            _currentDialogData = dialogData;
            _currentSentenceIndex = 0;
            ShowSentence(_currentDialogData.Sentences[_currentSentenceIndex]);
        }

        private void ShowSentence(DialogSentence sentence)
        {
            _dialogUI.ShowDialogPanel();

            _dialogUI.ShowSentence(sentence);
        }
    }
}