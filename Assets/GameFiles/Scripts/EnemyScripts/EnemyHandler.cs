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
    [SerializeField] private Transform triggerBoxTransform = null;

    [Header("Bead Colors Attributes")]
    [SerializeField] private Color32 litColorEatable;
    [SerializeField] private Color32 shadedColorEatable;
    [SerializeField] private Color32 litColorDanger;
    [SerializeField] private Color32 shadedColorDanger;

    private BeadColors beadColors = new BeadColors();
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

    #region Getter And Setter
    public int GetEnemyLevel { get => enemyLevel; }
    #endregion

    #region Private Core Functions
    private void SpawnBeads()
    {
        for (int i = 0; i < beadSpawnAmount; i++)
        {
            beads.Add(Instantiate(beadPrefab, spawnPosition, Quaternion.identity, this.transform).gameObject.transform);
            spawnPosition.z -= spawnOffset;
        }

        //Set Enemy Level
        enemyLevel = beads.Count;
        enemyLevelTMP.SetText(enemyLevel.ToString());

        //Change Color
        ChangeEnemyBeadColors();

        triggerBoxTransform.localScale = new Vector3(1, 1,enemyLevel/4f); ;
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

            EnemyManager.Instance.Updatecolor();
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

    public void ChangeEnemyBeadColors()
    {
        if (PlayerSingleton.Instance.GetPlayerBeadsManager.GetPlayerLevel >= enemyLevel)
        {
            beadColors.litColor = litColorEatable;
            beadColors.shadedColor = shadedColorEatable;
        }
        else
        {
            beadColors.litColor = litColorDanger;
            beadColors.shadedColor = shadedColorDanger;
        }

        foreach (Transform t in beads)
        {
            t.GetComponent<EnemyBeadColorUpdater>().UpdateColor(beadColors);
        }
    }
    #endregion
}
