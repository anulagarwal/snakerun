using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowLastBead : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float followSpeed = 0f;
    #endregion

    #region MonoBehaviour Functions
    private void Update()
    {
        transform.position = Vector3.LerpUnclamped(transform.position, LastBeadTransform.position, Time.smoothDeltaTime * followSpeed);
    }
    #endregion

    #region Getter And Setter
    public Transform LastBeadTransform { get; set; }
    #endregion
}
