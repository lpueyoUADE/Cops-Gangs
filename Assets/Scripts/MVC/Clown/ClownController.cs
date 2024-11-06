using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownController : NPCController<NPCStates>
{
    private ClownModel _model;

    IRunningAway _runningAway;

    protected override void Awake()
    {
        base.Awake();
        _model = GetComponent<ClownModel>();
        _runningAway = GetComponent<IRunningAway>();

    }
    protected override void InitDecisionTree()
    {
        var idle = new ActionTree(() => fsm.Transition(NPCStates.Idle));
        var runningAway = new ActionTree(() => fsm.Transition(NPCStates.RunningAway));
        var pain = new ActionTree(() => fsm.Transition(NPCStates.Pain));
        var dead = new ActionTree(() => fsm.Transition(NPCStates.Dead));
    
        var qPlayerInSight = new QuestionTree(() => _model.IsCurrentTargetInSight(), runningAway, idle);
        var qIAmInPain = new QuestionTree(() => _model.IsInPain, pain, qPlayerInSight);
        var qIAmDead = new QuestionTree(() => _model.IsDead, dead, qIAmInPain);

        actionTreeRoot = qIAmDead;
    }

    protected override void GenerateStatesDictionary()
    {
        var idle = new NPCStateIdle(_move, _foeDetection);
        var runningAway = new NPCStateRunningAway(_move, _runningAway, transform, _model.Target.Rb, _model.TimePrediction);
        var pain = new NPCStatePain(_move, _pain);
        var dead = new NPCStateDead(_move);

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
