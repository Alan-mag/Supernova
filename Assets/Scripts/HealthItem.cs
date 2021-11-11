using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : UpgradeItem
{
    /*[Header("Health Recover")]
    public bool isHealthRecover*/
    [Range(0, 100)]
    public int amountRecover;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Need to give health to player.");
            if (other.TryGetComponent(out IUpgradeItem item))
                item.HealItem(amountRecover);

            TriggerEnter(other.transform);
            // _animtion.Play(clip.name);
        }
    }
}
