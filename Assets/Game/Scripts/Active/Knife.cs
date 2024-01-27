using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [Range(0, 16)]
    public float speed = 2;
    float halfSpeed;
    [Header("Mode")]
    [Range(-2, 2)]
    [SerializeField] int direction = 0;

    Vector3 movement;

    void Start()
    {
        halfSpeed = speed / 2;
        switch (direction)
        {
            case 0:
                transform.Rotate(0, 0, 180f);
                movement = new Vector3(0f, -(speed), 0f);
                break;
            case 1:
                transform.Rotate(0, 0, 135f);
                movement = new Vector3(-halfSpeed, -halfSpeed, 0f);
                break;
            case -1:
                transform.Rotate(0, 0, -135f);
                movement = new Vector3(halfSpeed, -halfSpeed, 0f);
                break;
            case 2:
                transform.Rotate(0, 0, 90f);
                movement = new Vector3(-(halfSpeed), 0f, 0f);
                break;
            case -2:
                transform.Rotate(0, 0, -90f);
                movement = new Vector3(halfSpeed, 0f, 0f);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        transform.position += movement * Time.deltaTime;
    }
}
