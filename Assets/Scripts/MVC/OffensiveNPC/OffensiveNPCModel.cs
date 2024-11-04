using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class OffensiveNPCModel : NPCModel, IRespawn
{
    [Header("Respawn")]
    [SerializeField] Transform respawnTransform;
    [SerializeField] float respawnTime;

    public Vector3 respawnPoint { get => respawnTransform.position; }
    public float RespawnTime { get => respawnTime; set => respawnTime = value; }

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
        Bc.size = originalBCSize;
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        // Attack Range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
