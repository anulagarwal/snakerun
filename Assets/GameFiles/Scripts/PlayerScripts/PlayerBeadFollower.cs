using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeadFollower : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float followSpeed = 0f;
    #endregion

    #region MonoBehaviour Functions
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, TargetTransform.position, followSpeed * Time.deltaTime);
    }
    #endregion

    #region Getter And Setter
    public Transform TargetTransform { get; set; }
    #endregion
}
