using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderModel : EntityModel, IMoveMouse
{
    [Header("Money")]
    [SerializeField] private int currentMoney;
    public int CurrentMoney { get => currentMoney; set { currentMoney = value; ; OnMoneyAlteredAction?.Invoke(value); } }
    
    
    [Header("Raycast")]
    public LayerMask groundMask;

    public static Action OnPlayerDeadAction;
    public override void Look(Vector3 dir)
    {
        dir = dir - transform.position;
        base.Look(dir);
    }

    public void LookAround()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask))
            Look(hit.point);
    }

    public override void Die()
    {
        base.Die();
        OnPlayerDeadAction?.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReceiveDamage(1);
        }

        // TODO: Quitar este test
        // Test
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DynamicItemRoulette dynamicRoulette = new(
                new()
                {
                    {ItemType.Shield},
                    {ItemType.Life},
                    {ItemType.Money}
                },
                (0, MaxShieldPoints),
                (0, MaxLifePoints),
                () => { return CurrentShieldPoints; },
                () => { return CurrentLifePoints; }
            );

            for (int i = 0; i < 1000; i++)
            {
                print(dynamicRoulette.RollItem());
            }
        }

        //Debug.Log(currentMoney + " dollars");
        //Debug.Log(CurrentLifePoints);
    }
}
