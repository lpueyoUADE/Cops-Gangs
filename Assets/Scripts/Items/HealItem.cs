using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : Item
{
    [SerializeField] private int lifeToIncrease;
    
    private void Start()
    {
        OnCollected += IncreaseLife;
    }

    private void IncreaseLife(RyderModel player)
    {
        if (player.CurrentMoney >= 100)
        {
            player.CurrentMoney -= 100;
            player.CurrentLifePoints += lifeToIncrease;
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("No tienes dinero suficiente. Te faltan " + (100 - player.CurrentMoney) + " dolares");
        }
    }
}
