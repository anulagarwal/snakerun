using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    #region Properties
    public static PlayerSingleton Instance = null;

    [Header("Components Reference")]
    [SerializeField] private PlayerMovementController playerMovementController = null;
    [SerializeField] private PlayerSlinkyMovementController playerSlinkyMovementController = null;
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
        SwitchPlayerBeadsMovementStyle(BeadsMovementStyle.Normal);
    }
    #endregion

    #region Getter And Setter
    public PlayerMovementController GetPlayerMovementController { get => playerMovementController; }

    public PlayerSlinkyMovementController GetPlayerSlinkyMovementController { get => playerSlinkyMovementController; }
    
    public PlayerBeadsManager GetPlayerBeadsManager { get => playerBeadsManager; }

    public BeadsMovementStyle PlayerBeadsMovementStyle { get; set; }
    #endregion

    #region Public Core Functions
    public void DisableMovement()
    {
        playerMovementController.EnablePlayerMovement(false);
    }

    public void SwitchPlayerBeadsMovementStyle(BeadsMovementStyle style)
    {
        PlayerBeadsMovementStyle = style;

        switch (style)
        {
            case BeadsMovementStyle.Normal:
                playerMovementController.enabled = true;
                playerSlinkyMovementController.enabled = false;
                break;
            case BeadsMovementStyle.Slinky:
                playerMovementController.enabled = false;
                playerSlinkyMovementController.enabled = true;
                break;
        }
    }
    #endregion
}
