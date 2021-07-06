using Cinemachine;
using DG.Tweening;
using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControllerBehaviour : MonoBehaviour, IDamagable
{
    [Header("Cube Settings")]
    public CubeData data;

    [Header("Physics")]
    public Rigidbody _rigidbody;

    [Header("Paths")]
    public CinemachineDollyCart _stageDollyCart;

    [Header("References")]
    public Transform _playerModel;
    public Transform _armatureModel;
    public Transform _aimTarget;
    public Transform _playerTransform;
    public Transform _cubeTransform;
    public Transform _arrow;

    private float forwardSpeed;
    private float delayDamage;

    Quaternion deltaRotation;
    private Vector2 smoothAimSpeed;
    private Vector2 smoothLocalSpeed;

    void Awake()
    {
        if (data.physicMovement)
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        else
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        _stageDollyCart.m_Speed = data.trackSpeed;
        forwardSpeed = data.allRangeSpeed;
    }

    void FixedUpdate()
    {
        if (data.shipState == ShipState.AllRangeMode)
            AllRangeMove(_playerTransform, _cubeTransform, data.physicMovement);
        else
            LocalMove();

        if (data.leanAxisInput == 0)
            HorizontalLean(_armatureModel);
        else
            LeanRotation(_armatureModel);

        // will need to update condition
        if (data.acrobaticState == AcrobaticState.None)
            ClampPosition(_cubeTransform, CameraManagerAlan.mainCamera);
        else
            PathUpdate(data.acrobaticState);

        if (data.shipState == ShipState.TrackMode || (data.shipState == ShipState.AllRangeMode && !data.physicMovement))
            LookRotation(_cubeTransform, _aimTarget);
    }

    // local movement x and y transforms
    void LocalMove()
    {
        smoothLocalSpeed.x = Mathf.SmoothStep(smoothLocalSpeed.x, data.movementInput.x, data.smoothLocalSpeed);
        smoothLocalSpeed.y = Mathf.SmoothStep(smoothLocalSpeed.y, data.movementInput.y, data.smoothLocalSpeed);

        if ((data.movementInput.x > 0 && data.leanAxisInput == 1) || (data.movementInput.x < 0 && data.leanAxisInput == -1))
            _rigidbody.velocity = new Vector3(smoothLocalSpeed.x * 1.5f, smoothLocalSpeed.y, 0) * data.localSpeed;
        else
            _rigidbody.velocity = new Vector3(smoothLocalSpeed.x, smoothLocalSpeed.y, 0) * data.localSpeed;
    }

    void AllRangeMove(Transform parentTransform, Transform cubeTransform, bool isPhysic)
    {
        // smooth value increment and decrement
        smoothLocalSpeed.x = Mathf.SmoothStep(smoothLocalSpeed.x, data.movementInput.x, data.smoothAllRangeMovement);
        smoothLocalSpeed.y = Mathf.SmoothStep(smoothLocalSpeed.y, data.movementInput.y, data.smoothAllRangeMovement);

        if (isPhysic)
        {
            // avoid z rotation
            _rigidbody.rotation = Quaternion.Euler(cubeTransform.eulerAngles.x, cubeTransform.eulerAngles.y, 0);

            // Increase rotation value when lean
            if ((data.movementInput.x > 0 && data.leanAxisInput == 1) || (data.movementInput.x < 0 && data.leanAxisInput == -1))
                deltaRotation = Quaternion.Euler(new Vector3(-data.movementInput.y, data.movementInput.x * 1.5f, 0));
            else
                deltaRotation = Quaternion.Euler(new Vector3(-data.movementInput.y, data.movementInput.x, 0));

            // rotate the ship
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);

            // constant forward movement
            _rigidbody.velocity = cubeTransform.forward * forwardSpeed;
        }
        else
        {
            // rotate the parent transform of the ship (increase value when lean)
            if ((data.movementInput.x > 0 && data.leanAxisInput == 1) || (data.movementInput.x < 0 && data.leanAxisInput == -1))
                parentTransform.Rotate(0, (smoothLocalSpeed.x * 1.5f), 0);
            else
                parentTransform.Rotate(0, (smoothLocalSpeed.x), 0);

            // change rigid body velocity Y
            _rigidbody.velocity = new Vector3(0, smoothLocalSpeed.y, 0) * (data.localSpeed / 2);

            // move forward the parent based on the direction of the ship
            parentTransform.position += cubeTransform.forward * forwardSpeed * Time.fixedDeltaTime;
        }
    }

    void LookRotation(Transform t, Transform aimTarget)
    {
        //Smooth value increment and decrement
        smoothAimSpeed.x = Mathf.SmoothStep(smoothAimSpeed.x, data.movementInput.x, data.smoothAimSpeed);
        smoothAimSpeed.y = Mathf.SmoothStep(smoothAimSpeed.y, data.movementInput.y, data.smoothAimSpeed);

        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(smoothAimSpeed.x, smoothAimSpeed.y, aimTarget.localPosition.z);
        t.rotation = Quaternion.RotateTowards(t.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * data.aimSpeed * Time.fixedDeltaTime);
    }

    void ClampPosition(Transform transform, Camera camera)
    {
        //Get the transform point
        Vector3 worldPosition = camera.WorldToViewportPoint(transform.position);

        //Clamp the position
        worldPosition.x = Mathf.Clamp01(worldPosition.x);
        worldPosition.y = Mathf.Clamp01(worldPosition.y);

        //Set the transform point
        transform.position = camera.ViewportToWorldPoint(worldPosition);
    }

    void LeanRotation(Transform model)
    {
        Vector3 targetEulerAngle = model.localEulerAngles;
        model.localEulerAngles = new Vector3(targetEulerAngle.x, targetEulerAngle.y, Mathf.LerpAngle(targetEulerAngle.z, -data.leanAxisInput * data.leanAngle, data.leanSpeed));
    }

    //Calculates lean when moves in X
    void HorizontalLean(Transform model)
    {
        Vector3 targetEulerAngle = model.localEulerAngles;
        model.localEulerAngles = new Vector3(targetEulerAngle.x, targetEulerAngle.y, Mathf.LerpAngle(targetEulerAngle.z, -smoothLocalSpeed.x * data.horizontalLeanLimit, data.leanSpeed));
    }

    /*
     *
     *
     *
     *
     */
    void PathUpdate(AcrobaticState state)
    {
        Debug.Log("PathUpdate");
    }

    public void SetDamage(int damage)
    {
        if (delayDamage > 0)
            return;

        delayDamage = 1;
        float delayTime = 1;
        DOVirtual.Float(delayDamage, 0, delayTime, x => delayDamage = x);

        data.isDamageEffect = true;
        data.lifeAmount -= damage;
        data.OnUpdateHP();

        _playerModel.DOShakeRotation(.5f, 40);

        float backwardForce = 50f;
        if (data.shipState == ShipState.AllRangeMode)
            _playerTransform.DOMove(_playerTransform.position - (_playerTransform.forward * backwardForce), .25f);

        if (data.lifeAmount <= 0)
            Debug.Log("Destroyed");

        Debug.Log("BoxWing Life (" + data.lifeAmount + ")");
        // AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.DamageObstacle));
    }

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }

    public void SetAllRangeMode() => StartCoroutine(SetAllRangeModeCoroutine());

    IEnumerator SetAllRangeModeCoroutine()
    {
        data.OnInputActive(false);
        data.shipState = ShipState.AllRangeMode;

        _playerTransform.DOLocalRotate(Vector3.zero, .5f);
        _cubeTransform.DOLocalRotate(Vector3.zero, .5f);
        _cubeTransform.DOLocalMoveX(0, 3f).SetUpdate(UpdateType.Fixed);
        _cubeTransform.DOLocalMoveY(0, 3f).SetUpdate(UpdateType.Fixed);
        _rigidbody.velocity = Vector3.zero;

        _stageDollyCart.enabled = false;

        // _allRangeAnimation.Play();
        // _arrow.gameObject.SetActive(false);

        yield return new WaitForSeconds(3);

        // data.OnOpenWings(data.allRangeWings, 5f);

        // yield return new WaitUntil(() => !__allRangeAnimation.isPlaying)

        CameraManagerAlan.SetUpdateMethod(CinemachineBrain.UpdateMethod.FixedUpdate);
        CameraManagerAlan.SetLiveCamera("All Range VCam");
        // CameraManager.SetDeadZoneComposer("Acrobatic VCam", 0, 0);

        yield return new WaitForSeconds(.5f);

        _arrow.gameObject.SetActive(true);

        data.OnInputActive(true);

        yield break;
    }
}
