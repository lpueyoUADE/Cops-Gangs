using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRespawn
{
    public void Respawn();
    public Vector3 respawnPoint { get; }
    public float RespawnTime { get; set; }
}
