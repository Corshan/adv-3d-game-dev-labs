using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveAndLoadData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) SaveData3();
        if (Input.GetKeyDown(KeyCode.L)) LoadData3();
    }

    void SaveData4()
    {
        string mutipleData = "Corey|10,10,2|100*Mary|5,3,4|50";

        File.WriteAllText(Application.dataPath + "/gameData.txt", mutipleData);
        print("Saving Complete");
    }

    void LoadData4()
    {
        
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
}
