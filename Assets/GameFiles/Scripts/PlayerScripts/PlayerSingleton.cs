using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
public class PlayerSingleton : MonoBehaviour
{
    #region Properties
    public static PlayerSingleton Instance = null;

    [Header("Attributes")]
    [SerializeField] private float beadReleaseRate = 0f;

    [Header("Components Reference")]
    [SerializeField] private GameObject playerParentObj = null;
    [SerializeField] private GameObject splashVFXObj = null;
    [SerializeField] private PlayerMovementController playerMovementController = null;
    [SerializeField] private PlayerBeadsManager playerBeadsManager = null;
    [SerializeField] private PlayerMoveTowardsTarget playerMoveTowardsTarget = null;
    [SerializeField] private MeshRenderer headMR = null;
    [SerializeField] private GameObject playerLevelCanvasObj = null;
    [SerializeField] private GameObject playerHiddenTriggerBoxObj = null;
    [SerializeField] private GameObject holeObjPrefab = null;

    private int playerBeadIndex = 0;
    #endregion

    #region MonoBehaviour Functions
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        ForceStopPlayerMovement = false;
    }
    #endregion

    #region Getter And Setter
    public PlayerMovementController GetPlayerMovementController { get => playerMovementController; }
    
    public PlayerBeadsManager GetPlayerBeadsManager { get => playerBeadsManager; }

    public bool ForceStopPlayerMovement { get; set; }
    #endregion

    #region Public Core Functions
    public void DisableNormalMovement()
    {
        playerLevelCanvasObj.SetActive(false);
        CMVCManager.Instance.DisableFollow();
        playerMovementController.enabled = false;
        //playerMovementController.EnablePlayerMovement(false);
        playerMoveTowardsTarget.EnableMoveTowards(true);
        playerBeadsManager.enabled = false;

        InvokeRepeating("Invoke_MoveTowardsTarget", 0.2f, beadReleaseRate);
    }

    public void GameOver()
    {
        splashVFXObj.SetActive(true);
        splashVFXObj.transform.parent = null;
        playerBeadsManager.ReleaseAllBeads();
        playerParentObj.SetActive(false);
        Invoke("Invoke_DefeatUI", 1f); 
    }

    public void DisableHead()
    {
        headMR.gameObject.SetActive(false);
    }

    public void EnablePlayerHiddenTriggerBox(bool value)
    {
        Destroy(Instantiate(holeObjPrefab, transform.position, Quaternion.identity), 5f);
        playerHiddenTriggerBoxObj.SetActive(value);
    }
    #endregion

    #region Invoke functions

    public void Invoke_DefeatUI()
    {
        UIPackSingleton.Instance.SwitchUICanvas(UICanvas.GameOverCanvas, GameOverStatus.Defeat);
    }

    private void Invoke_MoveTowardsTarget()
    {
        playerBeadsManager.GetPlayerBeadsTransforms[playerBeadIndex].GetComponent<PlayerMoveTowardsTarget>().enabled = true;

        playerBeadIndex++;
        if (playerBeadIndex >= playerBeadsManager.GetPlayerBeadsTransforms.Count)
        {
            CancelInvoke("Invoke_MoveTowardsTarget");
        }
    }
    #endregion
}
