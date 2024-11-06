using UnityEngine;

public interface IMoveNPC : IMove
{
    public void AimAhead(Rigidbody TargetRb);

    public float Accuracy { get; set; }
}
