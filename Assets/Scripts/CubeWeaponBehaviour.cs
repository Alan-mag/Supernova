using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CubeWeaponBehaviour : MonoBehaviour
{
    [Header("Player Settings")]
    public PlayerData data;
    public InputReader inputReader;

    [Header("Logic FPS")]
    [Range(0, 60)]
    public int fps;

    [Header("VFX")]
    [SerializeField] VisualEffect chargeLaserVFS;
    [SerializeField] VisualEffect arrowVFX;

    [Header("Lasers")]
    [SerializeField] ParticleSystem[] laserLevels;

    [Header("References")]
    [SerializeField] Transform pointChargeLaser;
    [SerializeField] Transform pointDetect;

    [Header("Layers")]
    [SerializeField] LayerMask _enemyLayer;
    [SerializeField] LayerMask _laserColliderLayer;

    private CustomFixedUpdate FU_LogicInstance;
    private Transform detectedEnemy;
    private RaycastHit hit;
    private Vector3 fwd;
    private float holdTimer;
    private bool canFireBomb = true;

    private readonly string string_chargeLaser = "ChargedLaser";
    // private readonly string string_arrowCharge = "Arrow Charge";

    void Awake()
    {
        // FU_LogicInstance = new CustomFixedUpdate(OnFixedUpdate, fps);
         
        for (int i = 0; i < laserLevels.Length; i++)
        {
            ParticleSystem.CollisionModule collision = laserLevels[i].collision;
            collision.collidesWith = _laserColliderLayer;
        }

        // data.onReleaseLaser += () => ReleaseChargeLaser(); 
        // inputReader.onFireLaser += () => FireWeapon();
        inputReader.onFireLaser += FireWeapon;
    }

    void OnDestroy()
    {
        // inputReader.onFireLaser -= () => FireWeapon();
        inputReader.onFireLaser -= FireWeapon;
    }

    void Update()
    {
        // FU_LogicInstance.Update();
    }

    void OnFixedUpdate(float aDeltaTime)
    {
        // todo: once fps settings this is built
        // if (data.chargeWeaponState == ChargeWeaponState.Charging)
        //    ChargeLaser();

        if (data.enemyDetectionState == EnemyDetectionState.CanDetect)
            DetectEnemy(pointDetect);
    }

    #region LaserWeapon
    public void UpdateLaser()
    {
        if ((int)data.weaponState + 1 >= System.Enum.GetValues(typeof(WeaponState)).Length)
            return;

        data.weaponState = (WeaponState)((int)data.weaponState + 1);

        if ((int)data.weaponState + 1 == System.Enum.GetValues(typeof(WeaponState)).Length)
            // data.OnOpenHyperLaser(true);
            Debug.Log("todo update laser");
    }

    public void FireWeapon()
    {
        Debug.Log("fire weapon");
        // I think this fires the particle effect .Play()
        // error is here, laserlevels particlesystem has been destroyed but
        // is still trying to access on scene change
        Debug.Log(laserLevels[0].transform.root.name);

        laserLevels[(int)data.weaponState].Play();
        // laserLevels[0].Play();

        if (data.weaponState == WeaponState.LvOne)
            // AudioManager call 
            Debug.Log("Pew Pew todo FireWeaponSound1");
        else if (data.weaponState == WeaponState.LvTwo)
            // AudioManager call 
            Debug.Log("Pew Pew todo FireWeaponSound2");
        else
            // AudioManager call 
            Debug.Log("Pew Pew todo FireWeaponSound3");
    }
    #endregion

    #region Logic
    void DetectEnemy(Transform pointLaser)
    {
        fwd = pointLaser.TransformDirection(Vector3.forward);

        if (Physics.BoxCast(pointLaser.position, Vector3.one, fwd, out hit, pointLaser.rotation, 800))
        {
            if (((1 << hit.transform.gameObject.layer) & _enemyLayer) != 0)
            {
                if (hit.transform.TryGetComponent(out IDetectable dtc))
                {
                    detectedEnemy = dtc.GetTransform();
                    dtc.ShowArrow(true);
                    // arrowVFX.SetFloat(string_arrowFade, 0);
                    data.enemyDetectionState = EnemyDetectionState.Detected;
                    // AudioManager.Instance.PlaySFX(AudioEffectsHandler.GetAudioClip(EffectsSounds.TargetLock));
                }
            }
        }
    }
    #endregion
}
