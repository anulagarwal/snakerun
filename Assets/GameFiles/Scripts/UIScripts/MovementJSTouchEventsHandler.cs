using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJSTouchEventsHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    #region Interface Functions
    public void OnPointerDown(PointerEventData eventData)
    {
        print("Working");
        PlayerSingleton.Instance.GetPlayerMovementController.EnableUndergroundMovement(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerSingleton.Instance.GetPlayerMovementController.EnableUndergroundMovement(false);
    }
    #endregion
}
