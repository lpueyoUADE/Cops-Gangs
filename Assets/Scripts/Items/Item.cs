using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] ItemType type;
    [SerializeField] int cost;
    [SerializeField] int value;

    [Header("Audio")]
    [SerializeField] AudioClip collectedSound;
    [SerializeField] AudioClip cannotCollectSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RyderModel player))
        {
            if (player.ReceiveItemIfAble(type, cost, value))
                ItemPicked();
            else
                CannotPick();
        }
    }

    private void CannotPick()
    {
        if(cannotCollectSound != null)
            AudioSource.PlayClipAtPoint(cannotCollectSound, this.transform.position);
    }
    private void ItemPicked()
    {
        if (collectedSound != null)
            AudioSource.PlayClipAtPoint(collectedSound, this.transform.position);
        Destroy(this.gameObject);
    }
}
