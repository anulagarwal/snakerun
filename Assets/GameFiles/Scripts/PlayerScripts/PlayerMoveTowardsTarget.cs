using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveTowardsTarget : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0f;

    private Vector3 targetLocation = Vector3.zero;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        targetLocation = GameObject.FindGameObjectWithTag("ReleasePoint").transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, Time.smoothDeltaTime * moveSpeed);
    }
    #endregion

    #region Public Core Functions
    public void EnableMoveTowards(bool value)
    {
        this.enabled = value;
    }

    public void SwitchTargetLocationToHead()
    {
        targetLocation = GameObject.FindGameObjectWithTag("Head").transform.position;
    }
    #endregion
}
