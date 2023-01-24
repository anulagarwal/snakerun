 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIPackSingleton : MonoBehaviour
{
    #region Properties
    public static UIPackSingleton Instance = null;

    [Header("UI Canvas Reference")]
    [SerializeField] private GameObject mainMenuCanvasObj = null;
    [SerializeField] private GameObject gameplayCanvasObj = null;
    [SerializeField] private GameObject gameOverCanvasObj = null;

    [Header("GameOver Screen Components Reference")]
    [SerializeField] private GameObject victoryPanelObj = null;
    [SerializeField] private GameObject defeatPanelObj = null;

    [Header("Components Reference")]
    [SerializeField] private List<TextMeshProUGUI> levelTexts = new List<TextMeshProUGUI>();
    [SerializeField] private Transform radial;
    [SerializeField] private Transform smile;
    [SerializeField] private Transform sad;



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

    private void Start()
    {
    }
    #endregion

    #region Getter And Setter
    public GameplayCanvasHandler GetGameplayCanvasHandler { get => gameplayCanvasHandler; }
    #endregion

    #region Private Core Functions
    public void UpdateLevelText(int l)
    {
        foreach (TextMeshProUGUI tmp in levelTexts)
        {
            tmp.SetText("Level " + l);
        }
    }
    #endregion

    #region Public Core Functions
    public void SwitchUICanvas(UICanvas activeCanvas, GameOverStatus status = GameOverStatus.None)
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
                EnemyManager.Instance.StopAllEnemies();

                mainMenuCanvasObj.SetActive(false);
                gameplayCanvasObj.SetActive(false);
                gameOverCanvasObj.SetActive(true);

                if(status == GameOverStatus.Victory)
                {
                    GameManager.Instance.Win();
                    victoryPanelObj.SetActive(true);
                    defeatPanelObj.SetActive(false);
                    radial.transform.localScale = Vector3.zero;
                    smile.transform.localScale = Vector3.zero;
                    radial.DOScale(Vector3.one, 0.5f);
                    radial.DORotate(new Vector3(0, -180f, 0), 0.5f).SetLoops(-1,LoopType.Yoyo);
                    smile.DOScale(Vector3.one, 0.3f);
                }
                else if (status == GameOverStatus.Defeat)
                {
                    victoryPanelObj.SetActive(false);
                    defeatPanelObj.SetActive(true);
                    sad.transform.localScale = Vector3.zero;
                    sad.DOScale(Vector3.one, 0.5f);

                }
                break;
        }
    }
    #endregion

    public void OnClick_Continue()
    {
        GameManager.Instance.ChangeLevel();
    }
}
