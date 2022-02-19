using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataController;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    JSONController json = new JSONController();
    private string DataPath;
    public void Start()
    {
        DataPath = Application.persistentDataPath + "/Data.json";
    }

    public void SoloGameButtonPressed() 
    {
        json.LoadFromJson(DataPath);
        SceneManager.LoadScene(1);
    }
}
