using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class CubeInputBehaviour : MonoBehaviour
{
    [Header("Data Input")]
    public PlayerData data;
    public InputReader inputReader;

    [Header("Input Component")]
    public PlayerInput playerInput;

    private Vector2 inputValue;

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

        // check that things aren't breaking here:
        inputReader.onInputActive += (bool state) => { if (state) playerInput.ActivateInput(); else playerInput.DeactivateInput(); };
    }

    void OnDestroy()
    {
        inputReader.onInputActive -= (bool state) => { if (state) playerInput.ActivateInput(); else playerInput.DeactivateInput(); };
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

    // error happened after I uncommented this
    /*public void OnFireLaser(InputAction.CallbackContext value)
    {
        if (value.performed)
            inputReader.OnFireLaser();
    }*/

    /*
    public void OnLeftLean(InputAction.CallbackContext value)
    {
        if (value.started)
            inputReader.leanAxisInput = -1;
        else if (value.canceled)
            inputReader.leanAxisInput = 0;
    }

    public void OnRightLean(InputAction.CallbackContext value)
    {
        if (value.started)
            inputReader.leanAxisInput = 1;
        else if (value.canceled)
            inputReader.leanAxisInput = 0;
    }


    public void OnSomersult(InputAction.CallbackContext value)
    {
        if (value.performed)
            inputReader.OnSomersult();
    }*/

    void UpdateData() => inputReader.UpdateInputData(inputValue);
}
