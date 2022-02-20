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
        RightAnswerQuantity.text = "闲缆人芡壅 我屡椅耚n" + json.data.RightAnswerQuantity;
        HintQuantity.text = "衔难世俏蔦n" + json.data.HintQuantity;
        UnrightAnswerQuantity.text = "团闲缆人芡壅 我屡椅耚n" + json.data.UnrightAnswerQuantity;
    }
}
