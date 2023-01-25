using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusObject : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] float upSpeed;
    [SerializeField] float fadeSpeed;
    [SerializeField] float delayDestroy;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delayDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
        GetComponentInChildren<SpriteRenderer>().color = new Color(GetComponentInChildren<SpriteRenderer>().color.r, GetComponentInChildren<SpriteRenderer>().color.g, GetComponentInChildren<SpriteRenderer>().color.b, GetComponentInChildren<SpriteRenderer>().color.a - fadeSpeed);
    }
}
