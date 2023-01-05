using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerEventsHandler : MonoBehaviour
{
    #region MonoBehaviour Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            PlatformFinishlineHandler.Instance.PlayConfettiVFX();
            PlayerSingleton.Instance.DisableMovement();

            UIPackSingleton.Instance.SwitchUICanvas(UICanvas.GameOverCanvas);
        }
        else if (other.gameObject.tag == "Jumper")
        {
            PlayerSingleton.Instance.GetPlayerMovementController.Jump();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHandler>().PlayerCollisionRules(PlayerSingleton.Instance.GetPlayerBeadsManager.GetPlayerLevel);
        }
        else if (other.gameObject.tag == "Climber")
        {
            PlayerSingleton.Instance.GetPlayerMovementController.SwitchCrawlDirection(SnakeCrawlDirection.Up);
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
//
