using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUtils
{
    /// <summary>
    /// Devuelve un numero aleatorio entre [min, max]
    /// </summary>
    /// <returns></returns>
    public static float Range(float min, float max)
    {
        return min + UnityEngine.Random.value * (max - min);
    }

    /// <summary>
    /// Ruleta de items con pesos.
    /// </summary>
    /// <typeparam name="T">Tipo del elemento a devolver.</typeparam>
    /// <param name="items">Diccionario de Elementos Tipo T con sus pesos. </param>
    /// <returns>Devuelve uno de los elementos aleatoriamente respentando los pesos. </returns>
    public static T Roulette<T>(Dictionary<T, float> items)
    {
        float total = 0;
        foreach (var item in items)
        {
            total += item.Value;
        }
        float random = UnityEngine.Random.Range(0, total);
        foreach (var item in items)
        {
            random -= item.Value;
            if (random <= 0)
            {
                return item.Key;
            }
        }
        //default(T)
        return default;
    }

    /// <summary>
    /// Baraja los elementos de lista de entrada.
    /// </summary>
    /// <typeparam name="T">Tipo de elementos de la lista</typeparam>
    /// <param name="items">Lista de elementos tipo T</param>
    /// <param name="onSwap"></param>
    public static void Shuffle<T>(List<T> items, Action<T, T> onSwap = null)
    {
        for (int i = 0; i < items.Count; i++)
        {
            int r = UnityEngine.Random.Range(i, items.Count);
            onSwap?.Invoke(items[i], items[r]);
            (items[i], items[r]) = (items[r], items[i]); // Swap
        }
    }
}
