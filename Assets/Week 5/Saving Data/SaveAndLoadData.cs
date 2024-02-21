using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveAndLoadData : MonoBehaviour
{
    const int PLAYER_NAME = 0;
    const int PLAYER_POS = 1;
    const int PLAYER_SCORE = 2;
    const int PLAYER_LAST_LEVEL = 3;
    const int PLAYER_DIFFICULTY_LEVEL = 4;

    Vector3 playersPos;
    string playersName;
    int score;
    public GameObject playerCharacter;
    DataToSave myData;
    string jsonText;

    void Awake()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("saveData");
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (gameObjects.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);

        if (scene.name != "SaveData")
        {
            GameObject t = Instantiate(playerCharacter, playersPos, Quaternion.identity);
            t.name = playersName;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) SaveDataJson();
        if (Input.GetKeyDown(KeyCode.L)) LoadDataJson();
    }

    void SaveData4()
    {
        string mutipleData = "Corey|10,10,2|100*Mary|5,3,4|50";

        File.WriteAllText(Application.dataPath + "/gameData.txt", mutipleData);
        print("Saving Complete");
    }

    void LoadData4()
    {
        string dataToRead = File.ReadAllText(Application.dataPath + "/gameData.txt");
        string newRecord;
        float x, y, z;

        for (int i = 0; i < 2; i++)
        {
            newRecord = dataToRead.Split("*")[i];
            playersName = newRecord.Split("|")[PLAYER_NAME];
            string corrds = newRecord.Split("|")[PLAYER_POS];
            score = int.Parse(newRecord.Split("|")[PLAYER_SCORE]);

            x = float.Parse(corrds.Split(",")[0]);
            y = float.Parse(corrds.Split(",")[1]);
            z = float.Parse(corrds.Split(",")[2]);

            Vector3 newPosition = new(x, y, z);

            print("Players name: " + playersName);
            print("Player pos: " + newPosition);

            GameObject t = Instantiate(playerCharacter, newPosition, Quaternion.identity);
            t.name = playersName;
            print("Current Score =" + score);
        }
    }

    void SaveData3()
    {
        string mutipleData = "Corey|2*Mary|3";

        File.WriteAllText(Application.dataPath + "/gameData.txt", mutipleData);
        print("Saving Complete");
    }

    void LoadData3()
    {
        string dataToRead = File.ReadAllText(Application.dataPath + "/gameData.txt");
        string newData;
        for (int i = 0; i < 2; i++)
        {
            newData = dataToRead.Split("*")[i];
            print("Data " + i + " name=" + newData.Split("|")[0] + ", score=" + newData.Split("|")[1]);
        }
    }

    void SaveData2()
    {
        int newScore = 30;
        string playerName = "Corey";
        string[] mutipleData = new string[] { "" + newScore, playerName };
        string dataToSave = string.Join("|", mutipleData);

        File.WriteAllText(Application.dataPath + "/gameData.txt", dataToSave);
        print("Saving Complete");
    }

    void LoadData2()
    {
        print("Reading Data");
        string savedData = File.ReadAllText(Application.dataPath + "/gameData.txt");
        string[] mutipleDataLoaded = savedData.Split("|");
        int newScore = int.Parse(mutipleDataLoaded[0]);
        string newName = mutipleDataLoaded[1];
        print("Data Loaded: score=" + newScore + ", name=" + newName);
    }

    void SaveData1()
    {
        print("Saving Data");
        int nbLives = 2;
        File.WriteAllText(Application.dataPath + "/gameData.txt", "" + nbLives);
        print("Saving is complete");
    }

    void LoadData1()
    {
        print("Reading Data");
        string savedData = File.ReadAllText(Application.dataPath + "/gameData.txt");
        print("Loaded Data: " + savedData);
    }

    void SaveDataJson()
    {
        myData = new DataToSave { name = "Corey", score = 20, pos = new Vector3(10, 0, 10), lastLevel = "week6Level2" };
        jsonText = JsonUtility.ToJson(myData);

        File.WriteAllText(Application.dataPath + "/gameData.json", jsonText);
        Debug.Log(jsonText);

    }

    void LoadDataJson()
    {
        string savedData = File.ReadAllText(Application.dataPath + "/gameData.json");
        DataToSave dataToSave = JsonUtility.FromJson<DataToSave>(savedData);

        playersPos = dataToSave.pos;
        playersName = dataToSave.name;
        
        Debug.Log("Loaded Data: Name =" + dataToSave.name + " score=" + dataToSave.score + " Position=" + dataToSave.pos);

        SceneManager.LoadScene(dataToSave.lastLevel);
    }

    private class DataToSave
    {
        public string name;
        public int score;
        public Vector3 pos;
        public string lastLevel;
    }
}
