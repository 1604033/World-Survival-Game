using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

[CreateAssetMenu(menuName = "Data/Scene Obstacles")]
public class SceneObstacles : ScriptableObject
{
    public Dictionary<Vector3Int, bool> SceneObstacleData = new();
    [SerializeField]
    private string savePath = "SceneObstaclesSave.json";

    // Surrogate class for serialization
    [System.Serializable]
    public class ObstacleData
    {
        public int x, y, z;
        public bool isObstacle;

        public ObstacleData(int x, int y, int z, bool isObstacle)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.isObstacle = isObstacle;
        }
    }

    public void SaveData()
    {
        // Convert the dictionary to a list of ObstacleData for serialization
        List<ObstacleData> dataList = new List<ObstacleData>();
        foreach (var item in SceneObstacleData)
        {
            dataList.Add(new ObstacleData(item.Key.x, item.Key.y, item.Key.z, item.Value));
        }
    
        // Serialize the list to JSON using Newtonsoft.Json
        string data = JsonConvert.SerializeObject(dataList, Formatting.Indented);
        Debug.Log($"DataList Count: {data.Length}");
        // Save the JSON string to a file
        File.WriteAllText(Application.persistentDataPath + "/" + savePath, data);
    }
    
    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/" + savePath))
        {
            // Load the JSON string from the file
            string data = File.ReadAllText(Application.persistentDataPath + "/" + savePath);
    
            // Deserialize the JSON string to a list of ObstacleData
            List<ObstacleData> dataList = JsonConvert.DeserializeObject<List<ObstacleData>>(data);
    
            // Convert the list back to a dictionary
            SceneObstacleData.Clear(); // Clear the dictionary before adding items to it
            foreach (var item in dataList)
            {
                SceneObstacleData.Add(new Vector3Int(item.x, item.y, item.z), item.isObstacle);
            }
            
        }
    }
}
