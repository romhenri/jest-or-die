using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    // coloque aqui os dados do personagem 
    public float[] position;

    public PlayerData(GameObject player) {

        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
    }

}
