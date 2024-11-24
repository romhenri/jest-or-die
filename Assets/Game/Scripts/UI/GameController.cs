using System;
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

    public bool debugMode = false;
    public bool sounds = true;

    private void Awake()
    {
        string path = Application.persistentDataPath + "/Save.jest";
        if (File.Exists(path))
        {
            try
            {
                using (FileStream file = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                }
            }
            catch (IOException e)
            {
                Debug.LogWarning("Failed to close an existing open file: " + e.Message);
            }
        }

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
        //Load();
    }

    public void ToggleSounds()
    {
        sounds = !sounds;
        ApplySoundSettings();
    }

    public bool GetSoundsEnabled()
    {
        return sounds;
    }

    public void ApplySoundSettings()
    {
        AudioListener.volume = sounds ? 1f : 0f;
    }

    [Serializable]
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
        string path = Application.persistentDataPath + "/Save.jest";
        try
        {
            // Usa um bloco `using` para garantir que o arquivo seja fechado automaticamente
            using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                SavedData _data = new SavedData
                {
                    savedName = name,
                    savedLevel = level,
                    savedCoins = coins
                };

                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, _data);
                Debug.Log("Data saved successfully.");
            }
        }
        catch (IOException ioEx)
        {
            Debug.LogWarning("Failed to save data due to I/O issue: " + ioEx.Message);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed to save data: " + e.Message);
        }
    }


    public void SaveNow()
    {
        Save(name, level, coins);
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

    public void IncreaseCoins(int newCoins)
    {
        coins += newCoins;
    }

    private void OnGUI()
    {
        if (debugMode)
        {
            GUI.Label(new Rect(10, 10, 200, 20), $"Level: {level}");
            GUI.Label(new Rect(10, 30, 200, 20), $"Coins: {coins}");
            GUI.Label(new Rect(10, 50, 200, 20), $"Sounds: {(sounds ? "true" : "false")}");
        }
    }
}
