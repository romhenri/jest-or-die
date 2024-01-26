using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayer : MonoBehaviour
{
    public GameObject player;
    private float time;
    public float AutoSaveSeconds;
    public void Save()
    {
        SaveSystem.SavePlayer(player);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];

        player.transform.position = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= AutoSaveSeconds)
        {
            Save();
            time = 0;
        }
    }
}
