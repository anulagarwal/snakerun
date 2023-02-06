using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerBeadsTweener : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float tweenDelay = 0f;
    [SerializeField] private float tweenTime = 0f;
    [SerializeField] private float scaleUpMultiplier = 0f;

    private List<Transform> tweenableObjects = new List<Transform>();
    private int tweenObjActiveIndex = 0;
    private Vector3 defaultScale = Vector3.one;
    private Vector3 targetScale = Vector3.one;
    private float nextTimeToTween = 0;
    private int activeBeadIndex = 0;
    private PlayerBeadsManager playerBeadsManager = null;
    #endregion

    #region Delegates
    private delegate void TweenBeadsWave();

    private TweenBeadsWave tweenBeadsWave = null;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        nextTimeToTween = Time.time;
        playerBeadsManager = PlayerSingleton.Instance.GetPlayerBeadsManager;
        ActivateBeadsTweening(false);
    }

    private void Update()
    {
        if (tweenBeadsWave != null)
        {
            tweenBeadsWave();
        }
    }
    #endregion

    #region Private Core Functions
    private void TweenAllBeads()
    {
        if (nextTimeToTween <= Time.time)
        {
            targetScale = Vector3.one * scaleUpMultiplier;
            playerBeadsManager.GetPlayerBeadsTransforms[activeBeadIndex].DOPunchScale(targetScale, tweenTime, 1, .2f);
            activeBeadIndex++;
            if (activeBeadIndex >= playerBeadsManager.GetPlayerBeadsTransforms.Count)
            {
                ActivateBeadsTweening(false);
            }
            nextTimeToTween = Time.time + tweenDelay;
        }
    }

    private void ResetScale()
    {
        foreach (Transform t in playerBeadsManager.GetPlayerBeadsTransforms)
        {
            t.localScale = Vector3.one;
        }
    }
    #endregion

    #region Public core functions
    public void ActivateBeadsTweening(bool value)
    {
        if (value)
        {
            tweenBeadsWave = null;
            activeBeadIndex = 0;
            ResetScale();
            tweenBeadsWave += TweenAllBeads;
        }
        else
        {
            tweenBeadsWave = null;
            activeBeadIndex = 0;
            ResetScale();
        }
    }
    #endregion
}