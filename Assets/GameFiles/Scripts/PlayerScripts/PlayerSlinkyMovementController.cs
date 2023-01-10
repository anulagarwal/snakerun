using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlinkyMovementController : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float translationSpeed = 0f;
    
    private List<Transform> translatePoints = new List<Transform>();
    private int translatePointIndex = 0;
    private Vector3 targetPosition = Vector3.zero;
    #endregion

    #region MonoBehaviour Functions
    private void OnEnable()
    {
        SetupTargetPosition();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * translationSpeed);
        }
        else
        {
            SetupTargetPosition();
        }
    }
    #endregion

    #region Getter And Setter
    public List<Transform> SetTranslatePoints { set { translatePoints = value; } }
    #endregion

    #region Private Core Functions
    private void SetupTargetPosition()
    {
        targetPosition = new Vector3(transform.position.x, translatePoints[translatePointIndex].position.y, translatePoints[translatePointIndex].position.z);
        translatePointIndex++;

        if (translatePointIndex >= translatePoints.Count)
        {
            translatePointIndex = 0;
            PlayerSingleton.Instance.SwitchMovementType(MovementType.Normal);
        }
    }
    #endregion
}
