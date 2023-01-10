using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlinkyMovementController : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float translationSpeed = 0f;

    [Header("Componens Reference")]
    [SerializeField] private CharacterController headCharacterController = null;
    [SerializeField] private Transform targetParent = null;
    [SerializeField] private Transform mainParent = null;
    
    private List<Transform> translatePoints = new List<Transform>();
    private int translatePointIndex = 0;
    private Vector3 targetPosition = Vector3.zero;
    private CharacterController playerTailCC = null;
    private Transform headTransform = null;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        SwitchBeadMovementType(BeadFollowType.Tail);
    }

    private void OnEnable()
    {
        SetupTargetPosition();
    }

    private void Update()
    {
        if (Vector3.Distance(headTransform.position, targetPosition) > 0.1f)
        {
            headTransform.position = Vector3.MoveTowards(headTransform.position, targetPosition, Time.deltaTime * translationSpeed);
        }
        else
        {
            SetupTargetPosition();
        }
    }
    #endregion

    #region Getter And Setter
    public List<Transform> SetTranslatePoints { set { translatePoints = value; } }
    
    public CharacterController SetPlayerTailCC { set { playerTailCC = value; } }    
    #endregion

    #region Private Core Functions
    private void SetupTargetPosition()
    {
        targetPosition = new Vector3(transform.position.x, translatePoints[translatePointIndex].position.y, translatePoints[translatePointIndex].position.z);
        translatePointIndex++;

        if (translatePointIndex >= translatePoints.Count)
        {
            translatePointIndex = 0;
            SwitchBeadMovementType(BeadFollowType.Head);
        }
    }

    private void SwitchBeadMovementType(BeadFollowType followType)
    {
        PlayerSingleton.Instance.GetPlayerBeadsManager.PlayerBeadFollowType = followType;
        switch (followType)
        {
            case BeadFollowType.Head:
                headTransform = headCharacterController.transform;
                headCharacterController.transform.parent = mainParent;
                break;
            case BeadFollowType.Tail:
                headTransform = playerTailCC.transform;
                headCharacterController.transform.parent = targetParent;
                break;
        }
    }
    #endregion
}
