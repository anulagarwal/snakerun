using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMVCManager : MonoBehaviour
{
    #region Properties
    public static CMVCManager Instance = null;

    [Header("Components Reference")]
    [SerializeField] private CinemachineVirtualCamera cmvc = null;
    #endregion

    #region MonoBehaviour functions
    private void Awake()
    {
        if(Instance!=null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }
    #endregion

    #region Public Core Functions
    public void DisableFollow()
    {
        cmvc.Follow = null;
    }
    #endregion
}
