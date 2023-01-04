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
            PlayerSingleton.Instance.GetPlayerMovementController.EnablePlayerMovement(false);
        }
        else if (other.gameObject.tag == "Jumper")
        {
            PlayerSingleton.Instance.GetPlayerMovementController.Jump();
        }
    }
    #endregion
}
//
