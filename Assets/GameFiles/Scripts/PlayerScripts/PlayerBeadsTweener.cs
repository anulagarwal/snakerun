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
    #endregion

    #region MonoBehaviour Functions
    #endregion

    #region Private Core Functions
    private void Tween(Transform t)
    {
        targetScale = Vector3.one * scaleUpMultiplier;
        t.DOPunchScale(targetScale, tweenTime, 1, .2f);
    }
    #endregion

    #region Public core functions
    public void TweenBeads(List<Transform> transforms)
    {
        CancelInvoke();
        tweenableObjects = transforms;
        tweenObjActiveIndex = 0;

        InvokeRepeating("TweenOnDelay", 0.01f, tweenDelay);
    }
    #endregion

    #region Invoke Functions
    private void TweenOnDelay()
    {
        Tween(tweenableObjects[tweenObjActiveIndex]);

        tweenObjActiveIndex++;

        if (tweenObjActiveIndex >= tweenableObjects.Count)
        {
            tweenObjActiveIndex = 0;
            CancelInvoke();
        }
    }
    #endregion
}