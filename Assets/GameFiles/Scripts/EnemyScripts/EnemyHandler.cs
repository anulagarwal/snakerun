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
    [SerializeField] private bool shouldSpawn = true;


    [Header("Components Reference")]
    [SerializeField] private TextMeshPro enemyLevelTMP = null;
    [SerializeField] private MeshRenderer enemyLevelBGMesh = null;

    [SerializeField] private GameObject beadPrefab = null;
    [SerializeField] private GameObject splashVFXObj = null;
    [SerializeField] private List<Transform> beads = new List<Transform>();
    [SerializeField] private Transform triggerBoxTransform = null;

    [Header("Bead Colors Attributes")]
    [SerializeField] public Color32 litColorEatable;
    [SerializeField] public Color32 shadedColorEatable;
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

        if (shouldSpawn)
        {
            SpawnBeads();
        }
        else
        {
            enemyLevel = beadSpawnAmount;
            enemyLevelTMP.SetText("LVL "+ enemyLevel.ToString());
            ChangeEnemyBeadColors();
        }
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

        triggerBoxTransform.localScale = new Vector3(1, 1,enemyLevel/4f); 
    }
    #endregion

    #region Public Core Functions
    public void PlayerCollisionRules(int playerLevel)
    {
        if (playerLevel >= enemyLevel)
        {
           
            if (shouldSpawn)
            {
                foreach (Transform t in beads)
                {
                    PlayerSingleton.Instance.GetPlayerBeadsManager.AddBeadToPlayerTailFromEnemies(t,this);
                }
            }
            else
            {
                for(int i = 0; i< enemyLevel; i++)
                {
                    PlayerSingleton.Instance.GetPlayerBeadsManager.AddBeadToPlayerTailFromEnemies(Instantiate(beadPrefab, spawnPosition, Quaternion.identity, this.transform).gameObject.transform,this);
                }
            }
            beads.Clear();
            PlayerSingleton.Instance.GetPlayerBeadsManager.UpdateAllBeadsColor();
            PlaySplashVFX();

            EnemyManager.Instance.Updatecolor();
            Destroy(this.gameObject);
        }
        else
        {
            PlayerSingleton.Instance.GameOver();
            return;
        }
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
            splashVFXObj.GetComponent<ParticleSystemRenderer>().material.color = beadColors.litColor;
            splashVFXObj.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material.color = beadColors.litColor;
            enemyLevelBGMesh.material.color = litColorEatable;

        }
        else
        {
            beadColors.litColor = litColorDanger;
            beadColors.shadedColor = shadedColorDanger;
            splashVFXObj.GetComponent<ParticleSystemRenderer>().material.color = beadColors.litColor;
            splashVFXObj.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material.color = beadColors.litColor;
            enemyLevelBGMesh.material.color = litColorDanger;

        }

        foreach (Transform t in beads)
        {
            t.GetComponent<EnemyBeadColorUpdater>().UpdateColor(beadColors);
        }
    }
    #endregion
}
