using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ColorBeadHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private int strength = 0;
    [SerializeField] private Color32 litColor = Color.white;
    [SerializeField] private Color32 shadedColor = Color.white;

    [Header("Components Reference")]
    [SerializeField] private TextMeshPro beadStrengthTMP = null;
    [SerializeField] private MeshRenderer meshRenderer = null;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        beadStrengthTMP.SetText(strength.ToString());
        SetupColor();
    }
    #endregion

    #region Private Core functions
    private void SetupColor()
    {
        meshRenderer.materials[0].SetColor("_LitColor", litColor);
        meshRenderer.materials[0].SetColor("_ShadedColor", shadedColor);
    }
    #endregion

    #region Public Core functions
    public void AddColorBeadToPlayerTrail()
    {
        PlayerSingleton.Instance.GetPlayerBeadsManager.UpdateColorOfFrontBeads(strength, litColor, shadedColor);
    }
    #endregion
}