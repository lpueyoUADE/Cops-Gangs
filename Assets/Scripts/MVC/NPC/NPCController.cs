using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class NPCController<T> : EntityController<T> where T : Enum
{
    protected ITreeNode actionTreeRoot;
    protected LineOfSight lineOfSight;

    protected LayerMask foeMask;
    
    protected IFoeDetection _foeDetection;

    protected virtual void Awake()
    {
        lineOfSight = GetComponent<LineOfSight>();
        _foeDetection = GetComponent<IFoeDetection>();
    }

    protected override void Start()
    {
        base.Start();
        InitDecisionTree();
    }

    protected override void Update()
    {
        base.Update();
        actionTreeRoot.Execute();
    }

    protected abstract void InitDecisionTree();
}
