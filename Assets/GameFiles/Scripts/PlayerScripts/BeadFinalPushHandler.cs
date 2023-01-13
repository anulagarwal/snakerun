using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeadFinalPushHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float finalPushSpeed = 0f;

    private Vector3 targetDestination = Vector3.zero;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        targetDestination = GameObject.FindObjectOfType<EatTriggerHandler>().transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetDestination, Time.deltaTime * finalPushSpeed);
    }
    #endregion

    #region Private Core Functions
    #endregion
}
