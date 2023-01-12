using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Properties
    public static EnemyManager Instance = null;

    private EnemyHandler[] enemyHandlers = null;
    #endregion

    #region MonoBehaviour Functions
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        enemyHandlers = GameObject.FindObjectsOfType<EnemyHandler>();
    }
    #endregion

    #region Public Core Functions
    public void Updatecolor()
    {
        foreach (EnemyHandler eh in enemyHandlers)
        {
            if (eh != null)
            {
                eh.ChangeEnemyBeadColors();
            }
        }
    }

    public void StopAllEnemies()
    {
        foreach(EnemyHandler eh in enemyHandlers)
        {
            if (eh != null)
            {
                eh.GetComponent<EnemyMovementHandler>().enabled = false;
            }
        }
    }
    #endregion
}
