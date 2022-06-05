using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider slider;
    [SerializeField] private Image fill;
    
    public Gradient gradient;

    
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void HealthBarDestroy()
    {
        Destroy(transform.parent.gameObject);
    }

    public void TurnOffBillboard()
    {
        transform.GetComponentInParent<Bilboard>().enabled = false;
        transform.parent.localRotation = Quaternion.identity;
    }

    public void TurnOnBillboard()
    {
        transform.GetComponentInParent<Bilboard>().enabled = true;
    }
}
