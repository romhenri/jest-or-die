using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float speed = -2f;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 newPosition = new Vector3 (0f, speed, 0f);
        transform.position += newPosition * Time.deltaTime;
    }
}
