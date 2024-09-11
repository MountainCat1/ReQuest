using System;
using UnityEngine;
using Zenject;

namespace Managers
{
    public interface IPhaseManager
    {
        int CurrentPhase { get; }
        event Action<int> PhaseChanged;
    }

    public class PhaseManager : MonoBehaviour, IPhaseManager
    {
        [Inject] private ITimeManager _timeManager;
        
        public event Action<int> PhaseChanged;

        public int CurrentPhase { get; private set; } = 0;
        
        [SerializeField] private int firstPhaseTime;
        [SerializeField] private int secondPhaseTime;
        [SerializeField] private int thirdPhaseTime;
        [SerializeField] private int fourthPhaseTime;
        [SerializeField] private int fifthPhaseTime;
        
        [Inject] 
        private void Initialize()
        {
            _timeManager.NewSecond += OnNewSecond;
        }

        private void OnNewSecond(int seconds)
        {
            if (seconds == firstPhaseTime)
            {
                CurrentPhase = 1;
                PhaseChanged?.Invoke(CurrentPhase);
                Debug.Log("First phase");
            }
            else if (seconds == secondPhaseTime)
            {
                CurrentPhase = 2;
                PhaseChanged?.Invoke(CurrentPhase);
                Debug.Log("Second phase");
            }
            else if (seconds == thirdPhaseTime)
            {
                CurrentPhase = 3;
                PhaseChanged?.Invoke(CurrentPhase);
                Debug.Log("Third phase");
            }
            else if (seconds == fourthPhaseTime)
            {
                CurrentPhase = 4;
                PhaseChanged?.Invoke(CurrentPhase);
                Debug.Log("Fourth phase");
            }
            else if (seconds == fifthPhaseTime)
            {
                CurrentPhase = 5;
                PhaseChanged?.Invoke(CurrentPhase);
                Debug.Log("Fifth phase");
            }
            else if (seconds >= _timeManager.GameTime)
            {
                CurrentPhase = 6;
                PhaseChanged?.Invoke(CurrentPhase);
                Debug.Log("The end...");
            }
        }
    }
}