using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPackSingleton : MonoBehaviour
{
    #region Properties
    public static UIPackSingleton Instance = null;

    [Header("Components Reference")]
    [SerializeField] private GameplayCanvasHandler gameplayCanvasHandler = null;
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
    #endregion

    #region Getter And Setter
    public GameplayCanvasHandler GetGameplayCanvasHandler { get => gameplayCanvasHandler; }
    #endregion
}
