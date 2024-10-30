using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowerController : NPCController<FollowerStates>
{
    private FollowerModel _model;

    private void Awake()
    {
        _model = GetComponent<FollowerModel>();
    }
    protected override void InitDecisionTree()
    {
        
        var idle = new ActionTree(() => fsm.Transition(FollowerStates.Idle));
        var follow = new ActionTree(() => fsm.Transition(FollowerStates.Follow));
        var attack = new ActionTree(() => fsm.Transition(FollowerStates.Attack));
        var reload = new ActionTree(() => fsm.Transition(FollowerStates.Reload));
        var pain = new ActionTree(() => fsm.Transition(FollowerStates.Pain));
        var dead = new ActionTree(() => fsm.Transition(FollowerStates.Dead));

        var qPlayerInSight = new QuestionTree(() => false, follow, idle);
        var qEnemyInSight = new QuestionTree(() => true, attack, qPlayerInSight);
        var qINeedToReload = new QuestionTree(() => _model.NeedsToReload(), reload, qEnemyInSight);
        var qIAmInPain = new QuestionTree(() => _model.IsInPain, pain, qINeedToReload);
        var qIAmReloading = new QuestionTree(() => _model.IsReloading, reload, qIAmInPain);
        var qIAmDead = new QuestionTree(() => _model.IsDead, dead, qIAmReloading); 

        actionTreeRoot = qIAmDead;
        
    }

    protected override void GenerateStatesDictionary()
    {
        var idle = new NPCStateIdle(_move);
        var follow = new NPCStateFollow(_move);
        var attack = new NPCStateAttack(_attack);
        var reload = new NPCStateReload(_reload);
        var pain = new NPCStatePain(_move, _pain);
        var dead = new NPCStateDead(_move, _dead);

        statesDict = new()
        {
            { FollowerStates.Idle, idle },
            { FollowerStates.Follow, follow },
            { FollowerStates.Attack, attack },
            { FollowerStates.Reload, reload },
            { FollowerStates.Pain, pain },
            { FollowerStates.Dead, dead }
        };
    }

    protected override void SetInitialState()
    {
         fsm.SetInitial(statesDict[FollowerStates.Idle]);
    }
    override protected void Update()
    {
        base.Update();
        print(fsm.GetCurrent);
    }
}
