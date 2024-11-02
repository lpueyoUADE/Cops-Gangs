using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.UIElements;

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

    [Header("World Canvas Tag")]
    [SerializeField] string worldCanvasTagName;

    [Header("Entity")]
    [SerializeField] EntityModel entityModel;

    Transform canvas;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag(worldCanvasTagName).transform;

        sliderDict = new Dictionary<SliderType, Slider>()
        {
            { SliderType.Life, lifeSlider},
            { SliderType.Shield, shieldSlider},
            { SliderType.Ammo, ammoSlider},
        };

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
        transform.SetParent(canvas);

        InitSlider(SliderType.Life, entityModel.MaxLifePoints);
        InitSlider(SliderType.Shield, entityModel.MaxShieldPoints);
        InitSlider(SliderType.Ammo, entityModel.MaxAmmo);

    }

    private void Update()
    {
        transform.position = entityModel.transform.position + offset;
        // transform.LookAt(Camera.main.transform);
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
