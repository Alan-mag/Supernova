using System;
using System.Linq;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [Header("Asset References")]
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] GameplayObject _playerPrefab = default;
    [SerializeField] private TransformAnchor _playerTransformAnchor = default;
    [SerializeField] private TransformEventChannelSO _playerInstantiatedChannel = default;

    [Header("Scene References")]
    private Transform _defaultSpawnPoint;

    [Header("Scene Ready Event")]
    [SerializeField] private VoidEventChannelSO _OnSceneReady = default;

    private void Awake()
    {
        _defaultSpawnPoint = transform.GetChild(0);
    }

    private void OnEnable()
    {
        _OnSceneReady.OnEventRaised += SpawnPlayer;
    }

    private void OnDisable()
    {
        _OnSceneReady.OnEventRaised -= SpawnPlayer;
    }

    private Transform GetSpawnLocation()
    {
        return _defaultSpawnPoint; // default spawn point
    }

    private GameplayObject InstantiatePlayer(GameplayObject playerPrefab, Transform spawnLocation)
    {
        if (playerPrefab == null)
            throw new Exception("Player prefab can't be null.");

        GameplayObject playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);

        return playerInstance;
    }

    private void SpawnPlayer()
    {
        GameplayObject playerInstance = InstantiatePlayer(_playerPrefab, GetSpawnLocation());

        _playerInstantiatedChannel.RaiseEvent(playerInstance.transform);
        _playerTransformAnchor.Transform = playerInstance.transform;

        _inputReader.EnableGameplayInput();
    }
}
