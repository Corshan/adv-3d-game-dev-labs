using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    float[,] map;
    [SerializeField][Range(10, 500)] int mapHeight, mapWidth;
    [SerializeField][Range(0, 500)] float blockSize, blockHeight, frequency, scale;
    public GameObject block;

    void InnitArray()
    {
        map = new float[mapWidth, mapHeight];
        float nx, ny;

        for (int j = 0; j < mapHeight; j++)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                nx = i / mapWidth;
                ny = j / mapHeight;
                map[i, j] = Mathf.PerlinNoise(i * 1.0f / frequency + 0.1f, j * 1.0f / frequency + 0.1f);
            }
        }
    }

    void DisplayArray()
    {
        for (int j = 0; j < mapHeight; j++)
        {
            for (int i = 0; i < mapWidth; i++)
            {
                GameObject go = Instantiate(block, new Vector3(i*blockSize, MathF.Round(map[i,j]*blockHeight*scale), j*blockSize), Quaternion.identity);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        map = new float[mapWidth, mapHeight];
        block.transform.localScale = new Vector3(blockSize, blockSize, blockSize);

        InnitArray();
        DisplayArray();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
