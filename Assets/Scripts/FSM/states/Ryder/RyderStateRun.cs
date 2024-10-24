using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyderStateRun : State<PlayerStates>
{
    FSM<PlayerStates> fsm;
    IMove move;
    public RyderStateRun(FSM<PlayerStates> fsm, IMove move)
    {
        this.fsm = fsm;
        this.move = move;

    }
    public override void FixedExecute()
    {
        base.FixedExecute();

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        bool isInputACtive = Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;

        if (h == 0 && v == 0)
        {
            fsm.Transition(PlayerStates.Idle);
        }
        else
        {
            // Forma cabezona para que tenga el smooth del GetAxis 
            // pero que al moverse en diagonal no se mueva mas rapido.

            if (h != 0 && v != 0)
            {
                h *= 0.70710678f;
                v *= 0.70710678f;
            }

            Vector3 dir = new Vector3(h, 0, v);

            move.Move(dir);

            if (isInputACtive)
                move.Look(dir);
        }
    }
}
