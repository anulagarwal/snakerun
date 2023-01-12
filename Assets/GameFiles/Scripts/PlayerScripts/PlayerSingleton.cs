using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
public class PlayerSingleton : MonoBehaviour
{
    #region Properties
    public static PlayerSingleton Instance = null;

    [Header("Components Reference")]
    [SerializeField] private GameObject playerParentObj = null;
    [SerializeField] private GameObject splashVFXObj = null;
    [SerializeField] private PlayerMovementController playerMovementController = null;
    [SerializeField] private PlayerBeadsManager playerBeadsManager = null;
    #endregion

    #region MonoBehaviour Functions
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        ForceStopPlayerMovement = false;
    }
    #endregion

    #region Getter And Setter
    public PlayerMovementController GetPlayerMovementController { get => playerMovementController; }
    
    public PlayerBeadsManager GetPlayerBeadsManager { get => playerBeadsManager; }

    public bool ForceStopPlayerMovement { get; set; }
    #endregion

    #region Public Core Functions
    public void DisableNormalMovement()
    {
        playerMovementController.EnablePlayerMovement(false);
    }

    public void GameOver()
    {
        splashVFXObj.SetActive(true);
        splashVFXObj.transform.parent = null;
        playerParentObj.SetActive(false);
    }
    #endregion
}
