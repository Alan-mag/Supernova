using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameSceneSO : DescriptionBaseSO
{
    public GameSceneType sceneType;
    public AssetReference sceneReference;
    public AudioCueSO musicTrack;

    public enum GameSceneType
    {
        Location,
        Menu,

        Initialization,
        PersistentManagers,
        Gameplay,

        Art
    }
}
