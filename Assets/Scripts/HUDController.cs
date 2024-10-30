using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    public enum SliderType
    {
        Life,
        Shield,
        Ammo    
    }

    private Dictionary<SliderType, Slider> sliderDict;

    [Header("Parameters")]
    [SerializeField] TextMeshProUGUI entityName;
    [SerializeField] Slider lifeSlider;
    [SerializeField] Slider shieldSlider;
    [SerializeField] Slider ammoSlider;

    [Header("Position offset")]
    [SerializeField] Vector3 offset;

    [Header("Canvas")]
    [SerializeField] Transform canvas;

    [Header("Entity")]
    [SerializeField] EntityModel entityModel;

    private void Awake()
    {
        entityModel.OnNameAlteredAction += OnNameAlteredActionHandler;
        entityModel.OnLifePointsAlteredAction += OnLifePointsAlteredActionHandler;
        entityModel.OnShieldPointsAlteredAction += OnShieldPointsAlteredActionHandler;
        entityModel.OnAmmoAlteredAction += OnAmmoAlteredActionHandler;
    }

    private void OnDestroy()
    {
        entityModel.OnNameAlteredAction += OnNameAlteredActionHandler;
        entityModel.OnLifePointsAlteredAction += OnLifePointsAlteredActionHandler;
        entityModel.OnShieldPointsAlteredAction += OnShieldPointsAlteredActionHandler;
        entityModel.OnAmmoAlteredAction += OnAmmoAlteredActionHandler;
    }

    private void OnNameAlteredActionHandler(string name)
    {
        SetName(name);
    }

    private void OnLifePointsAlteredActionHandler(float lifePoints)
    {
        SetSliderValue(SliderType.Life, lifePoints);
    }

    private void OnShieldPointsAlteredActionHandler(float shieldPoints)
    {
        SetSliderValue(SliderType.Shield, shieldPoints);
    }
    private void OnAmmoAlteredActionHandler(float ammo)
    {
        SetSliderValue(SliderType.Ammo, ammo);
    }

    private void Start()
    {
        sliderDict = new Dictionary<SliderType, Slider>()
        {
            { SliderType.Life, lifeSlider},
            { SliderType.Shield, shieldSlider},
            { SliderType.Ammo, ammoSlider},
        };

        transform.SetParent(canvas);

        InitSlider(SliderType.Life, entityModel.MaxLifePoints);
        InitSlider(SliderType.Shield, entityModel.MaxShieldPoints);
        InitSlider(SliderType.Ammo, entityModel.MaxAmmo);

    }

    private void Update()
    {
        transform.position = entityModel.transform.position + offset;
    }

    public void SetName(string name)
    {
        entityName.text = name;
    }

    public void InitSlider(SliderType sliderType, float maxValue)
    {
        sliderDict[sliderType].maxValue = maxValue;
    }

    public void SetSliderValue(SliderType sliderType, float currentValue)
    {
        sliderDict[sliderType].value = currentValue;
    }
}
