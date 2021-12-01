using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameplayObject : MonoBehaviour
{
    void Awake()
    {
        // DontDestroyOnLoad(this.gameObject); // causes dullicated
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemiesContainer");
        if (enemies[0] != null)
        {
            enemies[0].transform.SetParent(this.gameObject.transform);
        }

        // get reference to cinemachineDollyCart component
        // check if m_Path is null
        // if so, get reference to object with tag LevelDollyTrack
        // set m_path on cinemacinedollycart component equal to scene levelDollyTrack

        // todo: might need to fix this for levels with no track?
        CinemachineDollyCart levelCinemachineDollyCart = gameObject.GetComponent(typeof(CinemachineDollyCart)) as CinemachineDollyCart;
        GameObject[] levelDollyTrack = GameObject.FindGameObjectsWithTag("LevelDollyTrack");
        CinemachineSmoothPath levelPath = levelDollyTrack[0].GetComponent(typeof(CinemachineSmoothPath)) as CinemachineSmoothPath;

        if (levelCinemachineDollyCart != null && levelDollyTrack != null && levelPath != null)
        {
            if (levelCinemachineDollyCart.m_Path == null)
            {
                levelCinemachineDollyCart.m_Path = levelPath;
            }
        }
    }

    void Start()
    {

    }


    public void SetAllRangeModeForPlayer()
    {
        CubeControllerBehaviour boxWing = GetComponentInChildren<CubeControllerBehaviour>();
        Debug.Log(boxWing);
        if (boxWing != null)
            boxWing.SetAllRangeMode();
    }
}
