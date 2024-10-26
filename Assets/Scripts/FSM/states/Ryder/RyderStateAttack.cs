using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderStateAttack : State<PlayerStates>
{
    FSM<PlayerStates> fsm;
    IMove move;
    IAttack attack;
    LayerMask groundMask;

    public RyderStateAttack(FSM<PlayerStates> fsm, IMove move, IAttack attack, LayerMask groundMask)
    {
        this.fsm = fsm;
        this.move = move;
        this.attack = attack;
        this.groundMask = groundMask;
    }
    public override void Enter()
    {
        base.Enter();
        attack.Attack();
        move.Move(Vector3.zero);
    }

    public override void Execute()
    {
        base.Execute();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask))
            move.Look(hit.point);

        if (!Input.GetMouseButton(0))
        {
            attack.IsAttacking = false;
            fsm.Transition(PlayerStates.Idle);
        }
    }
}
