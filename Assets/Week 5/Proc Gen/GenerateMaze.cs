using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMaze : MonoBehaviour
{
    public GameObject wall, target, NPC, t;

    private int[,] worldMap = new int[,]
    {
        {1,1,1,1,1,1,1,1,1,1},
        {1,0,0,0,0,0,0,1,0,1},
        {1,0,0,2,0,0,0,1,0,1},
        {1,0,1,1,1,1,0,0,0,1},
        {1,0,0,0,0,1,0,0,0,1},
        {1,0,0,0,1,1,0,0,0,1},
        {1,0,0,0,1,0,0,0,0,1},
        {1,0,0,0,0,3,0,0,0,1},
        {1,0,0,0,0,0,0,0,0,1},
        {1,1,1,1,1,1,1,1,1,1}
    };

    // Start is called before the first frame update
    void Start()
    {
        // GenerateFromArray();
        GenerateFromFile();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateFromFile()
    {
        TextAsset t1 = (TextAsset)Resources.Load("maze", typeof(TextAsset));
        string s = t1.text;
        s = s.Replace(System.Environment.NewLine, "");

        for (int i = 0; i < s.Length; i++)
        {
            int column, row;
            column = i % 10;
            row = i / 10;

            if (s[i] == '1') t = Instantiate(wall, new Vector3(50 - column * 10, 1.5f, 50 - row * 10), Quaternion.identity);
            else if (s[i] == '2') t = Instantiate(NPC, new Vector3(50 - column * 10, 1.5f, 50 - row * 10), Quaternion.identity);
            else if (s[i] == '3')
            {
                t = Instantiate(target, new Vector3(50 - column * 10, 1.5f, 50 - row * 10), Quaternion.identity);
                t.name = "target";
            }
        }
    }

    void GenerateFromArray()
    {

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (worldMap[i, j] == 1) t = Instantiate(wall, new Vector3(50 - i * 10, 1.5f, 50 - j * 10), Quaternion.identity);
                if (worldMap[i, j] == 2) t = Instantiate(NPC, new Vector3(50 - i * 10, 1.5f, 50 - j * 10), Quaternion.identity);
                else if (worldMap[i, j] == 3)
                {
                    t = Instantiate(target, new Vector3(50 - i * 10, 1.5f, 50 - j * 10), Quaternion.identity);
                    t.name = "target";
                }
            }
        }
    }
}
