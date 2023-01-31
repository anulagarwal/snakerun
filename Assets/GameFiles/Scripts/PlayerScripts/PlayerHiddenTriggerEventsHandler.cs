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
        else if (other.gameObject.tag == "Finish")
        {
            PlatformFinishlineHandler.Instance.PlayConfettiVFX();
            PlayerSingleton.Instance.DisableNormalMovement();
            MovementJSTouchEventsHandler.Instance.enabled = false;
            PlayerSingleton.Instance.GetPlayerBeadsManager.HideMeshRenderer(false);

            //Invoke("Invoke_ReleaseBeadsForFinalPush", 2f);
            //UIPackSingleton.Instance.SwitchUICanvas(UICanvas.GameOverCanvas);
        }
        else if (other.gameObject.tag == "FallStair")
        {
            PlayerSingleton.Instance.GetPlayerBeadsManager.HideMeshRenderer(false);
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
