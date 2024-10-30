using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorItem : Item
{
    [SerializeField] private int armorToIncrease;
    
    private void Start()
    {
        OnCollected += IncreaseArmor;
    }

    private void IncreaseArmor(RyderModel player)
    {
        if (player.CurrentMoney >= 200)
        {
            player.CurrentMoney -= 200;
            player.CurrentShieldPoints += armorToIncrease;
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("No tienes dinero suficiente. Te faltan " + (200 - player.CurrentMoney) + " dolares");
        }
    }
}
