using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFinishlineHandler : MonoBehaviour
{
    #region Properties
    public static PlatformFinishlineHandler Instance = null;

    [Header("Components Reference")]
    [SerializeField] private GameObject confettiVFXObj = null;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        Instance = this;
    }
    #endregion

    #region Public Core Functions
    public void PlayConfettiVFX()
    {
        confettiVFXObj.SetActive(true);

        Invoke("Invoke_Victory", 3f);
    }
    #endregion

    #region Invoke Functions
    private void Invoke_Victory()
    {
        UIPackSingleton.Instance.SwitchUICanvas(UICanvas.GameOverCanvas, GameOverStatus.Victory);
    }
    #endregion
}
