using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBeadColorUpdater : MonoBehaviour
{
    #region Properties
    [Header("Components Reference")]
    [SerializeField] private MeshRenderer meshRenderer = null;

    private Material beadMat = null;
    private BeadColors startColors = new BeadColors();
    #endregion

    #region MonoBehaviour Functions
    private void Awake()
    {
        beadMat = meshRenderer.materials[0];
    }
    #endregion

    #region Getter And Setter
    public BeadColors StartColors { get => startColors; set => startColors = value; }
    #endregion

    #region Public Core Functions
    public void UpdateColor(BeadColors colors)
    {
        startColors = colors;
        beadMat.SetColor("_LitColor", startColors.litColor);
        beadMat.SetColor("_ShadedColor", startColors.shadedColor);
    }
    #endregion
}
