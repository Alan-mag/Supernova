using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerStateController : MonoBehaviour
{
    [Header("Data Input")]
    public PlayerData data;

    void Awake()
    {
        // Default Settings
        data.shipState = ShipState.TrackMode;
        data.acrobaticState = AcrobaticState.None;
        data.buffState = BuffState.None;
        data.leanState = LeanState.None;
        data.chargeWeaponState = ChargeWeaponState.None;
        data.enemyDetectionState = EnemyDetectionState.None;
        data.weaponState = WeaponState.LvOne;

        data.buffAmount = 100;
        data.lifeAmount = 100;
    }
}
