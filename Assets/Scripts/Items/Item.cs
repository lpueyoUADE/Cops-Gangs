using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    protected Action<RyderModel> OnCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RyderModel player))
        {           
            OnCollected(player);            
        }
    }
}
