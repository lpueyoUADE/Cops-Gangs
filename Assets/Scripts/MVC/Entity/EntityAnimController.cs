using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimController : MonoBehaviour
{
    public Action FinishedReloadAction;
    public Action FinishedPainAction;
    public Action StepAction;
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

    public bool Step()
    {
        StepAction?.Invoke();
        return true;
    }
}
