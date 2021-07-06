using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataCap", menuName = "ScriptableObjects/Practice1/EnemyDataCap")]
public class EnemyDataCap : ScriptableObject
{
    [Header("Parameters")]
    public EnemyStateCap state;
    public bool canFire;
    public float trackSpeed;

    [Header("Status")]
    public int maxLife;
    public int scoreValue;
    public int damageToPlayer;

    [ReadOnly] public EnemyBehaviourCap behaviour;
    [ReadOnly] public int lifeAmount;
}

public enum EnemyBehaviourCap { None, InPath, Chasing, ReturningPath }
public enum EnemyStateCap { TrackStage, FreeStage }
