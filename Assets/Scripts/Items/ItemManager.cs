using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] List<ItemInfo> itemsToSpawn;
    [SerializeField] Transform[] spawnPoints;
    private float totalWeight = 0;

    private void Awake()
    {
        itemsToSpawn.Sort((a, b) => a.itemWeight.CompareTo(b.itemWeight));

        foreach(ItemInfo item in itemsToSpawn)
        {
            totalWeight += item.itemWeight;
        }
    }

    private void Start()
    {
        foreach(Transform location in spawnPoints)
        {
            var temp = RollItem();

            if (temp != null)
            {
                Instantiate(temp.gameObject, location);
            }
        }
    }

    private Item RollItem()
    {
        float weight = Random.Range(0, totalWeight);
        foreach (var item in itemsToSpawn)
        {
            weight -= item.itemWeight;

            if (weight <= 0 && item.item != null)
            {
                return item.item;
            }
        }

        return default;
    }   
}
