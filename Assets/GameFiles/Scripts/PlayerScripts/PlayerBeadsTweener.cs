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
        StartCoroutine(TweenOnDelay(transforms));
    }
    #endregion

    #region Coroutines
    private IEnumerator TweenOnDelay(List<Transform> transforms)
    {
        foreach (Transform t in transforms)
        {
            Tween(t);

            yield return new WaitForSeconds(tweenDelay);
        }

        StopAllCoroutines();
    }
    #endregion
}
