using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlinkyTrail : MonoBehaviour
{
    public GameObject spherePrefab;
    public float radius = 1.0f;
    public float frequency = 1.0f;
    public int numSpheres = 10;

    private List<GameObject> spheres = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < numSpheres; i++)
        {
            GameObject sphere = Instantiate(spherePrefab);
            sphere.transform.parent = transform;
            spheres.Add(sphere);
        }
    }

    void Update()
    {
        for (int i = 0; i < spheres.Count; i++)
        {
            float x = radius * Mathf.Sin(frequency * Time.time + i);
            float y = i * 0.1f;
            float z = i * 0.1f;
            spheres[i].transform.localPosition = new Vector3(x, y, z);
        }
    }
}

