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
    private void Start()
    {
        beadMat = meshRenderer.materials[0];
        beadMat.SetColor("_LitColor", startColors.litColor);
        beadMat.SetColor("_ShadedColor", startColors.shadedColor);
    }
    #endregion

    #region Getter And Setter
    public BeadColors StartColors { get => startColors; set => startColors = value; }
    #endregion

    #region Public Core Functions
    #endregion
}
