using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeadColorUpdater : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float transitionSpeed = 0;

    [Header("Components Reference")]
    [SerializeField] private MeshRenderer meshRenderer = null;

    private Material beadMat = null;
    #endregion

    #region MonoBehaviour Functions
    private void Awake()
    {
        beadMat = meshRenderer.materials[0];
    }
    #endregion

    #region Getter And Setter
    #endregion

    #region Public Core Functions
    public void UpdateColor(Color32 litColor, Color32 shadedColor)
    {
        beadMat.SetColor("_LitColor", litColor);
        beadMat.SetColor("_ShadedColor", shadedColor);
    }
    #endregion
}
