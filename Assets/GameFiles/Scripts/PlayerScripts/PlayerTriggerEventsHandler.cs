using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerEventsHandler : MonoBehaviour
{
    #region MonoBehaviour Functions
    private void Start()
    {
        IsTriggerEventsActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsTriggerEventsActive)
        {
            if (other.gameObject.tag == "Finish")
            {
                PlatformFinishlineHandler.Instance.PlayConfettiVFX();
                PlayerSingleton.Instance.DisableNormalMovement();
                MovementJSTouchEventsHandler.Instance.enabled = false;
                PlayerSingleton.Instance.GetPlayerBeadsManager.HideMeshRenderer(false);
                PlayerSingleton.Instance.EnablePlayerHiddenTriggerBox(false);
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
            else if (other.gameObject.tag == "SlinkyMovementTrigger")
            {
                PlayerSingleton.Instance.EnablePlayerMovement(true);
                PlayerSingleton.Instance.GetPlayerSlinkyMovementHandler.ActivateSlinkyMovement(PlayerSingleton.Instance.GetPlayerBeadsManager.GetPlayerBeadsTransforms, other.gameObject.GetComponent<SlinkyMovementTriggerHandler>().GetTranslatePoints);
            }
            else if (other.gameObject.tag == "ReverseSlinkyMovementTrigger")
            {
                if (PlayerSingleton.Instance.GetPlayerSlinkyMovementHandler.IsSlinkyMovementActive)
                {
                    PlayerSingleton.Instance.GetPlayerSlinkyMovementHandler.ReverseSlinkyMovement(other.gameObject.GetComponent<SlinkyMovementTriggerHandler>().GetTranslatePoints);
                }
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
            else if (other.gameObject.tag == "EatTrigger")
            {
                PlayerSingleton.Instance.DisableHead();
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

    #region Getter And Setter
    public bool IsTriggerEventsActive { get; set; }
    #endregion

    #region Invoke Functions
    private void Invoke_ReleaseBeadsForFinalPush()
    {
        PlayerSingleton.Instance.GetPlayerBeadsManager.ReleaseBeadsForFinalPush();
    }
    #endregion
}
//
