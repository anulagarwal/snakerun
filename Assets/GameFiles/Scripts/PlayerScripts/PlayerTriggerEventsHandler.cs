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
            PlayerSingleton.Instance.DisableNormalMovement();

            //Invoke("Invoke_ReleaseBeadsForFinalPush", 2f);
            //UIPackSingleton.Instance.SwitchUICanvas(UICanvas.GameOverCanvas);
        }
        else if (other.gameObject.tag == "Jumper")
        {
            PlayerSingleton.Instance.GetPlayerMovementController.Jump();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            SoundManager.Instance.PlaySound(SoundType.Pop);

            other.gameObject.transform.parent.transform.parent.GetComponent<EnemyHandler>().PlayerCollisionRules(PlayerSingleton.Instance.GetPlayerBeadsManager.GetPlayerLevel);
        }
        else if (other.gameObject.tag == "Climber")
        {
            PlayerSingleton.Instance.GetPlayerMovementController.SwitchCrawlDirection(SnakeCrawlDirection.Up);
        }
        else if (other.gameObject.tag == "MoveForward")
        {

        }
        else if (other.gameObject.tag == "Obstacle")
        {
            PlayerSingleton.Instance.GetPlayerMovementController.KnockBackPlayer();
            //other.gameObject.GetComponent<ObstacleHandler>().CheckForCollisionRules(PlayerSingleton.Instance.GetPlayerBeadsManager.GetPlayerLevel);
        }
        else if (other.gameObject.tag == "ColorBead")
        {
            SoundManager.Instance.PlaySound(SoundType.Pop);

            other.gameObject.GetComponent<ColorBeadHandler>().AddColorBeadToPlayerTrail();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "SlinkyMovementTrigger")
        {
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
        else if (other.gameObject.tag == "EatTrigger")
        {
            PlayerSingleton.Instance.DisableHead();
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

    #region Invoke Functions
    private void Invoke_ReleaseBeadsForFinalPush()
    {
        PlayerSingleton.Instance.GetPlayerBeadsManager.ReleaseBeadsForFinalPush();
    }
    #endregion
}
//
