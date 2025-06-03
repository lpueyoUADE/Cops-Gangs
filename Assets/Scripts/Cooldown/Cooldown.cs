using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown
{
    float _initTimer;
    Action _onFinishCooldown;

    bool _executedFinishAction;

    float _lastInterval;

    public float TimeElapsed
    {
        get
        {
            return Time.realtimeSinceStartup - _lastInterval;
        }
    }

    public Cooldown(float timer = 1, Action onFinishCooldown = null)
    {
        _initTimer = timer;
        _onFinishCooldown = onFinishCooldown;
        _executedFinishAction = true;
        _lastInterval = -1;
    }
    /// <summary>
    /// Reinicia el cooldown.
    /// </summary>
    public void ResetCooldown()
    {
        _executedFinishAction = false;
        _lastInterval = Time.realtimeSinceStartup;
    }
    /// <summary>
    /// Devuelve true mientras el tiempo siga corriendo.
    /// Cuando el timer se agota, IsCoolDown devuelve false.
    /// Si el cooldown tiene onFinishCooldown seteado, se ejecuta al agotar el timer.
    /// </summary>
    /// <returns></returns>
    public bool IsCooldown()
    { 
        RunCooldown();
        return _lastInterval != -1 && TimeElapsed < _initTimer;
    }

    /// <summary>
    /// Valida si el cooldown terminó.
    /// En caso afirmativo ejecuta la acción de fin de cooldown.
    /// El reloj del cooldown sigue contando aunque no se llame a esta función.
    /// </summary>
    public void RunCooldown()
    {
        if (TimeElapsed >= _initTimer && _onFinishCooldown != null && !_executedFinishAction)
        {
            _onFinishCooldown();
            _executedFinishAction = true;
        }
    }
    public Action OnFinishCooldown
    {
        get
        {
            return _onFinishCooldown;
        }
        set
        {
            _onFinishCooldown = value;
        }
    }
}
