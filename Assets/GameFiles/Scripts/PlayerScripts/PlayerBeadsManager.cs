using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using Unity.VisualScripting;

public class PlayerBeadsManager : MonoBehaviour
{
    #region Properties
    [Header("Final Push Attributes")]
    [SerializeField] private float pushDelay = 0f; 

    [Header("Player Level Indicator Attributes")]
    [SerializeField] private int playerLevel = 0;
    [SerializeField] private int startLength = 0;
    [Range(0, 1f)][SerializeField] private float beadSpawnOffsetDistance = 0f;
    [SerializeField] private TextMeshPro playerLevelIndicatorTMP = null;

    [Header("Components Reference")]
    [SerializeField] private Transform mainParent = null; 

    [Header("Player Beads Attributes")]
    [SerializeField] private float beadsFollowSpeed = 0f;
    [SerializeField] private List<Transform> beadTailTransforms = new List<Transform>();
    [SerializeField] private List<Transform> playerBeadsTransforms = new List<Transform>();
    [SerializeField] private List<PlayerBeadColorUpdater> playerBeadColorUpdaters = new List<PlayerBeadColorUpdater>();
    [Range(0, 1)][SerializeField] private float beadPositionOffset = 0f;
    [SerializeField] private Transform playerParentTransform = null;
    [SerializeField] private PlayerBeadsTweener playerBeadsTweener = null;
    [SerializeField] private GameObject playerBeadPrefab = null;

    [Header("Beads Color Attributes")]
    [SerializeField] private Color32 litColor;
    [SerializeField] private Color32 shadedColor;

    private List<BeadColors> playerBeadColorData = new List<BeadColors>();
    private CharacterController tailCharacterController = null;
    private Vector3 spawnPosition = Vector3.zero;
    private PlayerBeadFollower lastPlayerBeadFollower = null;
    #endregion

    #region Delegates
    private delegate void PlayerBeadsMovementMechanism();

    private PlayerBeadsMovementMechanism playerBeadsMovementMechanism = null;
    #endregion

    #region MonoBehaviour Functions
    private void Awake()
    {
        InitialSetup();
    }

    private void Start()
    {
        UpdatePlayerLevelIndicatorTMP();
        EnablePlayerBeadsMovementMechanism(false);
        BeadsInitialColorSetup();

        SwitchPlayerActiveMovementDirection(BeadFollowType.Head);
    }

    private void LateUpdate()
    {
        if (playerBeadsMovementMechanism != null)
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
    private void InitialSetup()
    {
        SpawnNewBead(startLength);
    }

    private void SpawnNewBead(int spawnAmount)
    {
        while (spawnAmount > 0)
        {
            playerBeadsTransforms.Add(Instantiate(playerBeadPrefab, spawnPosition, Quaternion.identity, mainParent).transform);
            playerBeadColorUpdaters.Add(playerBeadsTransforms[playerBeadsTransforms.Count - 1].GetComponent<PlayerBeadColorUpdater>());
            playerBeadsTransforms[playerBeadsTransforms.Count - 1].GetComponent<PlayerBeadFollower>().NormalMovementTargetTransform = beadTailTransforms[beadTailTransforms.Count - 1];
            beadTailTransforms.Add(playerBeadsTransforms[playerBeadsTransforms.Count - 1].GetComponent<PlayerBeadFollower>().GetBeadTailTransform);
            spawnAmount--;
            spawnPosition.z -= beadSpawnOffsetDistance;
        }
        lastPlayerBeadFollower = playerBeadsTransforms[playerBeadsTransforms.Count - 1].GetComponent<PlayerBeadFollower>();
    }

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
                if (playerBeadsTransforms[i].TryGetComponent<PlayerBeadFollower>(out PlayerBeadFollower playerBeadFollower))
                {
                    if (!playerBeadFollower.IsTail)
                    {
                        playerBeadsTransforms[i].position = Vector3.SlerpUnclamped(playerBeadsTransforms[i].position, playerBeadFollower.SlinkyMovementTargetTransform.position, beadsFollowSpeed * Time.smoothDeltaTime);
                    }
                }
            }
        }
    }

    private void AddBeadToPlayerTail(Transform beadTransform)
    {
        playerLevel++;
        UpdatePlayerLevelIndicatorTMP();
        beadTransform.parent = playerParentTransform;
        beadTransform.position = new Vector3(lastPlayerBeadFollower.transform.position.x, 0, lastPlayerBeadFollower.transform.position.z + beadPositionOffset);

        playerBeadsTransforms[playerBeadsTransforms.Count - 1].GetComponent<PlayerBeadFollower>().IsTail = false;
        if (beadTransform.TryGetComponent<PlayerBeadFollower>(out PlayerBeadFollower playerBeadFollower))
        {
            playerBeadFollower.NormalMovementTargetTransform = lastPlayerBeadFollower.GetBeadTailTransform;
            lastPlayerBeadFollower = playerBeadFollower;
            playerBeadsTransforms.Add(beadTransform);
            playerBeadColorUpdaters.Add(beadTransform.GetComponent<PlayerBeadColorUpdater>());
            playerBeadFollower.IsTail = true;
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
        playerBeadsTransforms[playerBeadsTransforms.Count - 1].GetComponent<PlayerBeadFollower>().IsTail = false;
        playerLevel++;
        UpdatePlayerLevelIndicatorTMP();
        newBeadTransform.parent = playerParentTransform;
        newBeadTransform.position = new Vector3(lastPlayerBeadFollower.transform.position.x, 0, lastPlayerBeadFollower.transform.position.z + beadPositionOffset);

        if (newBeadTransform.TryGetComponent<PlayerBeadFollower>(out PlayerBeadFollower playerBeadFollower))
        {
            playerBeadFollower.IsTail = true;
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
            AddBeadToPlayerTail(Instantiate(playerBeadPrefab, new Vector3(lastPlayerBeadFollower.transform.position.x, 0, lastPlayerBeadFollower.transform.position.z + beadPositionOffset), Quaternion.identity).transform);
            count--;
            playerBeadColorData.Insert(0, newBeadColors);
        }

        EnemyManager.Instance.Updatecolor();
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

        PlayerSingleton.Instance.GetPlayerMovementController.SetTailCharacterController = tailCharacterController;
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

    public void SwitchPlayerActiveMovementDirection(BeadFollowType followType)
    {
        PlayerBeadFollowType = followType;

        if (PlayerBeadFollowType == BeadFollowType.Tail)
        {
            ConnectBeadHeadForSlinkyMovement();
        }
    }

    public void ReleaseBeadsForFinalPush()
    {
        StartCoroutine(FinalPush());
    }

    public void ReduceBeadsCount()
    {
        if (playerLevel > 1)
        {
            playerLevel--;
        }
        else
        {
            UIPackSingleton.Instance.SwitchUICanvas(UICanvas.GameOverCanvas, GameOverStatus.Victory);
        }
    }

    public void ReleaseAllBeads()
    {
        foreach (Transform t in playerBeadsTransforms)
        {
            t.GetComponent<Collider>().isTrigger = false;
            t.AddComponent<Rigidbody>();
            t.parent = null;
        }
    }
    #endregion

    #region Coroutine
    private IEnumerator FinalPush()
    {
        for (int i = playerBeadsTransforms.Count - 1; i >= 0; i--)
        {
            playerBeadsTransforms[i].GetComponent<BeadFinalPushHandler>().enabled = true;
            yield return new WaitForSeconds(pushDelay);
        }
        Destroy(mainParent.gameObject);

        StopAllCoroutines();
    }
    #endregion
}
//