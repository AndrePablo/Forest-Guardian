using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient grad;
    public Image fill;

    public void SetMaxHealth (float health){

        slider.maxValue = health;
        slider.value = health;

        fill.color = grad.Evaluate(1f);
    }

    public void SetHealth(float health){
        
        slider.value = health;

        fill.color = grad.Evaluate(slider.normalizedValue);

    }

}
