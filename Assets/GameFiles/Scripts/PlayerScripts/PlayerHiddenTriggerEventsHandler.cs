using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHiddenTriggerEventsHandler : MonoBehaviour
{
    #region MonoBehaviour Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Climber")
        {
            PlayerSingleton.Instance.GetPlayerBeadsManager.HideMeshRenderer(false);
            PlayerSingleton.Instance.GetPlayerMovementController.SwitchCrawlDirection(SnakeCrawlDirection.Up);
        }
        else if (other.gameObject.tag == "SlinkyMovementTrigger")
        {
            PlayerSingleton.Instance.GetPlayerBeadsManager.HideMeshRenderer(false);
            if (PlayerSingleton.Instance.GetPlayerMovementController.IsHeadActive)
            {
                PlayerSingleton.Instance.GetPlayerMovementController.SwapActiveCharacterControllerToTail();
                PlayerSingleton.Instance.GetPlayerBeadsManager.SwitchPlayerActiveMovementDirection(BeadFollowType.Tail);
            }
            else
            {
                PlayerSingleton.Instance.GetPlayerMovementController.SwapActiveCharacterControllerToHead();
                PlayerSingleton.Instance.GetPlayerBeadsManager.SwitchPlayerActiveMovementDirection(BeadFollowType.Head);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Climber")
        {
            PlayerSingleton.Instance.GetPlayerMovementController.SwitchCrawlDirection(SnakeCrawlDirection.Forward);
        }
    }
    #endregion
}
