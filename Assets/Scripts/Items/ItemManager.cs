using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    private DynamicItemRoulette _roulette;
    [SerializeField] private RyderModel pModel;
    [SerializeField] private GameObject armorItem;
    [SerializeField] private GameObject lifeItem;
    [SerializeField] private GameObject moneyItem;
    
    
    private Dictionary<ItemType, GameObject> items;
    private void Start()
    {
        items = new Dictionary<ItemType, GameObject>(){
            { ItemType.Shield, armorItem },
            { ItemType.Life, lifeItem },
            { ItemType.Money, moneyItem }};
        
        _roulette = new(
            new()
            {
                { ItemType.Shield },
                { ItemType.Life },
                { ItemType.Money }
            },
            (0, pModel.MaxShieldPoints),
            (0, pModel.MaxLifePoints),
            () => { return pModel.CurrentShieldPoints; },
            () => { return pModel.CurrentLifePoints; }
        );
        RollItem();
    }

    private void RollItem()
    {
        foreach(Transform location in spawnPoints)
        {
            var itemType = _roulette.RollItem();
            
            Instantiate(items[itemType], location);
            
        }
    }

    
}
