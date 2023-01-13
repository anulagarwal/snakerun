using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSingleton : MonoBehaviour
{
    #region Properties
    public static HeadSingleton Instance = null;

    [Header("Components Reference")]
    [SerializeField] private Transform eatTriggerTransform = null;
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

    #region Getter And Setter
    public Transform GetEatTriggerTransform { get; set; }
    #endregion
}
