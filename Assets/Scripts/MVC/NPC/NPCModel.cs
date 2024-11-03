using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class NPCModel : EntityModel, IFoeDetection
{
    [Header("Eye Sight")]
    [SerializeField] Transform eyeSight;
    [SerializeField] float lineOfSightGraceTime;

    [Header("Attack")]
    [SerializeField] float attackRange;

    [Header("Obstacle Avoidance")]
    [SerializeField] float radius;
    [SerializeField] float angle;
    [SerializeField] float personalArea;
    [SerializeField] LayerMask obsMask;
    [SerializeField] float timePrediction;

    EntityModel target;
    ObstacleAvoidance obstacleAvoidance;
    protected LineOfSight lineOfSight;

    protected Cooldown graceTimeCooldown;

    private LayerMask foeMask;
    private int aliveFoesCount;

    public Transform EyeSight { get => eyeSight; set => eyeSight = value; }
    public float LineOfSightGraceTime { get => lineOfSightGraceTime; set => lineOfSightGraceTime = value; }
    public EntityModel Target { get => target; }
    public float AttackRange { get => attackRange; set => attackRange = value; }
    public float TimePrediction { get => timePrediction; set => timePrediction = value; }

    protected override void Awake()
    {
        base.Awake();
        obstacleAvoidance = new ObstacleAvoidance(transform, radius, angle, personalArea, obsMask);
        lineOfSight = GetComponent<LineOfSight>();
        graceTimeCooldown = new Cooldown(LineOfSightGraceTime);

        GetMyEnemiesLayer();
    }

    public override void Move(Vector3 dir)
    {
        Vector3 obsDir = obstacleAvoidance.GetDir(dir, false);
        obsDir.y = 0;
        base.Move(obsDir);
        Look(obsDir);
    }

    private void GetMyEnemiesLayer()
    {
        Assert.IsTrue(
            gameObject.layer == LayerMask.NameToLayer("Police") ||
            gameObject.layer == LayerMask.NameToLayer("Gangster") ||
            gameObject.layer == LayerMask.NameToLayer("Clown")
            ); // Me aseguro que el npc tenga una layer valida;

        foeMask =  
            (
                gameObject.layer == LayerMask.NameToLayer("Police") ||
                gameObject.layer == LayerMask.NameToLayer("Clown")
            ) ? 
    
            LayerMask.NameToLayer("Gangster") : 
            LayerMask.NameToLayer("Police");
    }
    public void DetectAliveFoes()
    {
        if (IsTargetSet())
            return;

        aliveFoesCount = lineOfSight.GetEntitiesInSight(transform.position, foeMask);

        if (aliveFoesCount > 0)
        {
            SetTarget(lineOfSight.EntitiesInSight[Random.Range(0, aliveFoesCount)].gameObject.GetComponent<EntityModel>());
        }
    }

    public bool IsAnyFoeInSightAlive()
    {
        return aliveFoesCount > 0;
    }

    public bool IsTargetSet()
    {
        return target != null;
    }
    public bool IsCurrentTargetAlive()
    {
        return IsTargetSet() && target.IsAlive;
    }
    public void SetTarget(EntityModel target)
    {
        this.target = target;
    }

    public void ClearTarget()
    {
        this.target = null;
    }

    public bool IsCurrentTargetInSight()
    {
        bool InSightAndInRangeAndWithinAngle =
            lineOfSight.InView(target.transform) &&
            lineOfSight.CheckRange(target.transform) &&
            lineOfSight.CheckAngle(target.transform);

        if (InSightAndInRangeAndWithinAngle)
            graceTimeCooldown.ResetCooldown();

        return graceTimeCooldown.IsCooldown() || InSightAndInRangeAndWithinAngle;
    }

    public bool IsTargetInAttackRange()
    {
        return IsTargetSet() && (target.transform.position - transform.position).magnitude <= attackRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
