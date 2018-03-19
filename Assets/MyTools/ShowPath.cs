using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShowPath : MonoBehaviour {

    private void OnGUI()
    {
        string text = "persistentData  " + Application.persistentDataPath 
            + "\nstreamingAssets  " + Application.streamingAssetsPath
            + "\ntemporaryCache  " + Application.temporaryCachePath
            + "\ndata  " + Application.dataPath;
        GUI.Label(new Rect(0, 0, 600, 400), text);
    }

    private void Start()
    {
        print(Application.dataPath + "/abc");
        DirectoryInfo a = Directory.CreateDirectory(Application.dataPath + "/abc");
        print(a == null);
    }

}
