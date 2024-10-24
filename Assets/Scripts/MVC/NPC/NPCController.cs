using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCController<T> : EntityController<T> where T : Enum
{
    protected ITreeNode actionTreeRoot;

    protected override void Start()
    {
        base.Start();
        InitDecisionTree();
    }

    protected abstract void InitDecisionTree();
}
