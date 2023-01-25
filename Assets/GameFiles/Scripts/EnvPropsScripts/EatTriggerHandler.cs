using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatTriggerHandler : MonoBehaviour
{
    #region Properties
    [Header("Components Reference")]
    [SerializeField] private ParticleSystem plusPS = null;
    [SerializeField] private GameObject plusObj = null;
    [SerializeField] private Animator charAnimator = null;

    #endregion

    #region MonoBehaviour Functions
    private void OnTriggerEnter(Collider other)
    {
        //plusPS.Play();
        if (other.gameObject.tag == "Bead")
        {
            other.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            PlayerSingleton.Instance.GetPlayerBeadsManager.ReduceBeadsCount();
            //plusPS.Play();
            Instantiate(plusObj, transform.position, Quaternion.identity);
            //Destroy(other.gameObject);
        }
    }
    #endregion
}
