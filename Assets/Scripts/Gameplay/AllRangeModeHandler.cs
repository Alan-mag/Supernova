using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllRangeModeHandler : MonoBehaviour
{
    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO _allRangeModeChannel;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _allRangeModeChannel.RaiseEvent();
        }
    }
}
