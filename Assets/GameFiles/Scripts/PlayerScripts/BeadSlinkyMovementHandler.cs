using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadSlinkyMovementHandler : MonoBehaviour
{
    #region Properties
    [Header("Components Reference")]
    [SerializeField] private Transform movementPoint = null;
    #endregion

    #region MonoBehaviour Functions
    #endregion

    #region Getter And Setter
    public Transform BeadTargetPoint { get; set; }

    public Transform BeadTargetReversePoint { get; set; }

    public Transform GetMovementPoint { get => movementPoint; }
    #endregion
}
