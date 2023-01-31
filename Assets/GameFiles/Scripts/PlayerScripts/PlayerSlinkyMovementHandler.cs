using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlinkyMovementHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float followSpeed = 0f;

    private List<Transform> beads = new List<Transform>();
    private List<Transform> movementPoints  = new List<Transform>();
    private Transform targetMovementPoint = null;
    private int movementPointIndex = 0;
    private Transform PlayerBeadMainTransform = null;

    private Transform headBead = null;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        PlayerBeadMainTransform = PlayerSingleton.Instance.transform;
        IsSlinkyMovementActive = false;
    }

    private void Update()
    {
        if (IsSlinkyMovementActive)
        {
            SlinkyMovementCore();
        }
    }
    #endregion

    #region Getter And Setter
    public bool IsSlinkyMovementActive { get; set; }
    #endregion

    #region Private Core Functions
    private void SlinkyMovementCore()
    {
        print(Vector3.Distance(headBead.position, targetMovementPoint.position));
        if (Vector3.Distance(headBead.position, targetMovementPoint.position) <= 0.1f)
        {
            targetMovementPoint = movementPoints[movementPointIndex];
            movementPointIndex++;

            if (movementPointIndex >= movementPoints.Count)
            {
                IsSlinkyMovementActive = false;
                print("Slinky Movement Done");
                return;
            }
        }
        else
        {
            headBead.position = Vector3.MoveTowards(headBead.position, targetMovementPoint.position, moveSpeed * Time.smoothDeltaTime);   
        
            foreach (Transform t in beads)
            {
                t.position = Vector3.Lerp(t.position, t.GetComponent<BeadSlinkyMovementHandler>().BeadTargetPoint.position, followSpeed * Time.smoothDeltaTime);
            }
        }
    }
    #endregion

    #region Public Core Functions
    public void ActivateSlinkyMovement(List<Transform> beads, List<Transform> movementPoints)
    {
        this.beads.Clear();
        this.movementPoints.Clear();

        this.beads = beads;
        this.movementPoints = movementPoints;
        movementPointIndex = 0;
        targetMovementPoint = this.movementPoints[movementPointIndex];

        movementPointIndex++;

        headBead = PlayerBeadMainTransform;
        beads.Insert(0, headBead);
        for(int i = 1; i < this.beads.Count; i++)
        {
            this.beads[i].GetComponent<BeadSlinkyMovementHandler>().BeadTargetPoint = this.beads[i - 1].GetComponent<BeadSlinkyMovementHandler>().GetMovementPoint;
        }

        IsSlinkyMovementActive = true;
    }
    #endregion
}
