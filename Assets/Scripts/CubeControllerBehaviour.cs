using Cinemachine;
using DG.Tweening;
using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IUpdateItem is in UIBehaviour in starfox ex.
public class CubeControllerBehaviour : MonoBehaviour, IDamagable, IUpgradeItem
{
    [Header("Cube Settings")]
    public PlayerData data;
    public InputReader inputReader;

    [Header("Physics")]
    public Rigidbody _rigidbody;

    [Header("Paths")]
    public CinemachineDollyCart _stageDollyCart;
    public PathCreator _somersultPath;
    public PathCreator _uTurnPath;

    [Header("References")]
    public Transform _playerModel;
    public Transform _armatureModel;
    public Transform _aimTarget;
    public Transform _playerTransform;
    public Transform _cubeTransform;
    public Transform _arrow;

    private float forwardSpeed;
    private float distanceAcrobaticPath;
    private float smoothAcrobatic;
    private float delayDamage;

    Quaternion deltaRotation;
    private Vector2 smoothAimSpeed;
    private Vector2 smoothLocalSpeed;

    [Header("Listening to")]
    [SerializeField] private VoidEventChannelSO _allRangeModeChannel;

    void Awake()
    {
        if (inputReader.physicMovement)
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        else
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        _stageDollyCart.m_Speed = inputReader.trackSpeed;
        forwardSpeed = inputReader.allRangeSpeed;

        inputReader.onBarrelRoll += (int axis) => StartCoroutine(BarrelRoll(axis));
        /*
        inputReader.onBoost += (bool state) => Boost(state);
        inputReader.onBreak += (bool state) => Break(state);
        */
        inputReader.onSomersult += () => DOAcrobatic(AcrobaticState.Somersult, null, 50, false); // getting same error as before
        inputReader.onUTurn += () => DOAcrobatic(AcrobaticState.UTurn, _playerTransform, 50, false); // getting same error as before
        _allRangeModeChannel.OnEventRaised += SetAllRangeMode;
    }

    void OnDestroy()
    {
        inputReader.onBarrelRoll -= (int axis) => StartCoroutine(BarrelRoll(axis));
        inputReader.onSomersult -= () => DOAcrobatic(AcrobaticState.Somersult, null, 50, false);
        inputReader.onUTurn -= () => DOAcrobatic(AcrobaticState.UTurn, _playerTransform, 50, false);
        _allRangeModeChannel.OnEventRaised -= SetAllRangeMode;
    }

    void FixedUpdate()
    {
        if (data.shipState == ShipState.AllRangeMode)
            AllRangeMove(_playerTransform, _cubeTransform, inputReader.physicMovement);
        else
            LocalMove();

        if (inputReader.leanAxisInput == 0)
            HorizontalLean(_armatureModel);
        else
            LeanRotation(_armatureModel);

        // will need to update condition
        if (data.acrobaticState == AcrobaticState.None)
            ClampPosition(_cubeTransform, CameraManager.mainCamera);
        else
            PathUpdate(data.acrobaticState);

        if (data.shipState == ShipState.TrackMode || (data.shipState == ShipState.AllRangeMode && !inputReader.physicMovement))
            LookRotation(_cubeTransform, _aimTarget);
    }

    #region Movement Calculations

    // local movement x and y transforms
    void LocalMove()
    {
        smoothLocalSpeed.x = Mathf.SmoothStep(smoothLocalSpeed.x, inputReader.movementInput.x, inputReader.smoothLocalSpeed);
        smoothLocalSpeed.y = Mathf.SmoothStep(smoothLocalSpeed.y, inputReader.movementInput.y, inputReader.smoothLocalSpeed);

        if ((inputReader.movementInput.x > 0 && inputReader.leanAxisInput == 1) || (inputReader.movementInput.x < 0 && inputReader.leanAxisInput == -1))
            _rigidbody.velocity = new Vector3(smoothLocalSpeed.x * 1.5f, smoothLocalSpeed.y, 0) * inputReader.localSpeed;
        else
            _rigidbody.velocity = new Vector3(smoothLocalSpeed.x, smoothLocalSpeed.y, 0) * inputReader.localSpeed;
    }

    void AllRangeMove(Transform parentTransform, Transform cubeTransform, bool isPhysic)
    {
        // smooth value increment and decrement
        smoothLocalSpeed.x = Mathf.SmoothStep(smoothLocalSpeed.x, inputReader.movementInput.x, inputReader.smoothAllRangeMovement);
        smoothLocalSpeed.y = Mathf.SmoothStep(smoothLocalSpeed.y, inputReader.movementInput.y, inputReader.smoothAllRangeMovement);

        if (isPhysic)
        {
            // avoid z rotation
            _rigidbody.rotation = Quaternion.Euler(cubeTransform.eulerAngles.x, cubeTransform.eulerAngles.y, 0);

            // Increase rotation value when lean
            if ((inputReader.movementInput.x > 0 && inputReader.leanAxisInput == 1) || (inputReader.movementInput.x < 0 && inputReader.leanAxisInput == -1))
                deltaRotation = Quaternion.Euler(new Vector3(-inputReader.movementInput.y, inputReader.movementInput.x * 1.5f, 0));
            else
                deltaRotation = Quaternion.Euler(new Vector3(-inputReader.movementInput.y, inputReader.movementInput.x, 0));

            // rotate the ship
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);

            // constant forward movement
            _rigidbody.velocity = cubeTransform.forward * forwardSpeed;
        }
        else
        {
            // rotate the parent transform of the ship (increase value when lean)
            if ((inputReader.movementInput.x > 0 && inputReader.leanAxisInput == 1) || (inputReader.movementInput.x < 0 && inputReader.leanAxisInput == -1))
                parentTransform.Rotate(0, (smoothLocalSpeed.x * 1.5f), 0);
            else
                parentTransform.Rotate(0, (smoothLocalSpeed.x), 0);

            // change rigid body velocity Y
            _rigidbody.velocity = new Vector3(0, smoothLocalSpeed.y, 0) * (inputReader.localSpeed / 2);

            // move forward the parent based on the direction of the ship
            parentTransform.position += cubeTransform.forward * forwardSpeed * Time.fixedDeltaTime;
        }
    }

    void LookRotation(Transform t, Transform aimTarget)
    {
        //Smooth value increment and decrement
        smoothAimSpeed.x = Mathf.SmoothStep(smoothAimSpeed.x, inputReader.movementInput.x, inputReader.smoothAimSpeed);
        smoothAimSpeed.y = Mathf.SmoothStep(smoothAimSpeed.y, inputReader.movementInput.y, inputReader.smoothAimSpeed);

        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(smoothAimSpeed.x, smoothAimSpeed.y, aimTarget.localPosition.z);
        t.rotation = Quaternion.RotateTowards(t.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * inputReader.aimSpeed * Time.fixedDeltaTime);
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
        model.localEulerAngles = new Vector3(targetEulerAngle.x, targetEulerAngle.y, Mathf.LerpAngle(targetEulerAngle.z, -inputReader.leanAxisInput * inputReader.leanAngle, inputReader.leanSpeed));
    }

    //Calculates lean when moves in X
    void HorizontalLean(Transform model)
    {
        Vector3 targetEulerAngle = model.localEulerAngles;
        model.localEulerAngles = new Vector3(targetEulerAngle.x, targetEulerAngle.y, Mathf.LerpAngle(targetEulerAngle.z, -smoothLocalSpeed.x * inputReader.horizontalLeanLimit, inputReader.leanSpeed));
    }

    #endregion

    #region Acrobatic Movement
    IEnumerator BarrelRoll(int axis)
    {
        if (data.leanState != LeanState.None)
            yield break;

        _armatureModel.DOLocalRotate(new Vector3(0, 0, 350 * -axis), inputReader.barrelRollSpeed, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);

        // AudioManager thing...

        data.leanState = LeanState.BarrelRoll;
        yield return new WaitWhile(() => DOTween.IsTweening(_armatureModel));
        inputReader.leanAxisInput = 0;
        data.leanState = LeanState.None;
    }

    IEnumerator Somersult()
    {
        ClearBuffs();
        _arrow.gameObject.SetActive(false);

        inputReader.OnInputActive(false);
        data.acrobaticState = AcrobaticState.Somersult;

        CameraManager.SetLiveCamera("Somersult VCam");
        // Audio manager...

        _cubeTransform.parent = _playerTransform.parent;
        _somersultPath.transform.position = _cubeTransform.position;

        if (data.shipState == ShipState.AllRangeMode)
        {
            forwardSpeed = 0;
            _somersultPath.transform.rotation = _cubeTransform.rotation;

            _playerTransform.DOLocalRotate(_cubeTransform.eulerAngles, .25f);
            _playerTransform.DOMove(_somersultPath.path.GetPointAtDistance(_somersultPath.path.length), .25f);
        }
        else if (data.shipState == ShipState.TrackMode)
        {
            _stageDollyCart.m_Speed = 0;
            _somersultPath.transform.rotation = _playerTransform.rotation;
        }

        DOVirtual.Float(0, 2000, .5f, x => smoothAcrobatic = x);

        yield return new WaitWhile(() => distanceAcrobaticPath < _somersultPath.path.length);

        _cubeTransform.parent = _playerTransform;
        distanceAcrobaticPath = 0;

        if (data.shipState == ShipState.TrackMode)
        {
            _stageDollyCart.m_Speed = inputReader.trackSpeed;
            CameraManager.SetLiveCamera("Track VCam");
        }

        if (data.shipState == ShipState.AllRangeMode)
        {
            _playerTransform.DOLocalRotate(new Vector3(0, _playerTransform.localEulerAngles.y, 0), .25f);
            forwardSpeed = inputReader.allRangeSpeed;
            CameraManager.SetLiveCamera("All Range VCam");
        }

        _armatureModel.localEulerAngles = Vector3.zero;
        _arrow.gameObject.SetActive(true);

        inputReader.OnInputActive(true);
        data.acrobaticState = AcrobaticState.None;
    }

    /*IEnumerator UTurn(Transform direction)
    {
        
    }*/

    void PathUpdate(AcrobaticState state)
    {
        if (state == AcrobaticState.Somersult)
        {
            distanceAcrobaticPath += Time.deltaTime * inputReader.acrobaticSomersultSpeed;

            _cubeTransform.position = _somersultPath.path.GetPointAtDistance(distanceAcrobaticPath, EndOfPathInstruction.Stop);
            _armatureModel.rotation = Quaternion.RotateTowards(_armatureModel.rotation, _somersultPath.path.GetRotationAtDistance(distanceAcrobaticPath), smoothAcrobatic * Time.deltaTime);
        }
        else if (state == AcrobaticState.UTurn)
        {
            distanceAcrobaticPath += Time.deltaTime * inputReader.acrobaticUTurnSpeed;

            _cubeTransform.position = _uTurnPath.path.GetPointAtDistance(distanceAcrobaticPath, EndOfPathInstruction.Stop);
            _armatureModel.rotation = Quaternion.RotateTowards(_armatureModel.rotation, _uTurnPath.path.GetRotationAtDistance(distanceAcrobaticPath), smoothAcrobatic * Time.deltaTime);
        }
    }

    void DOAcrobatic(AcrobaticState acrobaticState, Transform direction, int amountCost, bool forced)
    {
        if (!forced && amountCost > data.buffAmount)
            return;

        if (acrobaticState == AcrobaticState.Somersult)
            StartCoroutine(Somersult());
        else if (acrobaticState == AcrobaticState.UTurn && data.shipState == ShipState.AllRangeMode)
            // TODO: implement uturn
            // StartCoroutine(Uturn(direction));
            Debug.Log("uturn stuff");
        else
            return;

        data.buffAmount -= amountCost;

        if (forced)
            data.buffAmount = 0;
    }
    #endregion

    #region Buffs
    public void ClearBuffs()
    {
        /*Boost(false);
        Break(false);*/
    }
    #endregion

    public void SetDamage(int damage)
    {
        if (delayDamage > 0)
            return;

        delayDamage = 1;
        float delayTime = 1;
        DOVirtual.Float(delayDamage, 0, delayTime, x => delayDamage = x);

        inputReader.isDamageEffect = true;
        data.lifeAmount -= damage;
        inputReader.OnUpdateHP();

        _playerModel.DOShakeRotation(.5f, 40);

        float backwardForce = 50f;
        if (data.shipState == ShipState.AllRangeMode)
            _playerTransform.DOMove(_playerTransform.position - (_playerTransform.forward * backwardForce), .25f);

        if (data.lifeAmount <= 0)
        {
            Debug.Log("Destroyed");
            Destroy(this.gameObject);
        }

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
        inputReader.OnInputActive(false);
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

        // inputReader.OnOpenWings(inputReader.allRangeWings, 5f);

        // yield return new WaitUntil(() => !__allRangeAnimation.isPlaying)

        CameraManager.SetUpdateMethod(CinemachineBrain.UpdateMethod.FixedUpdate);
        CameraManager.SetLiveCamera("All Range VCam");
        // CameraManager.SetDeadZoneComposer("Acrobatic VCam", 0, 0);

        yield return new WaitForSeconds(.5f);

        _arrow.gameObject.SetActive(true);

        inputReader.OnInputActive(true);

        yield break;
    }

    #region Collisions
    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other);
            SetDamage(10); // TODO: get this damage amount from enemy inputReader
        }
    }
    #endregion

    void Heal(int value)
    {
        int newHP = data.lifeAmount + value;
        if (newHP > 100)
            data.lifeAmount = 100;
        else
            data.lifeAmount = newHP;

        // UpdateHPImage();
    }

    #region Interfaces
    public void UpgradeSmartBomb() => Debug.Log("Smart Bomb Upgrade");

    public void HealItem(int healValue) => Heal(healValue);

    public void AddLife() => Debug.Log("AddLife");
    #endregion
}
