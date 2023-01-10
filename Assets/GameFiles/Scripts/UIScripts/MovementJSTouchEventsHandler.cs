using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJSTouchEventsHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    #region Interface Functions
    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerSingleton.Instance.ForceStopPlayerMovement = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerSingleton.Instance.ForceStopPlayerMovement = true;
    }
    #endregion
}
