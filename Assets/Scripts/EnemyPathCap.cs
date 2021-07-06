using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyPathCap : MonoBehaviour
{
    [Header("Data")]
    public EnemyDataCap data;

    CinemachineDollyCart _dollyCart;
    CinemachineSmoothPath _path;

    void Awake()
    {
        _dollyCart = GetComponent<CinemachineDollyCart>();
        _dollyCart.m_Speed = data.trackSpeed;
        _path = (CinemachineSmoothPath) _dollyCart.m_Path;

        if (data.state == EnemyStateCap.TrackStage)
            _dollyCart.enabled = true;
        else
            _dollyCart.enabled = false;
    }
}
