using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFrameRateManager : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private int targetFrameRate = 0;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }
    #endregion
}
