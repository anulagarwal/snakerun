using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float spawnOffset = 0f;
    [SerializeField] private int beadSpawnAmount = 0;

    [Header("Components Reference")]
    [SerializeField] private TextMeshPro enemyLevelTMP = null;
    [SerializeField] private GameObject beadPrefab = null;
    [SerializeField] private GameObject splashVFXObj = null;
    [SerializeField] private List<Transform> beads = new List<Transform>();

    [Header("Bead Colors Attributes")]
    [SerializeField] private Color32 litColor;
    [SerializeField] private Color32 shadedColor;

    private BeadColors startColors = new BeadColors();
    private Vector3 spawnPosition = Vector3.zero;
    private int enemyLevel = 0;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        spawnPosition = transform.position;

        SpawnBeads();
    }
    #endregion

    #region Private Core Functions
    private void SpawnBeads()
    {
        startColors.litColor = litColor;
        startColors.shadedColor = shadedColor;

        for (int i = 0; i < beadSpawnAmount; i++)
        {
            beads.Add(Instantiate(beadPrefab, spawnPosition, Quaternion.identity, this.transform).gameObject.transform);
            spawnPosition.z -= spawnOffset;
        }

        foreach(Transform t in beads)
        {
            if (t.TryGetComponent<EnemyBeadColorUpdater>(out EnemyBeadColorUpdater enemyBeadColorUpdater))
            {
                enemyBeadColorUpdater.StartColors = startColors;
                enemyBeadColorUpdater.enabled = true;
            }            
        }

        //Set Enemy Level
        enemyLevel = beads.Count;
        enemyLevelTMP.SetText(enemyLevel.ToString());
    }
    #endregion

    #region Public Core Functions
    public void PlayerCollisionRules(int playerLevel)
    {
        if (playerLevel > enemyLevel)
        {
            foreach (Transform t in beads)
            {
                PlayerSingleton.Instance.GetPlayerBeadsManager.AddBeadToPlayerTailFromEnemies(t);
            }
            beads.Clear();
            PlayerSingleton.Instance.GetPlayerBeadsManager.UpdateAllBeadsColor();
            PlaySplashVFX();
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }

        PlayerSingleton.Instance.GetPlayerBeadsManager.AddCharacterControllerToPlayerTail();
    }

    public void PlaySplashVFX()
    {
        splashVFXObj.SetActive(true);
        splashVFXObj.transform.parent = null;
        Destroy(splashVFXObj, 5f);
    }
    #endregion
}
