using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthbarUI : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Creature creature;

        private void Start()
        {
            creature.Health.ValueChanged += OnHealthChanged;
            OnHealthChanged();
        }

        private void OnHealthChanged()
        {
            slider.maxValue = creature.Health.MaxValue;
            slider.value = creature.Health.CurrentValue;
        }
    }
}