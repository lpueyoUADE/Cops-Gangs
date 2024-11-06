using UnityEngine;

public interface IFoeDetection
{
    public bool DetectAliveFoes();

    public EntityModel Target {get;}

    public void SetTarget(EntityModel target);

    public void ClearTarget();
    
    public bool IsCurrentTargetSetAndAlive();

    public bool IsCurrentTargetInSight();
}
