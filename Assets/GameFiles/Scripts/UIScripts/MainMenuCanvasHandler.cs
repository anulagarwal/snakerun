using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvasHandler : MonoBehaviour
{
    #region Properties
    #endregion

    #region MonoBehaviour Functions
    #endregion

    #region Btn Events Functions
    public void OnClick_PlayBtn()
    {
        UIPackSingleton.Instance.SwitchUICanvas(UICanvas.GameplayCanvas);
        PlayerSingleton.Instance.GetPlayerMovementController.EnablePlayerMovement(true);
    }

    public void OnClick_QuitBtn()
    {
        Application.Quit();
    }
    #endregion
}
