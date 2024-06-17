using System;
using UnityEngine;
using Zenject;

public interface ITimeManager
{
    public event Action TimeRunOut;
    
    public float GameTime { get; }
    public float GameTillEnd { get; }
}

public class TimeManager : MonoBehaviour, ITimeManager
{
    [Inject] private IGameConfiguration _gameConfiguration;

    public event Action TimeRunOut;
    
    public float GameTime { get; private set; }
    public float GameTillEnd => _gameConfiguration.GameTime - GameTime;
    
    private bool _timeRunOut;

    private void Update()
    {
        GameTime += Time.deltaTime;
        
        if (!_timeRunOut && GameTime >= _gameConfiguration.GameTime)
        {
            _timeRunOut = true;
            TimeRunOut?.Invoke();
        }
    }
}
