using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    #region Properties
    public static PlayerSingleton Instance = null;

    [Header("Components Reference")]
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
    #endregion

    #region Getter And Setter
    public PlayerMovementController GetPlayerMovementController { get => playerMovementController; }
    
    public PlayerBeadsManager GetPlayerBeadsManager { get => playerBeadsManager; }
    #endregion

    #region Public Core Functions
    public void DisableMovement()
    {
        playerMovementController.EnablePlayerMovement(false);
    }
    #endregion
}
