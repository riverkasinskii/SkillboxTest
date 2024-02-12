using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, ISaveable
{
    [SerializeField] private Slider slider;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);        
    }

    public void SetHealthValue(float currentHealth, float maxHealth)
    {
        slider.gameObject.SetActive(currentHealth < maxHealth);        
        slider.value = currentHealth / maxHealth;        
    }

    public void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }

    public object CaptureState()
    {
        return slider.value;
    }

    public void RestoreState(object state)
    {
        slider.value = (float)state;
        gameObject.SetActive(true);
    }
}
