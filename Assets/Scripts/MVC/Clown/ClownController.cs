using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownController : NPCController<NPCStates>
{
    private FollowerModel _model;

    private void Awake()
    {
        _model = GetComponent<FollowerModel>();
    }
    protected override void InitDecisionTree()
    {

        var idle = new ActionTree(() => fsm.Transition(NPCStates.Idle));
        var runningAway = new ActionTree(() => fsm.Transition(NPCStates.RunningAway));
        var pain = new ActionTree(() => fsm.Transition(NPCStates.Pain));
        var dead = new ActionTree(() => fsm.Transition(NPCStates.Dead));
    
        var qPlayerInSight = new QuestionTree(() => false, runningAway, idle);
        var qIAmInPain = new QuestionTree(() => _model.IsInPain, pain, qPlayerInSight);
        var qIAmDead = new QuestionTree(() => _model.IsDead, dead, qIAmInPain); 

        actionTreeRoot = qIAmDead;
    }

    protected override void GenerateStatesDictionary()
    {
        var idle = new NPCStateIdle(_move);
        var runningAway = new NPCStateRunningAway();
        var pain = new NPCStatePain(_move, _pain);
        var dead = new NPCStateDead(_move, _dead);

        statesDict = new()
        {
            { NPCStates.Idle, idle },
            { NPCStates.RunningAway, runningAway },
            { NPCStates.Pain, pain },
            { NPCStates.Dead, dead }
        };
    }

    protected override void SetInitialState()
    {
        fsm.SetInitial(statesDict[NPCStates.Idle]);
    }
    override protected void Update()
    {
        base.Update();
        print(fsm.GetCurrent);
    }
}
