using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponScript : MonoBehaviour
{
    [Header("Enemy Data")]
    public EnemyData data;

    [SerializeField] ParticleSystem projectileParticle;

    [SerializeField] private TransformEventChannelSO _playerInstantiatedChannel = default;

    Transform target;

    void Awake()
    {
        // todo: update
        // cubecontroller isn't intialized by this time
        // i should probably be listening to the event that gets called
        // after spawning happenens, then do this.
    }
    
    void OnEnable()
    {
        // _playerInstantiatedChannel.OnEventRaised += AquirePlayerTarget;
        // todo: somethign is broken here
        /*if (FindObjectsOfType<CubeControllerBehaviour>()[0] != null)
        {
            Debug.Log(FindObjectsOfType<CubeControllerBehaviour>()[0]);
            target = FindObjectsOfType<CubeControllerBehaviour>()[0].transform;
        }*/
    }

    void OnDisable()
    {
        // _playerInstantiatedChannel.OnEventRaised -= AquirePlayerTarget;
    }

    // probably good to clean this up instead of handling it all in update
    // keep state for 'in firing range' and 'not in firing range'
    // setup coroutine or subscribed to change in state that fires projectiles
    // if state changes and the enemy is too far away to fire -- stop running coroutine
    void Update()
    {
        // todo: expensive calls here - see if you can do this another way
        if (FindObjectsOfType<CubeControllerBehaviour>() != null && FindObjectsOfType<CubeControllerBehaviour>().Length > 0)
            target = FindObjectsOfType<CubeControllerBehaviour>()[0].transform;
        if (target && projectileParticle &&  CheckWithinRange(gameObject.transform.position, target.transform.position))
        {
            FireWeapon();
        } else
        {
            if (projectileParticle)
                projectileParticle.Stop();
        }
    }
     
    void FireWeapon()
    {
        gameObject.transform.LookAt(target);
        if (!projectileParticle.isPlaying)
            projectileParticle.Play();
    }

    private void AquirePlayerTarget(Transform playerTransform)
    {
        // target = FindObjectsOfType<CubeControllerBehaviour>()[0].transform;
        // Debug.Log("target: " + target);
        // target = playerTransform;
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
