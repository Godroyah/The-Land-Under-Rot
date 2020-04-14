using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame()
    {
        //string path = Application.persistentDataPath + "/gamedata.lur";
        string path = Path.Combine(Application.persistentDataPath, "gamedata.lur");
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            GameData data = new GameData(GameController.Instance);

            formatter.Serialize(stream, data);
        }
            
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/gamedata.lur";

        if (File.Exists(path))
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                GameData data = formatter.Deserialize(stream) as GameData;

                return data;
            }      
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    #region Brackeys
    /*
    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gamedata.lur";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData(GameController.Instance);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/gamedata.lur";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    */
    #endregion
}
