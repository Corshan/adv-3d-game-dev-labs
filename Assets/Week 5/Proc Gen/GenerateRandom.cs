using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandom : MonoBehaviour
{
    const int N = 1, S = 2, E = 3, W = 4;
    int[,] grid;
    [SerializeField]
    [Range(5, 100)]
    int width = 5, height = 5, wallSize = 5;
    public GameObject verticalWall, horizontalWall;
    GameObject[,] gridObjectsH, gridObjectsV;
    GameObject[] allObjectsInScene;
    public GameObject target, npc;
    float wallHeight;

    void Innit()
    {
        // height = width;
        wallHeight = 4;

        verticalWall.transform.localScale = new Vector3(.1f, wallHeight, wallSize);
        horizontalWall.transform.localScale = new Vector3(wallSize, wallHeight, .1f);

        grid = new int[width, height];
        gridObjectsV = new GameObject[width + 1, height + 1];
        gridObjectsH = new GameObject[width + 1, height + 1];

        DrawGrid();

        GameObject.Find("Ground").transform.localScale = new Vector3((width + 1) * wallSize, 0.5f, (height + 1) * wallSize);
    }

    void DrawGrid()
    {
        float xOffset, zOffset;

        for (int i = 0; i <= height; i++)
        {
            for (int j = 0; j <= width; j++)
            {

                if (i < height)
                {
                    float vWallSize = verticalWall.transform.localScale.z;

                    xOffset = -(width * vWallSize) / 2;
                    zOffset = -(height * vWallSize) / 2;

                    gridObjectsV[j, i] = Instantiate(verticalWall, new Vector3(-vWallSize / 2 + j * wallSize + xOffset, wallSize / 2, i * vWallSize + zOffset), Quaternion.identity);
                    gridObjectsV[j, i].active = true;
                    gridObjectsV[j, i].name = "v" + i + j;
                    gridObjectsV[j, i].tag = "wall";
                }

                if (j < width)
                {
                    float hWallSize = horizontalWall.transform.localScale.x;

                    xOffset = -(width * hWallSize) / 2;
                    zOffset = -(height * hWallSize) / 2;

                    gridObjectsH[j, i] = Instantiate(horizontalWall, new Vector3(j * hWallSize + xOffset, wallSize / 2, -(hWallSize / 2) + i * hWallSize + zOffset), Quaternion.identity);
                    gridObjectsH[j, i].active = true;
                    gridObjectsH[j, i].name = "h" + i + j;
                    gridObjectsH[j, i].tag = "wall";
                }
            }
        }
    }

    void GenerateMazeBinary()
    {
        float randomNumber;
        int carvingDirection;

        for (int row = 0; row < height; row++)
        {
            for (int cell = 0; cell < width; cell++)
            {
                randomNumber = Random.Range(0, 100);

                if (randomNumber > 50) carvingDirection = N;
                else carvingDirection = E;
                if (cell == width - 1)
                {
                    if (row < height - 1) carvingDirection = N;
                    else carvingDirection = W;
                }
                else if (row == height - 1)
                {
                    if (cell < width - 1) carvingDirection = E;
                    else carvingDirection = -1;
                }
                grid[cell, row] = carvingDirection;
            }
        }
    }

    void DisplayGrid()
    {
        for (int row = 0; row < height; row++)
        {
            for (int cell = 0; cell < width; cell++)
            {
                if (grid[cell, row] == N) gridObjectsH[cell, row + 1].active = false;
                if (grid[cell, row] == E) gridObjectsV[cell + 1, row].active = false;
            }
        }
    }

    void AddTarget()
    {
        float zOffset, xOffset;
        xOffset = -(width * wallSize) / 2;
        zOffset = -(height * wallSize) / 2;

        GameObject p = Instantiate(target, new Vector3(xOffset, 1.0f, zOffset), Quaternion.identity);
        p.name = "target";
    }

    void AddNPC()
    {
        float zOffset, xOffset;
        xOffset = -(width * wallSize) / 2;
        zOffset = -(height * wallSize) / 2;

        GameObject p = Instantiate(npc, new Vector3(xOffset, 1.0f, zOffset), Quaternion.identity);
        p.name = "NPC";
    }

    // Start is called before the first frame update
    void Start()
    {
        Innit();
        GenerateMazeBinary();
        DisplayGrid();
        AddTarget();
        AddNPC();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Innit();
            GenerateMazeBinary();
            DisplayGrid();
        }
    }
}
