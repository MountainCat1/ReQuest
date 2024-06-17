using System;
using UnityEngine;

[System.Serializable]
public class ModifiableValue : IReadonlyModifiableValue
{
    public event Action ValueChanged;
    
    private float? _baseValue = null;
    private float _currentValue;
    private float _minValue = 0;
    
    [SerializeField] private float maxValue;

    public float BaseValue
    {
        get { return _baseValue ?? maxValue; }
        set
        {
            _baseValue = value;
            UpdateCurrentValue();
        }
    }

    public float CurrentValue
    {
        get => _currentValue;
        set => _currentValue = value;
    }

    public float MinValue
    {
        get { return _minValue; }
        set
        {
            _minValue = value;
            UpdateCurrentValue();
        }
    }

    public float MaxValue
    {
        get { return maxValue; }
        set
        {
            maxValue = value;
            UpdateCurrentValue();
        }
    }


    public ModifiableValue(float baseValue, float minValue, float maxValue)
    {
        this._baseValue = baseValue;
        this._minValue = minValue;
        this.maxValue = maxValue;
        UpdateCurrentValue();
    }

    private void UpdateCurrentValue()
    {
        _currentValue = Mathf.Clamp(_currentValue, _minValue, maxValue);
    }
}