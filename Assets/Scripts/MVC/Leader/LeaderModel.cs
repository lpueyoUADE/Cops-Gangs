using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderModel : OffensiveNPCModel
{
    [SerializeField] Node _start;
    public Node Start { get { return _start; } }
    [SerializeField] Node _goal;
    public Node Goal { get { return _goal; } }

    [SerializeField] float idleTime;
    private float currentIdleTime;

    private bool _isIdle = false;
    public bool isIdle { get { return _isIdle; } }

    protected override void Update()
    {
        base.Update();
        // TODO: Remover
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReceiveDamage(1);
        }

        if (isIdle)
        {
            if(currentIdleTime < idleTime)
            {
                currentIdleTime += Time.deltaTime;
            }
            else
            {
                _isIdle = false;
                currentIdleTime = 0;
            }
        }

        //print("idle: " + isIdle);
    }

    public void StartIdle()
    {
        _isIdle = true;
    }
}
