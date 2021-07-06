using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleColliderCube : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] int damage;

    private ParticleSystem ps;
    private List<ParticleCollisionEvent> particle;

    private int numEnterParticle;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        particle = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        numEnterParticle = ps.GetCollisionEvents(other, particle);

        for (int i = 0; i < particle.Count; i++)
        {
            if (other.TryGetComponent(out IDamagable dmg))
            {
                dmg.SetDamage(damage);
                return;
            }

            // Vector3 collisionHitRot = particle[i].normal;
            // Quaternion hitRot = Quaternion.FromToRotation(Vector3.up, collisionHitRot);
            // ObjectPoolerSystem.SpawnFromPool("ParticleCollider", particle[i].intersection, hitRot);
        }
    }
}
