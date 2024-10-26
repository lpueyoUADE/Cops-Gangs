using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderStateIdle : State<PlayerStates>
{
    FSM<PlayerStates> fsm;
    IMove move;

    private LayerMask groundMask;
    public RyderStateIdle(FSM<PlayerStates> fsm, IMove move, LayerMask groundMask)
    {
        this.fsm = fsm;
        this.move = move;
        this.groundMask = groundMask;
    }
    public override void Enter()
    {
        base.Enter();
        move.Move(Vector3.zero);
    }

    public override void FixedExecute()
    {
        base.FixedExecute();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask))
            move.Look(hit.point);

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        if (h != 0 || v != 0)
            fsm.Transition(PlayerStates.Run);

        if(Input.GetMouseButton(0))
            fsm.Transition(PlayerStates.Attack);
    }
}
