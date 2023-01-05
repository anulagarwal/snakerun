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
    [SerializeField] private List<Transform> beads = new List<Transform>();

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
        for (int i = 0; i < beadSpawnAmount; i++)
        {
            beads.Add(Instantiate(beadPrefab, spawnPosition, Quaternion.identity, this.transform).gameObject.transform);
            spawnPosition.z -= spawnOffset;
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
                PlayerSingleton.Instance.GetPlayerBeadsManager.AddBeadToPlayerTail(t);
            }
            beads.Clear();
            PlayerSingleton.Instance.GetPlayerBeadsManager.TweenAllBeads();
            Destroy(this.gameObject);
        }
        else
        {
            print("Game Over!");
        }
    }
    #endregion
}
