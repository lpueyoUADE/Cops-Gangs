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
    public void ResetCooldown()
    {
        _executedFinishAction = false;
        _lastInterval = Time.realtimeSinceStartup;
    }
    public bool IsCooldown()
    {
        // IsCooldown devuelve true mientras el tiempo siga corriendo.
        // Cuando el timer se agota, IsCoolDown devuelve false.
        RunCooldown();
        return _lastInterval != -1 && TimeElapsed < _initTimer;
    }
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
