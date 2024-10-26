using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderStateRun : State<PlayerStates>
{
    FSM<PlayerStates> fsm;
    IMove move;

    private LayerMask groundMask;

    public RyderStateRun(FSM<PlayerStates> fsm, IMove move, LayerMask groundMask)
    {
        this.fsm = fsm;
        this.move = move;
        this.groundMask = groundMask;
    }
    public override void Execute()
    {
        base.Execute();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundMask))
        {
            move.Look(hit.point);
        }
    }
    public override void FixedExecute()
    {
        base.FixedExecute();

        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        if (h == 0 && v == 0)
        {
            fsm.Transition(PlayerStates.Idle);
        }
        else
        {
            Vector3 dir = new Vector3(h, 0, v).normalized;

            move.Move(dir);

            if (Input.GetMouseButton(0))
            {
                fsm.Transition(PlayerStates.Attack);
            }
        }
    }
}
