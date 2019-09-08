using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using UnityEngine.JSON
[Serializable]
public class PlayerInfo
{
    public string water;
    public int energy;
    public float health;
 
    public static PlayerInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayerInfo>(jsonString);
    }
}
 
public class Waves : MonoBehaviour
{
    //UIELEMNTS
    public GameObject Confiramtion;
    public string returned;
    public Canvas comment;
    public Button send;
    public Button agreement;
    public GameObject parent;
    public InputField input1;
    public InputField input2;
    // public string SaveToString()

    //   return JsonUtility.ToJson(this);
    string sender;
    public void InstButton() {
        Instantiate(Confiramtion, parent.transform);
    }

     public void SendStuff()
     {
        Whattosend("auditor");
        Whattosend("Труба 377х9 с бетонным покрытием в оцинкованной оболочке " + input1.text);
        
     }



    public void SendFactory() {
        Whattosend("zavod");
        Whattosend("Truba trexsloenaya s politileonom");
    } 

     //
    public string Whattosend( string input) {
        sender = input;
       // Debug.Log(sender+"whatotsend");
        return sender;
    }
   
 
    void Start()
    {
       // Whattosend("Труба 377х9 с бетонным покрытием в оцинкованной оболочке"+input1.text);
        StartCoroutine(GetRequest("http://37.204.92.227:8087/" + sender ));
     
    }
    IEnumerator Upload()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("fa"));
        //formData.Add(new MultipartFormFileSection("volni_sosut", "myfile.txt"));
 
        UnityWebRequest www = UnityWebRequest.Post("http://37.204.92.227:8087/", formData);
        yield return www.SendWebRequest();
 
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
 
        }
    }
 
    IEnumerator GetRequest(string uri)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();
 
        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            Console.Write("Error While Sending: " + uwr.error);
        }
        else
        {
            returned = uwr.downloadHandler.text;
 
            //uwr.downloadHandler.text
            Debug.Log("Received: " + uwr.downloadHandler.text);
            Console.Write(uwr.downloadHandler.text);
        }
    }
}