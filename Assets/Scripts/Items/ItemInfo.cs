using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Info", menuName = "Data/Item")]
public class ItemInfo : ScriptableObject
{
    [SerializeField] private Item _item;
    public Item item { get { return _item; } }

    [SerializeField] private float _weight;

    public float itemWeight { get { return _weight; } }

}
