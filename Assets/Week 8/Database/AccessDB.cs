using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AccessDB : MonoBehaviour
{
    private string _url = "https://mosesadv3dgamedev.000webhostapp.com/Message.php?name=Moses&score=10";
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(_url);
        yield return www.SendWebRequest();
        string result = www.downloadHandler.text;
        print(result);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
