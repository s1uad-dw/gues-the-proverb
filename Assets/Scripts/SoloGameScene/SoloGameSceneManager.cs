using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Parser;
using DataController;
using UnityEngine.SceneManagement;
public class SoloGameSceneManager : MonoBehaviour
{
    public int y;
    public Text TextBox;
    public InputField InputBox;
    public Text LevelInfoTextBox;
    public Text LettersTextBox;
    CreateProverb createProverb = new CreateProverb();
    JSONController json = new JSONController();
    public string DataPath;
    void Start()
    {
        DataPath = Application.persistentDataPath + "/Data.json";
        y = -1;
        json.LoadFromJson(DataPath);
        if (json.data.level < json.data.proverbs.GetLength(0))
        {
            createProverb.WordsList(json.data.level);
            LevelInfoTextBox.text = "”–Œ¬≈Õ‹ " + (json.data.level + 1);
            UpdateAll();
        }
    }

    // Update is called once per frame
    public void UpdateAll()
    {
        y += 1;

        json.LoadFromJson(DataPath);
        if (createProverb.Proverb[y, 2] != null)
        {

            InputBox.GetComponentInChildren<Text>().fontSize = createProverb.Proverb[y, 1] != null && createProverb.Proverb[y, 1].Length < 15 ? 50 : 30;
            LettersTextBox.fontSize = createProverb.Proverb[y, 3].Length < 15 ? 50 : 30;

            TextBox.text = createProverb.Proverb[y, 2];
            InputBox.text = "";
            LettersTextBox.text = createProverb.Proverb[y, 3];

            InputBox.ActivateInputField();
            TouchScreenKeyboard.Android.closeKeyboardOnOutsideTap = false; 
        }
        else
        {
            y = -1;
            if (json.data.proverbs.GetLength(0) - json.data.level > 1)
            {
                json.data.level += 1;
                json.SaveToJson(DataPath);
                LevelInfoTextBox.text = "”–Œ¬≈Õ‹ " + (json.data.level+1);
                createProverb.WordsList(json.data.level);
                UpdateAll();
            }
            
        }
    }
    public void ButtonReadyPressed()
    {
        if (InputBox.text.ToUpper().Replace('®', '≈') == createProverb.Proverb[y, 0].Replace('®', '≈'))
        {
            json.data.RightAnswerQuantity += 1;
            json.SaveToJson(DataPath);
            //ÍÓÛÚËÌ‡
            UpdateAll();
        }
        else
        {
            json.data.UnrightAnswerQuantity += 1;
            json.SaveToJson(DataPath);
        }
    }
    public void ButtonHintPressed(){TextBox.text = createProverb.Proverb[y, 1];InputBox.ActivateInputField();
        json.data.HintQuantity += 1;json.SaveToJson(DataPath);}
    public void ButtonMenuPressed() { json.SaveToJson(DataPath); json.LoadFromJson(DataPath); SceneManager.LoadScene(0); }
}
