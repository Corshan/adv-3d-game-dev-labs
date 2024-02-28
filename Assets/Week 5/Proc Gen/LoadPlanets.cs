using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class LoadPlanets : MonoBehaviour
{
    public GameObject planetTemplate;


    void LoadAllPlanets()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("planets");
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(textAsset.text);

        foreach (XmlNode planet in doc.SelectNodes("planets/planet"))
        {
            string name, diameter, distanceToSun, roatationPeriod, orbitalVelocity;
            name = planet.Attributes.GetNamedItem("name").Value;
            diameter = planet.Attributes.GetNamedItem("diameter").Value;
            distanceToSun = planet.Attributes.GetNamedItem("distancetoSun").Value;
            roatationPeriod = planet.Attributes.GetNamedItem("rotationPeriod").Value;
            orbitalVelocity = planet.Attributes.GetNamedItem("orbitalVelocity").Value;
            Debug.Log("Name of the planet => " + name);


            GameObject g = Instantiate(planetTemplate);
            g.GetComponent<Planet>().SetDistanceToSun(float.Parse(distanceToSun));
            g.GetComponent<Planet>().SetOrbitSpeed(float.Parse(orbitalVelocity));
            g.GetComponent<Planet>().SetRadius(float.Parse(diameter));
            g.GetComponent<Planet>().SetRotationalSpeed(1/float.Parse(roatationPeriod)); 
            g.GetComponent<Planet>().SetName(name); 
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadAllPlanets();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
