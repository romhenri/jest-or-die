using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin, Random }
    enum FlowType { Const, Fluct, Burst }

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private FlowType flowType;
    [SerializeField] private float firingRate = 0.5f;
    [SerializeField] private float burstInterval = 1f;
    [SerializeField] private int bulletsPerBurst = 3;
    [SerializeField] private Vector2 firingAngleRange = new Vector2(0, 360);
    [Space]

    public bool moveHorizontal;
    public bool moveVertical = true;
    [Space]

    public float limitTop = 10f;
    public float limitBottom = -10f;
    bool isToTop = true;

    [Space]
    public float spawnerLife = 100f;
    [Range(1f, 10f)]
    public float spawnerSpeed = 3f;

    [Header("Bullet Attributes")]
    public GameObject Knife;
    public float bulletLife = 5f;
    public float speed = 9f;
    public Vector3 bulletScale = Vector3.one;
    public bool inheritRotation = false;
    public Color bulletColor = Color.white;

    private GameObject spawnedBullet;
    private float timer = 0f;
    private float lifeTimer = 0f;
    private float burstTimer = 0f;

    void Start()
    {
        spawnerSpeed = spawnerSpeed / 1000f;
        spawnerSpeed = spawnerSpeed * 105;
    }

    void Update()
    {
        timer += Time.deltaTime;
        lifeTimer += Time.deltaTime;
        burstTimer += Time.deltaTime;

        // Spawner rotation
        if (spawnerType == SpawnerType.Spin)
        {
            transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        }
        else if (spawnerType == SpawnerType.Random && timer >= firingRate)
        {
            transform.eulerAngles = new Vector3(0f, 0f, Random.Range(firingAngleRange.x, firingAngleRange.y));
        }

        // Firing system
        if (flowType == FlowType.Const && timer >= firingRate)
        {
            Fire();
            timer = 0;
        }
        else if (flowType == FlowType.Fluct && timer >= firingRate * 2)
        {
            Fire();
            timer = Random.Range(0f, firingRate);
        }
        else if (flowType == FlowType.Burst && burstTimer >= burstInterval)
        {
            StartCoroutine(BurstFire());
            burstTimer = 0;
        }

        // Movement system
        if (moveHorizontal || moveVertical)
        {
            if (lifeTimer > spawnerLife) Destroy(this.gameObject);
        }

        if (moveHorizontal)
        {
            transform.position += new Vector3(spawnerSpeed * Time.deltaTime, 0f, 0f);
        }

        if (moveVertical)
        {
            if (transform.position.y > limitTop)
            {
                isToTop = false;
            }
            else if (transform.position.y < limitBottom)
            {
                isToTop = true;
            }

            if (isToTop)
            {
                transform.position += new Vector3(0f, spawnerSpeed * Time.deltaTime, 0f);
            }
            else
            {
                transform.position -= new Vector3(0f, spawnerSpeed * Time.deltaTime, 0f);
            }
        }
    }

    private IEnumerator BurstFire()
    {
        for (int i = 0; i < bulletsPerBurst; i++)
        {
            Fire();
            yield return new WaitForSeconds(firingRate / bulletsPerBurst);
        }
    }

    private void Fire()
    {
        if (Knife)
        {
            spawnedBullet = Instantiate(Knife, transform.position, inheritRotation ? transform.rotation : Quaternion.identity);
            spawnedBullet.GetComponent<Knife>().speed = speed;
            spawnedBullet.GetComponent<Knife>().bulletLife = bulletLife;
            spawnedBullet.transform.localScale = bulletScale;
            spawnedBullet.GetComponent<SpriteRenderer>().color = bulletColor;

            if (spawnerType == SpawnerType.Spin || spawnerType == SpawnerType.Random)
            {
                spawnedBullet.transform.rotation = transform.rotation;
            }
        }
    }
}
