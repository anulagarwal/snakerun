using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlinkyMovementTriggerHandler : MonoBehaviour
{
    #region Properties
    [Header("Components Reference")]
    [SerializeField] private List<Transform> translatePoints = new List<Transform>();
    #endregion

    #region MonoBehaviour Functions
    #endregion

    #region Getter And Setter
    public List<Transform> GetTranslatePoints { get => translatePoints; }
    #endregion
}
