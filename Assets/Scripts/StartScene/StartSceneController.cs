using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DataController;
using System.IO;

public class StartSceneController : MonoBehaviour
{
    public Text RightAnswerQuantity;
    public Text HintQuantity;
    public Text UnrightAnswerQuantity;
    JSONController json = new JSONController();

    public void ButtonSoloGamePressed() {SceneManager.LoadScene(1);}
    public void ButtonPartyGamePressed() { SceneManager.LoadScene(2); }
    public void ButtonStatisticPressed()
    {
        json.LoadFromJson(Path.Combine(Application.persistentDataPath, "Data.json"));
        RightAnswerQuantity.text = "ПРАВИЛЬНЫХ ОТВЕТОВ\n" + json.data.RightAnswerQuantity;
        HintQuantity.text = "ПОДСКАЗОК\n" + json.data.HintQuantity;
        UnrightAnswerQuantity.text = "НЕПРАВИЛЬНЫХ ОТВЕТОВ\n" + json.data.UnrightAnswerQuantity;
    }
}
