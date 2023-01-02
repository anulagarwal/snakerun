using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCanvasHandler : MonoBehaviour
{
    #region Properties
    [Header("Components Reference")]
    [SerializeField] private VariableJoystick movementJS = null;
    #endregion

    #region MonoBehaviour Functions
    #endregion

    #region Getter And Setter
    public VariableJoystick GetMovementJS { get=>movementJS; }
    #endregion

    #region Public Core Functions
    #endregion
}
