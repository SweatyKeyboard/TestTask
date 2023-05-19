using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class SaveData
{
    public float PlayerPositionX;
    public float PlayerPositionY;
    public float PlayerHealth;
    public InventorySlotSaveData[] Inventory = new InventorySlotSaveData[3];
}

[Serializable]
public class InventorySlotSaveData
{
    public int ItemId;
    public int Count;
}


public class SaveLoad : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;

    private string _path;

    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
        {
            Debug.Log("Pause");
            Save();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            Debug.Log("Focus");
            Save();
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Quit");
        Save();
    }

    private void Start()
    {
         _path = Application.persistentDataPath + "/save.dat";
        Load();
    }


    public void Save()
    {
        PlayerToInventory playerToInventory = FindObjectOfType<PlayerToInventory>();
        SaveData saveData = new();
        saveData.PlayerPositionX = playerToInventory.transform.position.x;
        saveData.PlayerPositionY = playerToInventory.transform.position.y;
        saveData.PlayerHealth = FindAnyObjectByType<PlayerHealth>().Health;

        for (int i = 0; i < 3; i++)
        {
            saveData.Inventory[i] = new();
            saveData.Inventory[i].ItemId = playerToInventory.Inventory[i].Item?.Id ?? -1;
            saveData.Inventory[i].Count = playerToInventory.Inventory[i].ItemCount;
        }

        BinaryFormatter binaryFormatter = new();
        FileStream file = File.Create(_path);
        binaryFormatter.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {

        if (!File.Exists(_path))
            return;

        BinaryFormatter binaryFormatter = new();
        FileStream file = File.Open(_path, FileMode.Open);
        SaveData saveData = (SaveData)binaryFormatter.Deserialize(file);
        file.Close();

        PlayerToInventory playerToInventory = FindObjectOfType<PlayerToInventory>();

        playerToInventory.transform.position = new Vector2(
            saveData.PlayerPositionX,
            saveData.PlayerPositionY);
        FindAnyObjectByType<PlayerHealth>().Health = saveData.PlayerHealth;
        for (int i = 0; i < 3; i++)
        {
            playerToInventory.Inventory[i].PutItemById(saveData.Inventory[i].ItemId, saveData.Inventory[i].Count);
        }

        foreach (a_Enemy enemy in FindObjectsOfType<a_Enemy>())
        {
            Destroy(enemy.gameObject);
        }

        _spawner.Spawn(3);
    }
}
