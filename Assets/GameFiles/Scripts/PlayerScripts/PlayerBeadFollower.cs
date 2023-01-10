using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeadFollower : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private Transform beadTailTransform = null;
    [SerializeField] private Transform beadHeadTransform = null;
    #endregion

    #region MonoBehaviour Functions
    #endregion

    #region Getter And Setter
    public Transform NormalMovementTargetTransform { get; set; }

    public Transform SlinkyMovementTargetTransform { get; set; }

    public Transform GetBeadTailTransform { get => beadTailTransform; }

    public Transform GetBeadHeadTransform { get => beadHeadTransform; }

    public bool IsTail { get; set; }
    #endregion
}
