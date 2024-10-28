using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityView : EntityBase
{
    [Header("Animator")]
    [SerializeField] protected Animator anim;

    [Header("HUD")]
    [SerializeField] private Transform canvas;
    [SerializeField] private HUDController HUD; 
    [SerializeField] private Vector3 offset;

    IAttack _attack;
    IReload _reload;
    IPain _pain;
    IDead _dead;

    private EntityModel entityModel;

    protected override void Awake()
    {
        base.Awake();
        _attack = GetComponent<IAttack>();
        _reload = GetComponent<IReload>();
        _pain = GetComponent<IPain>();
        _dead = GetComponent<IDead>();

        entityModel = GetComponent<EntityModel>();
    }

    void Start()
    {
        HUD.transform.SetParent(canvas);

        HUD.SetName(entityModel.EntityName);
        HUD.InitSlider(HUDController.SliderType.health, entityModel.MaxLifePoints);
        HUD.InitSlider(HUDController.SliderType.shield, entityModel.MaxShieldPoints);
        HUD.InitSlider(HUDController.SliderType.ammo, entityModel.MaxAmmo);

        HUD.setSliderValue(HUDController.SliderType.health, entityModel.currentLifePoints);
        HUD.setSliderValue(HUDController.SliderType.health, entityModel.currentShieldPoints);
        HUD.setSliderValue(HUDController.SliderType.health, entityModel.currentAmmo);
    }

    protected virtual void Update()
    {
        // HUD.transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position); // Look at the camera
        HUD.transform.position = this.transform.position + offset;

        anim.SetFloat("Velocity", new Vector3(Rb.velocity.x, 0, Rb.velocity.z).magnitude);
        anim.SetBool("IsAttacking", _attack.IsAttacking);
        anim.SetBool("IsReloading", _reload.IsReloading);
        anim.SetBool("IsInPain", _pain.IsInPain);
        anim.SetBool("IsDead", _dead.IsDead);

        HUD.setSliderValue(HUDController.SliderType.health, entityModel.currentLifePoints);
        HUD.setSliderValue(HUDController.SliderType.shield, entityModel.currentShieldPoints);
        HUD.setSliderValue(HUDController.SliderType.ammo, entityModel.currentAmmo);
    }
}
