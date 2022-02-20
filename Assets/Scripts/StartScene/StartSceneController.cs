using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DataController;

public class StartSceneController : MonoBehaviour
{
    public Text RightAnswerQuantity;
    public Text HintQuantity;
    public Text UnrightAnswerQuantity;
    JSONController json = new JSONController();

    public void SoloGameButtonPressed() {SceneManager.LoadScene(1);}
    public void ButtonStatisticPressed()
    {
        json.LoadFromJson(Application.persistentDataPath + "/Data.json");
        RightAnswerQuantity.text = "���������� �������\n" + json.data.RightAnswerQuantity;
        HintQuantity.text = "���������\n" + json.data.HintQuantity;
        UnrightAnswerQuantity.text = "������������ �������\n" + json.data.UnrightAnswerQuantity;
    }
}
