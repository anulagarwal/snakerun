using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJSTouchEventsHandler : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    public static MovementJSTouchEventsHandler Instance = null;


    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }



    #region Interface Functions
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!PlayerSingleton.Instance.GetPlayerSlinkyMovementHandler.IsSlinkyMovementActive)
        {
          //  PlayerSingleton.Instance.GetPlayerBeadsManager.HideMeshRenderer(true);
          //  PlayerSingleton.Instance.EnablePlayerHiddenTriggerBox(true);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!PlayerSingleton.Instance.GetPlayerSlinkyMovementHandler.IsSlinkyMovementActive)
        {
          //  PlayerSingleton.Instance.GetPlayerBeadsManager.HideMeshRenderer(false);
         //   PlayerSingleton.Instance.EnablePlayerHiddenTriggerBox(false);
        }
    }
    #endregion
}
