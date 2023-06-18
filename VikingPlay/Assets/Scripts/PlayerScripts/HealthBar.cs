using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _fill;
    
    public void SetHealth(int health)
    {
        _healthSlider.value = health;
        _fill.color = _gradient.Evaluate(_healthSlider.normalizedValue);
    }
    
    public void SetMaxHealth(int health)
    {
        _healthSlider.maxValue = health;
        _healthSlider.value = health;
        _fill.color = _gradient.Evaluate(1f);
    }
    
}
