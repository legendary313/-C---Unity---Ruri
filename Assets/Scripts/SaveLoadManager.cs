using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public void SaveGame(PlayerData playerData, AchievementData achievement, ItemData itemData)
    {
        PlayerPrefs.SetInt("SavedGame", 1);
        BinaryFormatter formatter = new BinaryFormatter();

        // Save sharedFile Data (playerData & AchievementData)
        string sharedFilePath = Application.persistentDataPath + "/player.data";
        Debug.Log("Save shared file " + sharedFilePath);
        FileStream sharedFileStream = new FileStream(sharedFilePath, FileMode.Create);
        GameData gameData = new GameData(playerData, achievement);
        formatter.Serialize(sharedFileStream, gameData);
        sharedFileStream.Close();

        // Save privateFile Data (ItemData belong to each Map)
        string privateFilePath = Application.persistentDataPath + "/map.data";
        Debug.Log("Save private File " + privateFilePath);
        FileStream privateFileStream = new FileStream(privateFilePath, FileMode.Create);
        formatter.Serialize(privateFileStream, itemData);
        privateFileStream.Close();
    }
    
    public ItemData loadPrivateData()
    {
        string privateFilePath = Application.persistentDataPath + "/map.data";

        if (File.Exists(privateFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream privateFileStream = new FileStream(privateFilePath, FileMode.Open);
            ItemData itemData = formatter.Deserialize(privateFileStream) as ItemData;
            privateFileStream.Close();
            return itemData;
        }
        else
        {
            Debug.Log("Save privateFile not found in " + privateFilePath);
            return null;
        }
    }

    public GameData loadSharedData()
    {
        string sharedFilePath = Application.persistentDataPath + "/player.data";
        if (File.Exists(sharedFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream sharedFileStream = new FileStream(sharedFilePath, FileMode.Open);
            GameData gameData = formatter.Deserialize(sharedFileStream) as GameData;
            sharedFileStream.Close();
            return gameData;
        }
        else
        {
            Debug.Log("Save shareFile not found in " + sharedFilePath);
            return null;
        }
    }

    // public float loadCompleteMapData(int index){
    //     string privateFilePath = Application.persistentDataPath;
    //     if (index == 1){
    //         privateFilePath += "/map1.data";
    //     }else if (index == 2){
    //         privateFilePath += "/map2.data";
    //     }
    //     if (File.Exists(privateFilePath)){
    //         BinaryFormatter formatter = new BinaryFormatter();
    //         FileStream privateFileStream = new FileStream(privateFilePath, FileMode.Open);
    //         ItemData itemData = formatter.Deserialize(privateFileStream) as ItemData;
    //         privateFileStream.Close();
    //         return itemData.progressMap;
    //     }else{
    //         return 0f;
    //     }
    // }

}
