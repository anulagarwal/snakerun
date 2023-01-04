using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeadFollower : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float followSpeed = 0f;
    [SerializeField] private Transform beadTailtransform = null;
    #endregion

    #region MonoBehaviour Functions
    //private void LateUpdate()
    //{
    //    transform.position = Vector3.Lerp(transform.position, TargetTransform.position, followSpeed * Time.deltaTime);
    //}
    #endregion

    #region Getter And Setter
    public Transform TargetTransform { get; set; }

    public Transform GetBeadTailTransform { get => beadTailtransform; }
    #endregion
}
