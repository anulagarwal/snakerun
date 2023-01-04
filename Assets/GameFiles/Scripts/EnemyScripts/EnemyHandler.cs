using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHandler : MonoBehaviour
{
    #region Properties
    [Header("Components Reference")]
    [SerializeField] private TextMeshPro enemyLevelTMP = null;
    [SerializeField] private List<Transform> beads = new List<Transform>();

    private int enemyLevel = 0;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
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
