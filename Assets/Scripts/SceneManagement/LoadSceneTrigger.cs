using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneTrigger : MonoBehaviour
{
    public int levelCompleteNumber = 0;
    // [SerializeField] private GameSceneSO _locationToLoad = default;
    [SerializeField] private GameSceneSO _menuToLoad = default;

    [Header("Broadcasting on")]
    // [SerializeField] private LoadEventChannelSO _locationLoadChannel = default;
    // [SerializeField] private IntEventChannelSO _levelCompletedChannel = default;
    [SerializeField] private GameStateSO _gameState = default;
    [SerializeField] private LoadEventChannelSO _menuLoadChannel = default;

    /*private void OnEnable()
    {
        _menuLoadChannel.OnLoadingRequested += LoadMenu;
    }*/

    /*public void LoadScene()
    {
        Debug.Log("Load scene!");
        _locationLoadChannel.RaiseEvent(_locationToLoad, false, false);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // _levelCompletedChannel.RaiseEvent(2);
            _gameState.latestCompletedLevel = levelCompleteNumber;
            _menuLoadChannel.RaiseEvent(_menuToLoad, false, false);
            // LoadScene();
        }
    }
}
