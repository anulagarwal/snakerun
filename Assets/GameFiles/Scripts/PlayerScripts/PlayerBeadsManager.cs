using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using Unity.VisualScripting;

public class PlayerBeadsManager : MonoBehaviour
{
    #region Properties
    [Header("Player Level Indicator Attributes")]
    [SerializeField] private int playerLevel = 0;
    [SerializeField] private TextMeshPro playerLevelIndicatorTMP = null;

    [Header("Player Beads Attributes")]
    [SerializeField] private float beadsFollowSpeed = 0f;
    [SerializeField] private List<Transform> playerBeadsTransforms = new List<Transform>();
    [SerializeField] private List<PlayerBeadColorUpdater> playerBeadColorUpdaters = new List<PlayerBeadColorUpdater>();
    [Range(0, 1)][SerializeField] private float beadPositionOffset = 0f;
    [SerializeField] private Transform playerParentTransform = null;
    [SerializeField] private PlayerBeadFollower lastPlayerBeadFollower = null;
    [SerializeField] private PlayerBeadsTweener playerBeadsTweener = null;
    [SerializeField] private GameObject playerBeadPrefab = null;

    [Header("Beads Color Attributes")]
    [SerializeField] private Color32 litColor;
    [SerializeField] private Color32 shadedColor;

    private List<BeadColors> playerBeadColorData = new List<BeadColors>();
    private CharacterController tailCharacterController = null;
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
        BeadsInitialColorSetup();
        PlayerBeadFollowType = BeadFollowType.Head;
    }

    private void Update()
    {
        if (playerBeadsMovementMechanism != null && (!PlayerSingleton.Instance.ForceStopPlayerMovement || PlayerSingleton.Instance.GetPlayerSlinkyMovementController.enabled))
        {
            playerBeadsMovementMechanism();
        }
    }
    #endregion

    #region Getter And Setter
    public int GetPlayerLevel { get => playerLevel; }

    public BeadFollowType PlayerBeadFollowType { get; set; }
    #endregion

    #region Private Core Functions
    private void BeadsInitialColorSetup()
    {
        int index = 0;
        foreach (PlayerBeadColorUpdater t in playerBeadColorUpdaters)
        {
            playerBeadColorData.Add(new BeadColors());
            playerBeadColorData[index].litColor = litColor;
            playerBeadColorData[index].shadedColor = shadedColor;
            t.UpdateColor(playerBeadColorData[index].litColor, playerBeadColorData[index].shadedColor);
            index++;
        }
    }

    private void PlayerBeadsMovementCore()
    {
        for (int i = 0; i < playerBeadsTransforms.Count; i++)
        {
            if (PlayerBeadFollowType == BeadFollowType.Head)
            {
                playerBeadsTransforms[i].position = Vector3.SlerpUnclamped(playerBeadsTransforms[i].position, playerBeadsTransforms[i].GetComponent<PlayerBeadFollower>().NormalMovementTargetTransform.position, beadsFollowSpeed * Time.deltaTime);
            }
            else if (PlayerBeadFollowType == BeadFollowType.Tail)
            {
                playerBeadsTransforms[i].position = Vector3.SlerpUnclamped(playerBeadsTransforms[i].position, playerBeadsTransforms[i].GetComponent<PlayerBeadFollower>().SlinkyMovementTargetTransform.position, beadsFollowSpeed * Time.deltaTime);
            }
        }
    }

    private void AddBeadToPlayerTail(Transform beadTransform)
    {
        playerLevel++;
        UpdatePlayerLevelIndicatorTMP();
        beadTransform.parent = playerParentTransform;
        beadTransform.position = new Vector3(lastPlayerBeadFollower.transform.position.x, 0, lastPlayerBeadFollower.transform.position.z + beadPositionOffset);

        if (beadTransform.TryGetComponent<PlayerBeadFollower>(out PlayerBeadFollower playerBeadFollower))
        {
            playerBeadFollower.NormalMovementTargetTransform = lastPlayerBeadFollower.GetBeadTailTransform;
            lastPlayerBeadFollower = playerBeadFollower;
            playerBeadsTransforms.Add(beadTransform);
            playerBeadColorUpdaters.Add(beadTransform.GetComponent<PlayerBeadColorUpdater>());
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

    public void AddBeadToPlayerTailFromEnemies(Transform newBeadTransform)
    {
        playerLevel++;
        UpdatePlayerLevelIndicatorTMP();
        newBeadTransform.parent = playerParentTransform;
        newBeadTransform.position = new Vector3(lastPlayerBeadFollower.transform.position.x, 0, lastPlayerBeadFollower.transform.position.z + beadPositionOffset);

        if (newBeadTransform.TryGetComponent<PlayerBeadFollower>(out PlayerBeadFollower playerBeadFollower))
        {
            playerBeadFollower.NormalMovementTargetTransform = lastPlayerBeadFollower.GetBeadTailTransform;
            lastPlayerBeadFollower = playerBeadFollower;
            playerBeadsTransforms.Add(newBeadTransform);
            playerBeadColorUpdaters.Add(newBeadTransform.GetComponent<PlayerBeadColorUpdater>());
        }

        playerBeadColorData.Insert(0, newBeadTransform.GetComponent<EnemyBeadColorUpdater>().StartColors);
    }

    public void SpawnAndUpdateColorOfFrontBeads(int count, Color32 litColor, Color32 shadedColor)
    {
        RemoveCharacterControllerFromPreviousActiveTail();

        BeadColors newBeadColors = new BeadColors();
        newBeadColors.litColor = litColor;
        newBeadColors.shadedColor = shadedColor;



        while (count > 0)
        {
            AddBeadToPlayerTail(Instantiate(playerBeadPrefab, Vector3.zero, Quaternion.identity).transform);
            count--;
            playerBeadColorData.Insert(0, newBeadColors);
        }

        AddCharacterControllerToPlayerTail();
    }

    public void UpdateAllBeadsColor()
    {
        for (int i = 0; i < playerBeadColorData.Count; i++)
        {
            playerBeadColorUpdaters[i].UpdateColor(playerBeadColorData[i].litColor, playerBeadColorData[i].shadedColor);
        }

        TweenAllBeads();
    }

    public void TweenAllBeads()
    {
        playerBeadsTweener.TweenBeads(playerBeadsTransforms);
    }

    //Remove Beads From Player Tail
    public void RemoveBeadFromEnd()
    {
        Transform temp = playerBeadsTransforms[playerBeadsTransforms.Count - 1];
        Destroy(temp.gameObject);
        playerBeadsTransforms.RemoveAt(playerBeadsTransforms.Count - 1);
        playerLevel--;
        lastPlayerBeadFollower = playerBeadsTransforms[playerBeadsTransforms.Count - 1].GetComponent<PlayerBeadFollower>();
        UpdatePlayerLevelIndicatorTMP();
    }

    public void AddCharacterControllerToPlayerTail()
    {
        lastPlayerBeadFollower.AddComponent<CharacterController>();
        tailCharacterController = lastPlayerBeadFollower.GetComponent<CharacterController>();
        tailCharacterController.center = new Vector3(0f, 0.5f, 0f);
        tailCharacterController.radius = 0.5f;
        tailCharacterController.height = 1f;

        PlayerSingleton.Instance.GetPlayerSlinkyMovementController.SetPlayerTailCC = tailCharacterController;
    }

    public void RemoveCharacterControllerFromPreviousActiveTail()
    {
        Destroy(lastPlayerBeadFollower.GetComponent<CharacterController>());
    }

    public void ConnectBeadHeadForSlinkyMovement()
    {
        for (int i = playerBeadsTransforms.Count - 2; i > -1; i--)
        {
            playerBeadsTransforms[i].GetComponent<PlayerBeadFollower>().SlinkyMovementTargetTransform = playerBeadsTransforms[i + 1].GetComponent<PlayerBeadFollower>().GetBeadHeadTransform;
        }
    }
    #endregion
}
