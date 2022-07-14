using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemyScript = other.gameObject.GetComponentInParent<Enemy>();
            if (enemyScript != null && enemyScript.destroyableByKillZone)
                Destroy(other.gameObject);
        }
    }
}
