using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCModel : EntityModel
{
    [Header("Eye Sight")]
    [SerializeField] Transform eyeSight;
    [SerializeField] float lineOfSightGraceTime;

    public Transform EyeSight { get => eyeSight; set => eyeSight = value; }
    public float LineOfSightGraceTime { get => lineOfSightGraceTime; set => lineOfSightGraceTime = value; }

    public override void Move(Vector3 dir)
    {
        base.Move(dir);
        Look(dir);
    }
}
