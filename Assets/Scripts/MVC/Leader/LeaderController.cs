using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderController : OffensiveNPCController<NPCStates>
{
    private LeaderModel _model;

    protected override void Awake()
    {
        base.Awake();
        _model = GetComponent<LeaderModel>();
    }
    protected override void InitDecisionTree()
    {
        var idle = new ActionTree(() => fsm.Transition(NPCStates.Idle));
        var patrol = new ActionTree(() => fsm.Transition(NPCStates.Patrol));
        var pursuit = new ActionTree(() => fsm.Transition(NPCStates.Pursuit));
        var attack = new ActionTree(() => fsm.Transition(NPCStates.Attack));
        var reload = new ActionTree(() => fsm.Transition(NPCStates.Reload));
        var pain = new ActionTree(() => fsm.Transition(NPCStates.Pain));
        var dead = new ActionTree(() => fsm.Transition(NPCStates.Dead));
        
        var qIsPatrolTime = new QuestionTree(() => false, patrol, idle);
        var qCanAttack = new QuestionTree(() => true, attack, pursuit);
        var qAnyEnemyAlive = new QuestionTree(() => true, qCanAttack, qIsPatrolTime);
        var qEnemyInSight = new QuestionTree(() => true, qAnyEnemyAlive, qIsPatrolTime);
        var qINeedToReload = new QuestionTree(() => _model.NeedsToReload(), reload, qEnemyInSight);
        var qIAmInPain = new QuestionTree(() => _model.IsInPain, pain, qINeedToReload);
        var qIAmReloading = new QuestionTree(() => _model.IsReloading, reload, qIAmInPain);
        var qIAmDead = new QuestionTree(() => _model.IsDead, dead, qIAmReloading);
        
        actionTreeRoot = qIAmDead;
    }
    protected override void GenerateStatesDictionary()
    {
        var idle = new NPCStateIdle(_move, _foeDetection);
        var patrol = new NPCStatePatrol(_move, _foeDetection);
        var pursuit = new NPCStatePursuit(_move, _foeDetection, transform, _model.TimePrediction);
        var attack = new NPCStateAttack(_move, _attack, _foeDetection, transform, _model.TimePrediction);
        var reload = new NPCStateReload(_move, _reload, _foeDetection, transform, _model.TimePrediction);
        var pain = new NPCStatePain(_move, _pain);
        var dead = new OffensiveNPCStateDead(_move, _dead, _respawn);

        statesDict = new()
        {
            { NPCStates.Idle, idle },
            { NPCStates.Patrol, patrol },
            { NPCStates.Pursuit, pursuit },
            { NPCStates.Attack, attack },
            { NPCStates.Reload, reload },
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

        // TODO: Quitar en todos los controllers
        // print(fsm.GetCurrent);
    }
}
