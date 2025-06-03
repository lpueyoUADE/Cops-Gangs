using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicItemRoulette 
{
    [SerializeField] Dictionary<ItemType, float> itemWeights;

    [SerializeField] List<List<Vector3>> bilinearInterpolationMatrix;

    (float min, float max) shieldRange;
    (float min, float max) lifeRange;

    Func<float> ShieldGetter;
    Func<float> LifeGetter;

    /// <summary>
    /// Interpolaci�n bilineal R2 -> R3
    /// Dominio(datos del modelo normalizados)
    /// - (porcentaje de escudo, porcentaje de vida) : ([0, 1], [0, 1])
    /// Imagen(peso de items normalizados)
    /// - (Escudo, vida, dinero) : ([0, 1],[0, 1],[0, 1]) , x + y + z = 1
    /// </summary>
    /// <param name="items"></param>
    /// <param name="shieldRange"></param>
    /// <param name="lifeRange"></param>
    /// <param name="shieldGetter"></param>
    /// <param name="lifeGetter"></param>
    public DynamicItemRoulette(
        HashSet<ItemType> items, 
        (float shieldMin, float shieldMax ) shieldRange, 
        (float lifeMin, float lifeMax) lifeRange,
        Func<float> shieldGetter,
        Func<float> lifeGetter
        )
    {
        // Creo el diccionario de items y pesos.

        // Esta hardcodeadisimo para que la imagen de la interpolaci�n bilineal sea R3 (una dimension por item).
        // En algun momento se podr� implementar una versi�n generica. Por ahora vamos con esto.
        this.itemWeights = new()
        {
            {ItemType.Shield, 1/3},
            {ItemType.Life, 1/3},
            {ItemType.Money, 1/3},
        };

        this.shieldRange = shieldRange;
        this.lifeRange = lifeRange;
        this.ShieldGetter = shieldGetter;
        this.LifeGetter = lifeGetter;

        bilinearInterpolationMatrix = new()
        {
            new(){ new (0,1,0), new (1,0,0)},
            new(){ new (0,0.5f,0.5f), new (0,0,1)}
        };
    }

    private void CalculateDynamicWeights()
    {
        //Paso 1: Normalizo los parametros de entrada
        Vector2 input = new Vector2
        (
            Mathf.InverseLerp(shieldRange.min, shieldRange.max, ShieldGetter()),
            Mathf.InverseLerp(lifeRange.min, lifeRange.max, LifeGetter())
        );

        // Paso 2: Interpolaci�n en el eje x para cada par de puntos en y
        Vector3 fx_y0 = Vector3.Lerp(bilinearInterpolationMatrix[0][0], bilinearInterpolationMatrix[1][0], input.x);
        Vector3 fx_y1 = Vector3.Lerp(bilinearInterpolationMatrix[0][1], bilinearInterpolationMatrix[1][1], input.x);

        // Paso 3: Interpolaci�n en el eje y usando los resultados anteriores
        Vector3 f_xy = Vector3.Lerp(fx_y0, fx_y1, input.y);

        itemWeights[ItemType.Shield] = f_xy.x;
        itemWeights[ItemType.Life] = f_xy.y;
        itemWeights[ItemType.Money] = f_xy.z;

        UnityEngine.Debug.Log(f_xy);
    }

    public ItemType RollItem()
    {
        CalculateDynamicWeights();
        return RandomUtils.Roulette(itemWeights);
    }
}
