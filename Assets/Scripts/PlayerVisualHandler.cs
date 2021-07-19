using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public class PlayerVisualHandler : MonoBehaviour
{
    [Header("Player Settings")]
    public CubeData data;

    [Header("Logic FPS")]
    [Range(0, 60)]
    public int fps;

    /*[Header("VFX")]
    [SerializeField] VisualEffect fireVFX;
    [SerializeField] VisualEffect spinVFX;
    [SerializeField] VisualEffect damageVFX;
    [SerializeField] VisualEffect dustVFX;
    [SerializeField] VisualEffect[] boostVFX;
    [SerializeField] VisualEffect[] colorVFX;*/

    /*[Header("Trails")]
    [SerializeField] TrailRenderer fireTrail;
    [SerializeField] TrailRenderer[] trails;*/

    private CustomFixedUpdate FU_LogicInstance;

    // readonly string string_orientation = "Orientation";
    // readonly string string_materialColor = "Vector1_253D52CD";

    void Awake()
    {
        FU_LogicInstance = new CustomFixedUpdate(OnFixedUpdate, fps);
    }

    void Update()
    {
        FU_LogicInstance.Update();
    }

    void OnFixedUpdate(float aDeltaTime)
    {

    }
}
