using UnityEngine;

public interface IFoeDetection
{
    public void DetectAliveFoes();

    public EntityModel Target {get;}

    public void SetTarget(EntityModel target);

    public void ClearTarget();
    
    public bool IsCurrentTargetAlive();

    public bool IsCurrentTargetInSight();
}
