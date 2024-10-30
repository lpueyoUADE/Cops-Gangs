using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifetime;

    Rigidbody rb;
    Cooldown lifetimeCooldown;

    // Medio overkill pero bueh
    public Dictionary<string, HashSet<string>> AppliesDamageToTable = new Dictionary<string, HashSet<string>>
    {
        {"Player", new(){"Enemy"}},
        {"Gang", new(){"Enemy"}},
        {"Enemy", new(){"Player", "Gang"}}
    };

    public Vector3 Direction { get => transform.forward; set => transform.forward = value; }
    public string Owner { get => owner; set => owner = value; }
    public float Damage { get => damage; set => damage = value; }

    private string owner;
    private float damage;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lifetimeCooldown = new Cooldown(lifetime, Die);
        lifetimeCooldown.ResetCooldown();
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    private void ApplyDamage(Collider other)
    {
        if (AppliesDamageToTable[owner].Contains(other.tag))
        {
            EntityModel entityModel = other.GetComponent<EntityModel>();
            entityModel.ReceiveDamage(Damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "Player")
        {
            RemyModel remyModel = other.GetComponent<RemyModel>();
            remyModel.ChangeLife(-1);
        }
        */
        ApplyDamage(other);
        Die();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed *  Time.fixedDeltaTime;
        lifetimeCooldown.RunCooldown();
    }
}
