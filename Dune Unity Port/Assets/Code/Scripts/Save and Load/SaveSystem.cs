using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static string playerPath = Application.persistentDataPath + "/Player.data";

    public static void SavePlayer(PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = playerPath;


        PlayerData data = new PlayerData(player);
        FileStream fs = new FileStream(path, FileMode.Create);
        
        formatter.Serialize(fs, data);
        fs.Close();

    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(playerPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(playerPath, FileMode.Open);

            PlayerData data = formatter.Deserialize(fs) as PlayerData;
            fs.Close();
            return data;
        }
        else
        {
            Debug.Log("Player file not found at" + playerPath);
            return null;
        }
    }


}
