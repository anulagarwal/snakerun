using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadsPoolManager : MonoBehaviour
{
    #region Properties
    public static BeadsPoolManager Instance = null;

    [Header("Components Reference")]
    [SerializeField] private List<GameObject> beads = new List<GameObject>();

    private int activeBeadIndex = -1;
    #endregion

    #region MonoBehaviour Functions
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }
    #endregion

    #region Public Core Functions
    public GameObject SpawnBead()
    {
        if (activeBeadIndex < beads.Count)
        {
            activeBeadIndex++;
            return beads[activeBeadIndex];
        }
        else
        {
            return null;
        }
    }
    #endregion

    #region Private Core Functions
    #endregion
}
