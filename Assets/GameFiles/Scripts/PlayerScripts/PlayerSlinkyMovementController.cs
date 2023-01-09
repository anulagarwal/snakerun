using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlinkyMovementController : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0f;

    [Header("Components Reference")]
    [SerializeField] private CharacterController playerHeadCC = null;

    private CharacterController playerTailCC = null;
    #endregion

    #region MonoBehaviour Functions
    private void Update()
    {
        
    }
    #endregion

    #region Getter And Setter
    public CharacterController SetPlayerTailCC { set { playerTailCC = value; } }
    #endregion
}
