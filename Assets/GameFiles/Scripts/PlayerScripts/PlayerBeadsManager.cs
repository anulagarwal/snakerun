using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBeadsManager : MonoBehaviour
{
    #region Properties
    [Header("Player Level Indicator Attributes")]
    [SerializeField] private int playerLevel = 0;
    [SerializeField] private TextMeshPro playerLevelIndicatorTMP = null;

    [Header("Player Beads Attributes")]
    [SerializeField] private float beadsFollowSpeed = 0f;
    [SerializeField] private List<Transform> playerBeadsTransforms = new List<Transform>();
    [Range(0, 1)][SerializeField] private float beadPositionOffset = 0f;
    [SerializeField] private Transform playerParentTransform = null;
    [SerializeField] private PlayerBeadFollower lastPlayerBeadFollower = null;
    [SerializeField] private PlayerBeadsTweener playerBeadsTweener = null;
    #endregion

    #region Delegates
    private delegate void PlayerBeadsMovementMechanism();

    private PlayerBeadsMovementMechanism playerBeadsMovementMechanism = null;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        UpdatePlayerLevelIndicatorTMP();
        EnablePlayerBeadsMovementMechanism(false);
    }

    private void Update()
    {
        if (playerBeadsMovementMechanism != null)
        {
            playerBeadsMovementMechanism();
        }
    }
    #endregion

    #region Getter And Setter
    public int GetPlayerLevel { get => playerLevel; }
    #endregion

    #region Private Core Functions
    private void PlayerBeadsMovementCore()
    {
        for (int i = 0; i < playerBeadsTransforms.Count; i++)
        {
            playerBeadsTransforms[i].position = Vector3.SlerpUnclamped(playerBeadsTransforms[i].position, playerBeadsTransforms[i].GetComponent<PlayerBeadFollower>().TargetTransform.position, beadsFollowSpeed * Time.deltaTime);
        }
    }
    #endregion

    #region Public Core Functions
    public void EnablePlayerBeadsMovementMechanism(bool value)
    {
        if (value)
        {
            playerBeadsMovementMechanism += PlayerBeadsMovementCore;
        }
        else
        {
            playerBeadsMovementMechanism -= PlayerBeadsMovementCore;
        }
    }

    public void UpdatePlayerLevelIndicatorTMP()
    {
        playerLevelIndicatorTMP.SetText(playerLevel.ToString());
    }

    public void AddBeadToPlayerTail(Transform newBeadTransform)
    {
        playerLevel++;
        UpdatePlayerLevelIndicatorTMP();
        newBeadTransform.parent = playerParentTransform;
        newBeadTransform.position = new Vector3(lastPlayerBeadFollower.transform.position.x, 0, lastPlayerBeadFollower.transform.position.z + beadPositionOffset);
        
        if (newBeadTransform.TryGetComponent<PlayerBeadFollower>(out PlayerBeadFollower playerBeadFollower))
        {
            playerBeadFollower.TargetTransform = lastPlayerBeadFollower.GetBeadTailTransform;
            lastPlayerBeadFollower = playerBeadFollower;
            playerBeadsTransforms.Add(newBeadTransform);
        }
    }

    public void TweenAllBeads()
    {
        foreach (Transform t in playerBeadsTransforms)
        {
            playerBeadsTweener.TweenBeads(playerBeadsTransforms);
        }
    }

    //Remove Beads From Player Tail
    public void RemoveBeadFromEnd()
    {
        Transform temp = playerBeadsTransforms[playerBeadsTransforms.Count - 1];
        Destroy(temp.gameObject);
        playerBeadsTransforms.RemoveAt(playerBeadsTransforms.Count - 1);
    }
    #endregion
}
