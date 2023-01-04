using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPackSingleton : MonoBehaviour
{
    #region Properties
    public static UIPackSingleton Instance = null;

    [Header("UI Canvas Reference")]
    [SerializeField] private GameObject mainMenuCanvasObj = null;
    [SerializeField] private GameObject gameplayCanvasObj = null;
    [SerializeField] private GameObject gameOverCanvasObj = null;

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

    #region Public Core Functions
    public void SwitchUICanvas(UICanvas activeCanvas)
    {
        switch (activeCanvas)
        {
            case UICanvas.MainMenuCanvas:
                mainMenuCanvasObj.SetActive(true);
                gameplayCanvasObj.SetActive(false);
                gameOverCanvasObj.SetActive(false);
                break;
            case UICanvas.GameplayCanvas:
                mainMenuCanvasObj.SetActive(false);
                gameplayCanvasObj.SetActive(true);
                gameOverCanvasObj.SetActive(false);
                break;
            case UICanvas.GameOverCanvas:
                mainMenuCanvasObj.SetActive(false);
                gameplayCanvasObj.SetActive(false);
                gameOverCanvasObj.SetActive(true);
                break;
        }
    }
    #endregion
}
