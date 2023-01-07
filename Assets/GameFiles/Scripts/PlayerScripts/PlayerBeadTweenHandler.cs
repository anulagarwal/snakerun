using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeadTweenHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float tweenSpeed = 0f;
    [SerializeField] private float scaleMultiplier = 0f;

    private Vector3 targetScale = Vector3.zero;
    private bool finalTween = false;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        targetScale = Vector3.one * scaleMultiplier;
    }

    private void Update()
    {
        if (transform.localScale.x < scaleMultiplier)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, Time.deltaTime * tweenSpeed);
        }
        else
        {
            if (finalTween)
            {
                this.enabled = false;
            }
            else
            {
                finalTween = true;
                targetScale = Vector3.one;
            }
        }
    }
    #endregion
}
