using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMazeForImage : MonoBehaviour
{
    public GameObject wall, water, tree;
    Color[,] colorPixel;
    public Texture2D outlineImage;

    void GenerateFromImage()
    {
        colorPixel = new Color[outlineImage.width, outlineImage.height];

        for (int x = 0; x < outlineImage.width; x++)
        {
            for (int y = 0; y < outlineImage.height; y++)
            {
                colorPixel[x, y] = outlineImage.GetPixel(x, y);

                if (colorPixel[x, y] == Color.black)
                {
                    GameObject go = Instantiate(wall, new Vector3(outlineImage.width / 2 - x, 1.5f, outlineImage.height / 2 - y), Quaternion.identity);
                }
                if (colorPixel[x, y] == Color.blue)
                {
                    Debug.Log("Blue");
                    GameObject go = Instantiate(water, new Vector3(outlineImage.width / 2 - x, -1.9f, outlineImage.height / 2 - y), Quaternion.identity);
                }
                if (colorPixel[x, y] == Color.red)
                {
                    Debug.Log("red");
                    GameObject go = Instantiate(tree, new Vector3(outlineImage.width / 2 - x, 2.0f, outlineImage.height / 2 - y), Quaternion.identity);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateFromImage();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
