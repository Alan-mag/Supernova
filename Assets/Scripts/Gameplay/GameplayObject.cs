using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameplayObject : MonoBehaviour
{
    [SerializeField]
    private GameObject spaceSkybox;

    void Awake()
    {
        // DontDestroyOnLoad(this.gameObject); // causes dullicated
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("EnemiesContainer");
        if (enemies.Length > 0)
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

        string levelName = SceneManager.GetActiveScene().name;

        // Debug.Log("Awake:" + SceneManager.GetActiveScene().name);

        // not good: hard coded --> fix this another wya

        if (levelName.Equals("Level6[SpaceElevator]") || levelName.Equals("Level8[Boss]"))
            spaceSkybox.SetActive(true);
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
