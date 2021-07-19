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
        // Default Settings
        cubeData.shipState = ShipState.TrackMode;
        cubeData.acrobaticState = AcrobaticState.None;
        cubeData.buffState = BuffState.None;
        cubeData.leanState = LeanState.None;
        cubeData.chargeWeaponState = ChargeWeaponState.None;
        cubeData.enemyDetectionState = EnemyDetectionState.None;
        cubeData.weaponState = WeaponState.LvOne;

        cubeData.buffAmount = 100;
        cubeData.lifeAmount = 100;

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

    public void OnSomersult(InputAction.CallbackContext value)
    {
        if (value.performed)
            cubeData.OnSomersult();
    }

    void UpdateData() => cubeData.UpdateInputData(inputValue);
}
