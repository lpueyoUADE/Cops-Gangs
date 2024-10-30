using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyItem : Item
{
    [SerializeField] private int moneyToIncrease;
    
    private void Start()
    {
        OnCollected += IncreaseMoney;
    }

    private void IncreaseMoney(RyderModel player)
    {
        player.CurrentMoney += moneyToIncrease;
        Debug.Log("Has obtenido " + moneyToIncrease + " dolares. Actualmente tienes " + player.CurrentMoney + " dolares");
        
        Destroy(this.gameObject);
    }
}
