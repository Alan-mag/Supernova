using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponScript : MonoBehaviour
{
    [Header("Enemy Data")]
    public EnemyData data;

    [SerializeField] ParticleSystem projectileParticle;

    Transform target;

    void Awake()
    {
        target = FindObjectsOfType<CubeControllerBehaviour>()[0].transform;
    }

    // probably good to clean this up instead of handling it all in update
    // keep state for 'in firing range' and 'not in firing range'
    // setup coroutine or subscribed to change in state that fires projectiles
    // if state changes and the enemy is too far away to fire -- stop running coroutine
    void Update()
    {
        if (target && CheckWithinRange(gameObject.transform.position, target.transform.position))
        {
            FireWeapon();
        } else
        {
            projectileParticle.Stop();
        }
    }

    void FireWeapon()
    {
        gameObject.transform.LookAt(target);
        if (!projectileParticle.isPlaying)
            projectileParticle.Play();
    }

    private bool CheckWithinRange(Vector3 targetPosition, Vector3 currentPosition)
    {
        // Debug.Log(Vector3.Distance(targetPosition, currentPosition));
        /*Debug.Log("game obj z val " + gameObject.transform.position.z);
        Debug.Log("target z val " + target.transform.position.z);*/
        var enemyBehindCheck = gameObject.transform.position.z < target.transform.position.z;
        return Vector3.Distance(targetPosition, currentPosition) < data.attackRange && !enemyBehindCheck;
    }
}
