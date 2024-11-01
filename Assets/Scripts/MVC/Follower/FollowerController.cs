using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowerController : NPCController<NPCStates>
{
    private FollowerModel _model;

    private void Awake()
    {
        _model = GetComponent<FollowerModel>();
    }
    protected override void InitDecisionTree()
    {
        var idle = new ActionTree(() => fsm.Transition(NPCStates.Idle));
        var follow = new ActionTree(() => fsm.Transition(NPCStates.Follow));
        var pursuit = new ActionTree(() => fsm.Transition(NPCStates.Pursuit));
        var attack = new ActionTree(() => fsm.Transition(NPCStates.Attack));
        var reload = new ActionTree(() => fsm.Transition(NPCStates.Reload));
        var pain = new ActionTree(() => fsm.Transition(NPCStates.Pain));
        var dead = new ActionTree(() => fsm.Transition(NPCStates.Dead));

        var qLeaderInSight = new QuestionTree(() => false, follow, idle);
        var qCanAttack = new QuestionTree(() => true, attack, pursuit);
        var qAnyEnemyAlive = new QuestionTree(() => true, qCanAttack, qLeaderInSight);
        var qEnemiesInSight = new QuestionTree(() => true, qAnyEnemyAlive, qLeaderInSight);
        var qINeedToReload = new QuestionTree(() => _model.NeedsToReload(), reload, qEnemiesInSight);
        var qIAmInPain = new QuestionTree(() => _model.IsInPain, pain, qINeedToReload);
        var qIAmReloading = new QuestionTree(() => _model.IsReloading, reload, qIAmInPain);
        var qIAmDead = new QuestionTree(() => _model.IsDead, dead, qIAmReloading); 

        actionTreeRoot = qIAmDead;   
    }

    protected override void GenerateStatesDictionary()
    {
        var idle = new NPCStateIdle(_move);
        var follow = new NPCStateFollow(_move);
        var pursuit = new NPCStatePursuit(_move);
        var attack = new NPCStateAttack(_attack);
        var reload = new NPCStateReload(_reload);
        var pain = new NPCStatePain(_move, _pain);
        var dead = new NPCStateDead(_move, _dead);

        statesDict = new()
        {
            { NPCStates.Idle, idle },
            { NPCStates.Follow, follow },
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
        // TODO: Borrar
        // print(fsm.GetCurrent);
    }
}
