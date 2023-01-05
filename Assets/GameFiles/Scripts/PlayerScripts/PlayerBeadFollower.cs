using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeadFollower : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private Transform beadTailtransform = null;
    #endregion

    #region MonoBehaviour Functions
    #endregion

    #region Getter And Setter
    public Transform TargetTransform { get; set; }

    public Transform GetBeadTailTransform { get => beadTailtransform; }
    #endregion
}
