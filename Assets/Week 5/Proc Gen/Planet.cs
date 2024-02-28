using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Planet : MonoBehaviour
{
    GameObject sun;
    float distance = 150;

    float rotatinalSpeed = 10;
    float orbitalSpeed = 0.2f;
    float orbitalAngle = 0;
    float angle = 0;
    float orbitalRotationalSpeed = 20;
    Color c1 = Color.blue;
    int lengthOfLine = 100;

    void DrawOrbit()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));

        lineRenderer.SetColors(c1, c1);
        lineRenderer.SetWidth(1.0f, 1f);

        lineRenderer.SetVertexCount(lengthOfLine + 1);

        int i = 0;
        while (i <= lengthOfLine)
        {
            float unitAngle = (2 * 3.14f) / lengthOfLine;
            float currentAngle = unitAngle * i;
            Vector3 pos = new Vector3(distance * MathF.Cos(currentAngle), 0, distance * MathF.Sin(currentAngle));
            lineRenderer.SetPosition(i, pos);
            i++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sun = GameObject.Find("Sun");
        transform.position = new Vector3(distance, 0, distance);
        DrawOrbit();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotatinalSpeed * Time.deltaTime, Space.World);

        float tempX, tempY, tempZ;
        orbitalAngle += Time.deltaTime * orbitalSpeed;

        tempX = sun.transform.position.x + distance * Mathf.Cos(orbitalAngle);
        tempZ = sun.transform.position.z + distance * Mathf.Sin(orbitalAngle);
        tempY = sun.transform.position.y;

        transform.position = new Vector3(tempX, tempY, tempZ);

    }

    public void SetRotationalSpeed(float s)
    {
        rotatinalSpeed *= s;
    }

    public void SetOrbitSpeed(float os)
    {
        orbitalSpeed *= os;
    }

    public void SetDistanceToSun(float d)
    {
        distance *= d;
    }

    public void SetName(string n)
    {
        name = n;
        transform.Find("label").GetComponent<TextMeshPro>().text = name;
    }

    public void SetRadius(float r)
    {
        transform.localScale = new Vector3(r, r, r);
    }
}
