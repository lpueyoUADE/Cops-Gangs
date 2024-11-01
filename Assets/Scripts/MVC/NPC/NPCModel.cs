using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCModel : EntityModel
{
    [Header("Eye Sight")]
    [SerializeField] Transform eyeSight;
    [SerializeField] float lineOfSightGraceTime;

    [Header("Obstacle Avoidance")]
    [SerializeField] float radius;
    [SerializeField] float angle;
    [SerializeField] float personalArea;
    [SerializeField] LayerMask obsMask;
    [SerializeField] float timePrediction;

    EntityModel target;
    ObstacleAvoidance obstacleAvoidance;

    public Transform EyeSight { get => eyeSight; set => eyeSight = value; }
    public float LineOfSightGraceTime { get => lineOfSightGraceTime; set => lineOfSightGraceTime = value; }
    public EntityModel Target { get => target; set => target = value; }

    protected override void Awake()
    {
        base.Awake();
        obstacleAvoidance = new ObstacleAvoidance(transform, radius, angle, personalArea, obsMask);
    }

    public override void Move(Vector3 dir)
    {
        Vector3 obsDir = obstacleAvoidance.GetDir(dir, false);
        obsDir.y = 0;
        base.Move(obsDir);
        Look(obsDir);
    }
}
