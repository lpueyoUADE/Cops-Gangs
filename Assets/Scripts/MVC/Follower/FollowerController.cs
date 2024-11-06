using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class FollowerController : OffensiveNPCController<NPCStates>, IFlockingBehaviour
{
    private FollowerModel _model;
    public float multiplier;
    public float timePrediction;
    Pursuit pursuit;
    public Transform target;
    
    protected override void Awake()
    {
        base.Awake();
        _model = GetComponent<FollowerModel>();

        pursuit = new Pursuit(transform, null, timePrediction);
        SetTarget(target);
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

        // TODO Conectar el flocking
        var qLeaderInSight = new QuestionTree(() => false, follow, idle);
        var qCanAttack = new QuestionTree(() => _model.IsTargetInAttackRange(), attack, pursuit);
        var qAnyFoeInSightAlive = new QuestionTree(() => _model.DetectAliveFoes(), qCanAttack, qLeaderInSight);
        var qIsCurrentTargetSetAndAlive = new QuestionTree(() => _model.IsCurrentTargetSetAndAlive(), qCanAttack, qAnyFoeInSightAlive);
        var qINeedToReload = new QuestionTree(() => _model.NeedsToReload(), reload, qIsCurrentTargetSetAndAlive);
        var qIAmInPain = new QuestionTree(() => _model.IsInPain, pain, qINeedToReload);
        var qIAmReloading = new QuestionTree(() => _model.IsReloading, reload, qIAmInPain);
        var qIAmDead = new QuestionTree(() => _model.IsDead, dead, qIAmReloading); 

        actionTreeRoot = qIAmDead;   
    }

    protected override void GenerateStatesDictionary()
    {
        var idle = new NPCStateIdle(_move, _foeDetection);
        var follow = new NPCStateFollow(_move, _foeDetection);
        var pursuit = new NPCStatePursuit(_move, _foeDetection, transform, _model.TimePrediction);
        var attack = new NPCStateAttack(_moveNPC, _attack, _foeDetection);
        var reload = new NPCStateReload(_move, _reload, _foeDetection);
        var pain = new NPCStatePain(_move, _pain);
        var dead = new OffensiveNPCStateDead(_move, _foeDetection, _dead, _respawn, transform);

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
        print(fsm.GetCurrent);
    }

    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null) return;
        target = newTarget;

        var rb = target.GetComponent<Rigidbody>();
        pursuit.Target = rb;
    }

    public Vector3 GetDir(List<IBoid> boids, IBoid self)
    {
        if (target == null) return Vector3.zero;
        return pursuit.GetDir() * multiplier;
    }
}
