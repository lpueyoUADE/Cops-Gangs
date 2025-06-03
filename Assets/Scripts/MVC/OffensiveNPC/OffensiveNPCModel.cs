using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class OffensiveNPCModel : NPCModel, IMoveNPC, IRespawn
{
    [Header("Respawn")]
    [SerializeField] Transform respawnTransform;
    [SerializeField] float respawnTime;

    [Header("Accuracy")]
    [SerializeField] float accuracy;

    public Vector3 respawnPoint { get => respawnTransform.position; }
    public float RespawnTime { get => respawnTime; set => respawnTime = value; }
    public float Accuracy { get => accuracy; set => accuracy = value; }

    /// <summary>
    /// https://discussions.unity.com/t/target-movement-prediction-for-projectile/929069
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <param name="shooterPosition"></param>
    /// <param name="targetVelocity"></param>
    /// <param name="projectileSpeed"></param>
    /// <returns></returns>
    private Vector3 predictedPosition(Vector3 targetPosition, Vector3 shooterPosition, Vector3 targetVelocity, float projectileSpeed)
    {
        Vector3 displacement = targetPosition - shooterPosition;
        float targetMoveAngle = Vector3.Angle(-displacement, targetVelocity) * Mathf.Deg2Rad;
        //if the target is stopping or if it is impossible for the projectile to catch up with the target
        if (targetVelocity.magnitude == 0 || targetVelocity.magnitude > projectileSpeed && Mathf.Sin(targetMoveAngle) / projectileSpeed > Mathf.Cos(targetMoveAngle) / targetVelocity.magnitude)
        {
            return targetPosition;
        }
        float shootAngle = Mathf.Asin(Mathf.Sin(targetMoveAngle) * targetVelocity.magnitude / projectileSpeed);
        return targetPosition + targetVelocity * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / targetVelocity.magnitude;
    }


    public void AimAhead(Rigidbody TargetRb)
    {
        // Calculo a donde apuntar
        var predicted = predictedPosition(Target.Rb.position, transform.position, Target.Rb.velocity, Bullet.Speed);

        // Hago un random entre la posicion actual y la futura del target
        // teniendo en cuenta el accuarcy.

        var baseAccuracy = Mathf.Lerp(-1, 1, accuracy);
        var accuracyFactor = Mathf.Clamp01(Random.Range(baseAccuracy, baseAccuracy + 1));
        var lerpedPredicted = Vector3.Lerp(Target.Rb.position, predicted, accuracyFactor);

        Look((lerpedPredicted - transform.position).normalized);
    }

    public bool IsTargetInAttackRange()
    {
        return IsTargetSet() && (Target.transform.position - transform.position).magnitude <= AttackRange;
    }

    public void Respawn()
    {
        ReceiveLife(MaxLifePoints);
        ReceiveShield(MaxShieldPoints);
        RefillAmmo();
        transform.position = respawnPoint;
        Bc.enabled = true;
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        // Attack Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
