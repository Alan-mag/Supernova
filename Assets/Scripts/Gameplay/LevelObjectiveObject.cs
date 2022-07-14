using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectiveObject : MonoBehaviour
{
    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO levelObjectiveChannel = default;

    private void OnDestroy() //only issue is that this will also trigger on scene change? not sure if that might break things
    {
        RaiseLevelObjectChannel();
    }

    public void RaiseLevelObjectChannel()
    {
        Debug.Log("RaiseLevelObjectChannel");
        levelObjectiveChannel.RaiseEvent();
    }
}
