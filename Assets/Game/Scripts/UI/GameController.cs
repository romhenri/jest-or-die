using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private string playerName;
    private int coins;
    private int level;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Load();
    }

    private class SavedData
    {
        public string savedName;
        public int savedLevel;
        public int savedCoins;
    }

    FileStream file = null;
    BinaryFormatter bf = new BinaryFormatter();

    private void Save(string name, int level, int coins)
    {
        try
        {
            file = File.Create(Application.persistentDataPath + "/Save.jest");
            SavedData _data = new SavedData
            {
                savedName = name,
                savedLevel = level,
                savedCoins = coins
            };

            bf.Serialize(file, _data);
            file.Close();
        }
        catch
        {
            Debug.LogWarning("Failed to save data.");
        }
    }

    private void Load()
    {
        try
        {
            file = File.Open(Application.persistentDataPath + "/Save.jest", FileMode.Open);
            SavedData _data = (SavedData)bf.Deserialize(file);
            playerName = _data.savedName;
            level = _data.savedLevel;
            coins = _data.savedCoins;
            file.Close();
        }
        catch
        {
            Debug.LogWarning("Failed to load data.");
        }
    }

    public string GetPlayerName() => playerName;
    public int GetCoins() => coins;
    public int GetLevel() => level;

    public void SetLevel(int receivedLevel)
    {
        level = receivedLevel;
    }
}
