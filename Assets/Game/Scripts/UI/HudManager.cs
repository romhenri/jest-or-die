using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class HudManager : MonoBehaviour
{
    [SerializeField]
    public GameObject Life;

    void Start()
    {
    }

    void Update()
    {
    }

    public void DecreaseLives()
    {
        if (Life.transform.childCount > 0)
        {
            Destroy(Life.transform.GetChild(0).gameObject);
        }
    }
}
