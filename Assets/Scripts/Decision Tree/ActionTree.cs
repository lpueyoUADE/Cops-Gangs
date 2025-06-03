using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTree : ITreeNode
{
    Action action;

    public ActionTree(Action action)
    {
        this.action = action;
    }

    public void Execute()
    {
        action();
    }
}
