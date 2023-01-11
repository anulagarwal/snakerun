using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObstacleHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private int obstacleStrength = 0;

    [Header("Components Reference")]
    [SerializeField] private TextMeshPro obstacleStrengthTMP = null;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        UpdateObstacleStrengthTMP();
    }
    #endregion

    #region Private Core Functions
    private void UpdateObstacleStrengthTMP()
    {
        obstacleStrengthTMP.SetText(obstacleStrength.ToString());
    }
    #endregion

    #region Public Core Functions
    public void CheckForCollisionRules(int playerHealth)
    {
        if (playerHealth <= obstacleStrength)
        {
            print("GameOver!");
        }
        else
        {
            while (obstacleStrength > 0)
            {
                PlayerSingleton.Instance.GetPlayerBeadsManager.RemoveBeadFromEnd();
                obstacleStrength--;
            }

            EnemyManager.Instance.Updatecolor();
            Destroy(this.gameObject);
        }
    }
    #endregion
}
