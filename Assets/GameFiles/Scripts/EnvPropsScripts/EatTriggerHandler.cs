using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatTriggerHandler : MonoBehaviour
{
    #region Properties
    [Header("Components Reference")]
    [SerializeField] private ParticleSystem plusPS = null;
    #endregion

    #region MonoBehaviour Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bead")
        {
            other.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            plusPS.Play();
            //Destroy(other.gameObject);
        }
    }
    #endregion
}
