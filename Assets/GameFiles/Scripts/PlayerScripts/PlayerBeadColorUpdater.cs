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

    private bool litColorChanged = false;
    private bool shadedColorChanged = false;
    private Material beadMat = null;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        NewColors = new BeadColors();
        beadMat = meshRenderer.materials[0];
    }

    //private void Update()
    //{
    //    if (!litColorChanged || !shadedColorChanged)
    //    {
    //        if (!beadMat.GetColor("_LitColor").Equals(NewColors.litColor))
    //        {
    //            beadMat.SetColor("_LitColor", Color32.Lerp(beadMat.GetColor("_LitColor"), NewColors.litColor, Time.deltaTime * transitionSpeed));
    //        }
    //        else if (!beadMat.GetColor("_LitColor").Equals(NewColors.litColor))
    //        {
    //            litColorChanged = true;
    //        }

    //        if (beadMat.GetColor("_ShadedColor").Equals(NewColors.shadedColor))
    //        {
    //            beadMat.SetColor("_ShadedColor", Color32.Lerp(beadMat.GetColor("_ShadedColor"), NewColors.shadedColor, Time.deltaTime * transitionSpeed));
    //        }
    //        else if (beadMat.GetColor("_ShadedColor").Equals(NewColors.shadedColor))
    //        {
    //            shadedColorChanged = true;
    //        }
    //    }
    //    else if (litColorChanged && shadedColorChanged)
    //    {
    //        this.enabled = false;
    //    }
    //}
    #endregion

    #region Getter And Setter
    public BeadColors NewColors { get; set; }
    #endregion

    #region Public Core Functions
    public void UpdateColor(BeadColors colors)
    {
        NewColors = colors;
        beadMat.SetColor("_LitColor", NewColors.litColor);
        beadMat.SetColor("_ShadedColor", NewColors.shadedColor);
        this.enabled = true;
    }
    #endregion
}
