using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementHandler : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float moveLength = 0f;

    private bool move = false;
    #endregion

    #region MonoBehaviour Functions
    private void Update()
    {
        if (move)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);

            if (moveLength <= 0f)
            {
                move = false;
                this.enabled = false;
            }
            else
            {
                moveLength -= Time.deltaTime * moveSpeed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            move = true;
        }
    }
    #endregion
}
