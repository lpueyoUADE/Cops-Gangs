using System;
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

    [Header("Personal Space Range")]
    [SerializeField] float personalSpaceRange; // Si un enemigo está en este rango lo ve aunque esté a sus espaldas.
    [SerializeField] float enlargedPersonalSpaceRangeTime;

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
    private Cooldown enlargedPersonalSpaceRangeCooldown;

    private LayerMask foeMask;

    private float initialPersonalSpaceRange;
    private float enlargedPersonalSpaceRange;

    public Transform EyeSight { get => eyeSight; set => eyeSight = value; }
    public float LineOfSightGraceTime { get => lineOfSightGraceTime; set => lineOfSightGraceTime = value; }
    public EntityModel Target { get => target; }
    public float TimePrediction { get => timePrediction; set => timePrediction = value; }

    protected override void Awake()
    {
        base.Awake();
        obstacleAvoidance = new ObstacleAvoidance(transform, radius, angle, personalArea, obsMask);
        lineOfSight = GetComponent<LineOfSight>();
        graceTimeCooldown = new Cooldown(LineOfSightGraceTime);
        enlargedPersonalSpaceRangeCooldown = new Cooldown(enlargedPersonalSpaceRangeTime, ResetPersonalSpaceRange);
        initialPersonalSpaceRange = personalSpaceRange;
        enlargedPersonalSpaceRange = personalSpaceRange * 5;
        GetMyEnemiesLayer();
    }

    public override void Move(Vector3 dir)
    {
        Vector3 obsDir = obstacleAvoidance.GetDir(dir, false);
        obsDir.y = 0;
        base.Move(obsDir);
        Look(obsDir);
    }

    public override void ReceiveDamage(float amount)
    {
        base.ReceiveDamage(amount);
        EnlargePersonalSpaceRange();
    }

    private void ResetPersonalSpaceRange()
    {
        personalSpaceRange = initialPersonalSpaceRange;
    }
    private void EnlargePersonalSpaceRange()
    {
        enlargedPersonalSpaceRangeCooldown.ResetCooldown();
        personalSpaceRange = enlargedPersonalSpaceRange; 
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
    /// <summary>
    /// Busca todas las entidades enemigas, de aquellas que estén vivas y, muy cerca o a la vista
    /// elije 1 al azar como objetivo.
    /// Devuelve true si encuentra y setea un nuevo target, false caso contrario.
    /// </summary>
    /// <returns></returns>
    public bool DetectAliveFoes()
    {
        if (IsTargetSet())
            return true;

        var closeFoesCount = lineOfSight.GetEntitiesInRange(transform.position, foeMask);
        List<EntityModel> entitiesInSight = new();

        for (int i = 0; i < closeFoesCount; i++)
        {
            var entityModel = lineOfSight.EntitiesInRange[i].gameObject.GetComponent<EntityModel>();

            if (entityModel.IsAlive && ((entityModel.transform.position - transform.position).magnitude <= personalSpaceRange || lineOfSight.InSight(entityModel.transform)))
                entitiesInSight.Add(entityModel);
        }

        if (entitiesInSight.Count > 0)
        {
            SetTarget(entitiesInSight[UnityEngine.Random.Range(0, entitiesInSight.Count)]);
            return true;
        }

        return false;
    }

    public bool IsTargetSet()
    {
        return target != null;
    }
    public bool IsCurrentTargetSetAndAlive()
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
        bool InSightAndInRangeAndWithinAngle = lineOfSight.InSight(target.transform);

        if (InSightAndInRangeAndWithinAngle)
            graceTimeCooldown.ResetCooldown();

        return graceTimeCooldown.IsCooldown() || InSightAndInRangeAndWithinAngle;
    }

    protected virtual void Update()
    {
        enlargedPersonalSpaceRangeCooldown.IsCooldown();
    }

    protected virtual void OnDrawGizmosSelected()
    {
        // Personal Area Range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, personalSpaceRange);
    }
}
