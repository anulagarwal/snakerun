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
    [SerializeField] private GameObject splashVFXObj = null;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        beadStrengthTMP.SetText(strength.ToString());
        SetupColor();
    }
    #endregion

    #region Getter And Setter
    #endregion

    #region Private Core functions
    private void SetupColor()
    {
        meshRenderer.materials[0].SetColor("_LitColor", litColor);
        meshRenderer.materials[0].SetColor("_ShadedColor", shadedColor);
        splashVFXObj.GetComponent<ParticleSystemRenderer>().material.color = litColor;
        splashVFXObj.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().material.color = litColor;
    }
    #endregion

    #region Public Core functions
    public void AddColorBeadToPlayerTrail()
    {
        splashVFXObj.SetActive(true);
        splashVFXObj.transform.parent = null;
        Destroy(splashVFXObj, 5f);

        PlayerSingleton.Instance.GetPlayerBeadsManager.SpawnAndUpdateColorOfFrontBeads(strength, litColor, shadedColor);
        PlayerSingleton.Instance.GetPlayerBeadsManager.UpdateAllBeadsColor();
        //PlayerSingleton.Instance.GetPlayerBeadsManager.TweenAllBeads();
    }
    #endregion
}