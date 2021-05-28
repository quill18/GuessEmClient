using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class ServerTalker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        // Make a web request to get info from the server
        // this will be a text response.
        // This will return/continue IMMEDIATELY, but the coroutine
        // will take several MS to actually get a response from the server.
        StartCoroutine( GetWebData("http://localhost:8000/user/", "myAwesomeID" ) );

    }

    IEnumerator GetWebData( string address, string myID )
    {
        UnityWebRequest www = UnityWebRequest.Get(address + myID);
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Something went wrong: " + www.error);
        }
        else
        {
            Debug.Log( www.downloadHandler.text );

            ProcessServerResponse(www.downloadHandler.text);

        }
    }

    void ProcessServerResponse( string rawResponse )
    {
        // That text, is actually JSON info, so we need to 
        // parse that into something we can navigate.

        JSONNode node = JSON.Parse( rawResponse );

        // Output some stuff to the console so that we know
        // that it worked.

        Debug.Log("Username: " + node["username"]);
        Debug.Log("Misc Data: " + node["someArray"][1]["name"] + " = " + node["someArray"][1]["value"]);
    }

    
}
