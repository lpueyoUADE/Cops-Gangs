using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderModel : EntityModel, IMoveMouse
{
    [Header("Raycast")]
    public LayerMask groundMask;

    protected override void Awake()
    {
        base.Awake();
        RyderAnimController.FinishedReloadAction += FinishedReloadActionHandler;
        RyderAnimController.FinishedPainAction += FinishedPainActionHandler;
    }

    private void OnDestroy()
    {
        RyderAnimController.FinishedReloadAction -= FinishedReloadActionHandler;
        RyderAnimController.FinishedPainAction -= FinishedPainActionHandler;
    }

    private void FinishedReloadActionHandler()
    {
        IsReloading = false;
    }

    private void FinishedPainActionHandler()
    {
        IsInPain = false;
    }

    public void LookAround()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask))
            Look(hit.point);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReceiveDamage(1);
        }
    }
}
