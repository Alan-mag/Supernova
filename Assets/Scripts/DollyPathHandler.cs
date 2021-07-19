using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyPathHandler : MonoBehaviour
{
    public CinemachinePathBase newPath;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetCinemachineDollyCart()
    {
        var dolly = gameObject.GetComponent<CinemachineDollyCart>();
        dolly.m_Path = newPath;
    }
}
