using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Straight, Spin }


    [Header("Bullet Attributes")]
    public GameObject Knife;
    public float bulletLife = 1f;
    public float speed = 1f;

    public bool moveHorizontal;  //a partir daqui eu to fazendo merda
    public bool moveVertical;

    public float spawnerLife;
    public float spawnerSpeed; //aqui ja n sou mais eu


    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;


    private GameObject spawnedBullet;
    private float timer = 0f;
    private float lifeTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
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
        // a partir daqui eu to inventando moda

       
        if (moveHorizontal)
        {
            if (lifeTimer > spawnerLife) Destroy(this.gameObject);
            transform.position += new Vector3(spawnerSpeed, 0f, 0f);
        }
        if (moveVertical)
        {
            if (lifeTimer > spawnerLife) Destroy(this.gameObject);
            transform.position += new Vector3(0f, spawnerSpeed, 0f);
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

