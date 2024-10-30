using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderModel : EntityModel, IMoveMouse
{

    [SerializeField] private int currentMoney;
    public int CurrentMoney { get => currentMoney; set { currentMoney = value; ; OnMoneyAlteredAction?.Invoke(value); } }
    
    
    [Header("Raycast")]
    public LayerMask groundMask;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReceiveDamage(1);
        }

        //Debug.Log(currentMoney + " dollars");
        //Debug.Log(CurrentLifePoints);
    }
}
