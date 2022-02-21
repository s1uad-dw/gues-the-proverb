using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataController;
using Parser;

public class PartyGameSceneManager : MonoBehaviour
{
    System.Random rnd = new System.Random();
    JSONController json = new JSONController();
    CreateProverb createProverb = new CreateProverb();

    public Text TextBox;
    public InputField InputBox;
    public Text PlayerInfoTextBox;
    public Text LettersTextBox;
    public string DataPath;
    public int y;
    // Start is called before the first frame update
    void Start()
    {
        y = -1;
        DataPath = Application.persistentDataPath + "/Data.json";
        json.LoadFromJson(DataPath);
        UpdatePlayer();
    }

    // Update is called once per frame
    public void UpdatePlayer()
    {
        //корутина
        
        print("current player -------- " + json.data.CurrentPlayer);
        PlayerInfoTextBox.text = json.data.Players[json.data.CurrentPlayer][0];
        int RandomValue = rnd.Next(0, json.data.proverbs.GetLength(0) - 1);
        while (json.data.UsedProverbs.Contains(RandomValue) && json.data.proverbs.GetLength(0)-1!=json.data.UsedProverbs.Count) {RandomValue = rnd.Next(0, json.data.proverbs.GetLength(0) - 1);}
        print("random value -------- " + RandomValue);
        json.data.UsedProverbs.Add(RandomValue);
        json.SaveToJson(DataPath);
        createProverb.WordsList(RandomValue); 
        UpdateContent();
    }
    public void UpdateContent()
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
        else{ json.data.CurrentPlayer = json.data.CurrentPlayer < json.data.Players.Count ? json.data.CurrentPlayer + 1 : 0; y = -1; UpdatePlayer(); }

    }
    public void ButtonReadyPressed()
    {
        if (InputBox.text.ToUpper().Replace('Ё', 'Е') == createProverb.Proverb[y, 0].Replace('Ё', 'Е'))
        {
            json.data.RightAnswerQuantity += 1;
            json.SaveToJson(DataPath);
            //корутина
            UpdateContent();
        }
        else
        {
            json.data.UnrightAnswerQuantity += 1;
            json.SaveToJson(DataPath);
        }
    }
    public void ButtonHintPressed()
    {
        TextBox.text = createProverb.Proverb[y, 1];
        InputBox.ActivateInputField();
        json.data.HintQuantity += 1;
        json.SaveToJson(DataPath);
    }
}
