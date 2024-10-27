using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderAnimController : MonoBehaviour
{
    public static Action FinishedReloadAction;
    public static Action FinishedPainAction;
    public bool FinishedReloading()
    {
        FinishedReloadAction?.Invoke();
        return true;
    }

    public bool FinishedPain()
    {
        FinishedPainAction?.Invoke();
        return true;
    }
}
