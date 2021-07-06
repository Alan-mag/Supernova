using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class CubeInputBehaviour : MonoBehaviour
{
    [Header("Data Input")]
    public CubeData cubeData;

    [Header("Input Component")]
    public PlayerInput cubeInput;

    private Vector2 inputValue;

    void Awake()
    {
        cubeData.shipState = ShipState.TrackMode;
        cubeData.acrobaticState = AcrobaticState.None;
        cubeData.leanState = LeanState.None;
        cubeData.chargeWeaponState = ChargeWeaponState.None;
        cubeData.enemyDetectionState = EnemyDetectionState.None;
        cubeData.weaponState = WeaponState.LvOne;

        cubeData.onInputActive += (bool state) => { if (state) cubeInput.ActivateInput(); else cubeInput.DeactivateInput(); };
    }

    void Start()
    {

    }

    void Update()
    {
        UpdateData();
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        inputValue = value.ReadValue<Vector2>();
    }

    public void OnLeftLean(InputAction.CallbackContext value)
    {
        if (value.started)
            cubeData.leanAxisInput = -1;
        else if (value.canceled)
            cubeData.leanAxisInput = 0;
    }

    public void OnRightLean(InputAction.CallbackContext value)
    {
        if (value.started)
            cubeData.leanAxisInput = 1;
        else if (value.canceled)
            cubeData.leanAxisInput = 0;
    }

    public void OnFireLaser(InputAction.CallbackContext value)
    {
        if (value.performed)
            cubeData.OnFireLaser();
    }

    void UpdateData() => cubeData.UpdateInputData(inputValue);
}
