using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneTrigger : MonoBehaviour
{
    [SerializeField] private GameSceneSO _locationToLoad = default;

    [Header("Broadcasting on")]
    [SerializeField] private LoadEventChannelSO _locationLoadChannel = default;

    public void LoadScene()
    {
        Debug.Log("Load scene!");
        _locationLoadChannel.RaiseEvent(_locationToLoad, false, false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadScene();
        }
    }
}
