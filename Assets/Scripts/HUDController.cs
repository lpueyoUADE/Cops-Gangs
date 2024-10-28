using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    public enum SliderType
    {
        health,
        shield,
        ammo    
    }

    private Dictionary<SliderType, Slider> sliderDict;

    [SerializeField] TextMeshProUGUI entityName;
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider shieldSlider;
    [SerializeField] Slider ammoSlider;

    private void Start()
    {
        sliderDict = new Dictionary<SliderType, Slider>()
        {
            { SliderType.health, healthSlider},
            { SliderType.shield, shieldSlider},
            { SliderType.ammo, ammoSlider},
        };
    }
    public void SetName(string name)
    {
        entityName.text = name;
    }

    public void InitSlider(SliderType sliderType, float maxValue)
    {
        sliderDict[sliderType].maxValue = maxValue;
    }

    public void setSliderValue(SliderType sliderType, float currentValue)
    {
        sliderDict[sliderType].value = currentValue;
    }
}
