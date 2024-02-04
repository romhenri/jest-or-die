using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }
    enum FlowType { Const, Fluct }

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private FlowType flowType;
    [SerializeField] private float firingRate = 0.5f;
    [Space]

    public bool moveHorizontal;
    public bool moveVertical = true;
    [Space]

    public float limitTop = 10f;
    bool isToTop = true;

    [Space]
    public float spawnerLife = 100;
    [Range(1f, 10f)]
    public float spawnerSpeed = 3f;

    [Header("Bullet Attributes")]
    public GameObject Knife;
    public float bulletLife = 5f;
    public float speed = 9f;

    private GameObject spawnedBullet;
    private float timer = 0f;
    private float lifeTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnerSpeed = spawnerSpeed / 1000f;
        spawnerSpeed = spawnerSpeed * 105;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        lifeTimer += Time.deltaTime;

        // Spawner rotation
        if (spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);

        // Firing system
        if (flowType == FlowType.Const)
        {
            if (timer >= firingRate)
            {
                Fire();
                timer = 0;
            }
        }
        else
        {
            if (timer >= firingRate * 2)
            {
                Fire();
                timer = Random.Range(0f, firingRate);
            }
        }

        // Movem system
        if (moveHorizontal)
        {
            if (lifeTimer > spawnerLife) Destroy(this.gameObject);
            transform.position += new Vector3(spawnerSpeed * Time.deltaTime, 0f, 0f);
        }
        if (moveVertical)
        {
            if (lifeTimer > spawnerLife) Destroy(this.gameObject);

            if (transform.position.y > limitTop)
            {
                isToTop = false;
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


    private void Fire()
    {
        if (Knife)
        {
            spawnedBullet = Instantiate(Knife, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Knife>().speed = speed;
            spawnedBullet.GetComponent<Knife>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }
}

