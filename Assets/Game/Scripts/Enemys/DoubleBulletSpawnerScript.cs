using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBulletSpawner : MonoBehaviour
{
    [Header("Spawner Configuration")]
    public GameObject bulletSpawnerPrefab;
    public float distanceBetweenSpawners = 5f;
    public float spawnerSpeed = 3f;
    public float limitTop = 10f;
    public float limitBottom = -10f;

    private GameObject spawnerA;
    private GameObject spawnerB;
    private bool isToTop = true;

    void Start()
    {
        if (bulletSpawnerPrefab == null)
        {
            Debug.LogError("Bullet Spawner Prefab is not assigned in the Inspector.");
            return;
        }

        spawnerA = Instantiate(bulletSpawnerPrefab, transform.position + Vector3.up * (distanceBetweenSpawners / 2), Quaternion.identity, transform);
        spawnerB = Instantiate(bulletSpawnerPrefab, transform.position - Vector3.up * (distanceBetweenSpawners / 2), Quaternion.identity, transform);

        EnableSpawnerScripts(spawnerA);
        EnableSpawnerScripts(spawnerB);
    }

    void EnableSpawnerScripts(GameObject spawner)
    {
        var bulletSpawnerScript = spawner.GetComponent<BulletSpawner>();
        if (bulletSpawnerScript != null)
        {
            bulletSpawnerScript.enabled = true;
        }
        else
        {
            Debug.LogWarning("BulletSpawner script not found on the instantiated prefab.");
        }
    }

    void Update()
    {
        if (spawnerA == null || spawnerB == null)
        {
            Debug.LogWarning("One of the spawners is null. Please check initialization.");
            return;
        }

        float moveStep = spawnerSpeed * Time.deltaTime * (isToTop ? 1 : -1);

        spawnerA.transform.position += new Vector3(0f, moveStep, 0f);
        spawnerB.transform.position += new Vector3(0f, moveStep, 0f);

        if (spawnerA.transform.position.y >= limitTop || spawnerB.transform.position.y >= limitTop)
        {
            isToTop = false;
        }
        else if (spawnerA.transform.position.y <= limitBottom || spawnerB.transform.position.y <= limitBottom)
        {
            isToTop = true;
        }
    }

}
