using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataCap", menuName = "ScriptableObjects/Practice1/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Parameters")]
    public EnemyStateCap state;
    public bool canFire;
    public float trackSpeed;

    [Header("Status")]
    public int maxLife;
    public int scoreValue;
    public int damageToPlayer;

    [Header("Weapons")]
    public float attackRange;
    public int weaponDamage;

    [Header("Other")]
    [ReadOnly] public EnemyBehaviourCap behaviour;
    [ReadOnly] public int lifeAmount;
}

public enum EnemyBehaviourCap { None, InPath, Chasing, ReturningPath }
public enum EnemyStateCap { TrackStage, FreeStage }
