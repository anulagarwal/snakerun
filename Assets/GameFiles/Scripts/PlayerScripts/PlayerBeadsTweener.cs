using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeadsTweener : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float tweenDelay = 0f;
    [SerializeField] private float tweenTime = 0f;
    [SerializeField] private float scaleUpMultiplier = 0f;

    private List<Transform> tweenableObjects = new List<Transform>();
    private int tweenObjActiveIndex = 0;
    #endregion

    #region MonoBehaviour Functions
    #endregion

    #region Private Core Functions
    private void Tween(Transform t)
    {
        LeanTween.cancel(t.gameObject);
        t.localScale = Vector3.one;

        LeanTween.scale(t.gameObject, Vector3.one * scaleUpMultiplier, tweenTime).setEasePunch();
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