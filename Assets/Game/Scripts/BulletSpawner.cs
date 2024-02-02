using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;
    [Space]

    public bool moveHorizontal;
    public bool moveVertical;
    [Space]

    public float limitTop = 5f;
    bool isToTop = true;

    [Space]
    public float spawnerLife;
    [Range(1f, 10f)]
    public float spawnerSpeed = 2f;

    [Header("Bullet Attributes")]
    public GameObject Knife;
    public float bulletLife = 1f;
    public float speed = 1f;

    private GameObject spawnedBullet;
    private float timer = 0f;
    private float lifeTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnerSpeed = spawnerSpeed / 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        lifeTimer += Time.deltaTime;
        if (spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        if (timer >= firingRate)
        {
            Fire();
            timer = 0;
        }

        // Movem system
        if (moveHorizontal)
        {
            if (lifeTimer > spawnerLife) Destroy(this.gameObject);
            transform.position += new Vector3(spawnerSpeed, 0f, 0f);
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
                transform.position += new Vector3(0f, spawnerSpeed, 0f);
            }
            else
            {
                transform.position -= new Vector3(0f, spawnerSpeed, 0f);
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

