using System;
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
    public DateTime StartTime;
    public TimeSpan PlayerTime;
    void Start()
    {
        y = -1;
        DataPath = Application.persistentDataPath + "/Data.json";
        json.LoadFromJson(DataPath);
        UpdatePlayer();
    }

    public void UpdatePlayer()
    {
        print(json.data.Players.Count);
        if (json.data.PartyPlayersQuantity < json.data.Players.Count || json.data.Players.Count == 0)
        {
            StartTime = DateTime.Now;
            PlayerInfoTextBox.text = json.data.Players[json.data.CurrentPlayer][0];
            int RandomValue = rnd.Next(0, json.data.proverbs.GetLength(0) - 1);
            while (json.data.UsedProverbs.Contains(RandomValue) && json.data.proverbs.GetLength(0) - 1 > json.data.UsedProverbs.Count)
            {
                RandomValue = rnd.Next(0, json.data.proverbs.GetLength(0) - 1);
            }
            json.data.UsedProverbs.Add(RandomValue);
            json.SaveToJson(DataPath);
            createProverb.WordsList(RandomValue);
            UpdateContent();
        }
        else { print("that's all"); }
        
    }
    public void UpdateContent()
    {
        y += 1;
        json.LoadFromJson(DataPath);
        if (createProverb.Proverb[y, 2] != null){
            InputBox.GetComponentInChildren<Text>().fontSize = createProverb.Proverb[y, 1] != null && createProverb.Proverb[y, 1].Length < 15 ? 50 : 30;
            LettersTextBox.fontSize = createProverb.Proverb[y, 3].Length < 15 ? 50 : 30;

            TextBox.text = createProverb.Proverb[y, 2];
            InputBox.text = "";
            LettersTextBox.text = createProverb.Proverb[y, 3];

            InputBox.ActivateInputField();
            TouchScreenKeyboard.Android.closeKeyboardOnOutsideTap = false;
        }
        else{ 
            json.data.CurrentPlayer = json.data.CurrentPlayer < json.data.Players.Count-1 ? json.data.CurrentPlayer + 1 : 0; 
            y = -1;
            PlayerTime = DateTime.Now - StartTime;
            json.data.Players[json.data.CurrentPlayer][0] = Convert.ToString(Convert.ToInt32(json.data.Players[json.data.CurrentPlayer][0]) + Convert.ToInt32(PlayerTime.TotalSeconds));
            json.SaveToJson(DataPath);
            UpdatePlayer(); 
        }

    }
    public void ButtonReadyPressed()
    {
        if (InputBox.text.ToUpper().Replace('¨', 'Å') == createProverb.Proverb[y, 0].Replace('¨', 'Å'))
        {
            json.data.RightAnswerQuantity += 1;
            json.SaveToJson(DataPath);
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
