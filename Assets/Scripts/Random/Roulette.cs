using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Roulette<T>
{
    Dictionary<T, float> items;

    public Roulette(Dictionary<T, float> items)
    {
        Assert.IsNotNull(items);
        Assert.IsTrue(items.Count > 0);

        this.items = items;
    }

    /// <summary>
    /// Crea una nueva ruleta con el mismo peso para todos los items.
    /// </summary>
    /// <param name="items">Hashset de items. Debe tener al menos 1 elemento.</param>
    public Roulette(HashSet<T> items)
    {
        Assert.IsNotNull(items);
        Assert.IsTrue(items.Count > 0);

        this.items = new();

        foreach (var item in items)
        {
            this.items.Add(item, 1 / items.Count);
        }
    }

    public virtual T RollItem()
    {
        return RandomUtils.Roulette(items);
    }
}
