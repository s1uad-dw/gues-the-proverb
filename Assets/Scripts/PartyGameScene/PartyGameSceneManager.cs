using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataController;
using Parser;
using UnityEngine.SceneManagement;

public class PartyGameSceneManager : MonoBehaviour
{
    System.Random rnd = new System.Random();
    JSONController json = new JSONController();
    CreateProverb createProverb = new CreateProverb();

    public Text TextBox;
    public InputField InputBox;
    public Text PlayerInfoTextBox;
    public GameObject ButtonReady;
    public GameObject ButtonHint;
    public Text LettersTextBox;
    public GameObject ResultListWindow;
    public Text ResultTextBox;
    public string DataPath;
    public int y;
    public DateTime StartTime;
    public TimeSpan PlayerTime;
    public string ResultListValue;
    public string[] BestPlayer;
    public double BestResult = 10000000;
    void Start()
    {
        y = -1;
        DataPath = Path.Combine(Application.persistentDataPath, "Data.json");
        json.LoadFromJson(DataPath);
        UpdatePlayer();
    }
    public void UpdatePlayer(){
        /*êîðóòèíà*/
        if (json.data.UsedProverbs.Count > json.data.PartyPlayersQuantity * json.data.PartyProverbsQuantity) { ResultList(); return; }
        if (json.data.CurrentPlayer >= json.data.PartyPlayersQuantity){json.data.CurrentPlayer = 0;}
        PlayerInfoTextBox.text = json.data.Players[json.data.CurrentPlayer][0];
        StartTime = DateTime.Now; json.SaveToJson(DataPath); UpdateContent();}
    public void UpdateContent(){y += 1;json.LoadFromJson(DataPath);
        if (json.data.UsedProverbs.Count == 0){createProverb.WordsList(CreateProverb());}
        if (createProverb.Proverb[y, 2] != null){
            InputBox.GetComponentInChildren<Text>().fontSize = createProverb.Proverb[y, 1] != null && createProverb.Proverb[y, 0].Length < 20 ? 50 : 30;
            LettersTextBox.fontSize = createProverb.Proverb[y, 3].Length < 15 ? 50 : 30;
            TextBox.text = createProverb.Proverb[y, 2];InputBox.text = "";LettersTextBox.text = createProverb.Proverb[y, 3];
            InputBox.ActivateInputField();TouchScreenKeyboard.Android.closeKeyboardOnOutsideTap = false;}
        else{y = -1;
            if (json.data.UsedProverbs.Count >= json.data.proverbs.GetLength(0)){
                TextBox.text = "БОЛЬШЕ ПОСЛОВИЦ В ПЛАТНОЙ ВЕРСИИ ИГРЫ!";
                ButtonReady.SetActive(false); ButtonHint.SetActive(false);}
            else {PlayerTime = DateTime.Now - StartTime;
                json.data.Players[json.data.CurrentPlayer][1] = Convert.ToString(Convert.ToDouble(json.data.Players[json.data.CurrentPlayer][1]) + PlayerTime.TotalSeconds);
                json.data.CurrentPlayer += 1;
                createProverb.WordsList(CreateProverb());
                json.SaveToJson(DataPath); UpdatePlayer();}}
    }
    public int CreateProverb(){int CurrentProverb = 0;
        for (int i = 0; i < 1; i++){CurrentProverb = rnd.Next(0, json.data.proverbs.GetLength(0));
            if (json.data.UsedProverbs.Contains(CurrentProverb)){i--;}
            else{json.data.UsedProverbs.Add(CurrentProverb);}} json.SaveToJson(DataPath); return CurrentProverb;}

    public void ResultList(){ ResultListWindow.SetActive(true); ResultListValue = ""; PlayerInfoTextBox.text = "";
        foreach (var player in json.data.Players){if (BestResult > (Convert.ToDouble(player[1]) * 0.1 * (Convert.ToDouble(player[2])+1))){
                BestResult = (Convert.ToDouble(player[1]) * 0.1 * (Convert.ToDouble(player[2])+1)); BestPlayer = player;}}
        ResultListValue += BestPlayer[0] + " - ПОБЕДИТЕЛЬ\n\n\n"; json.data.Players.Remove(BestPlayer);
        foreach (var player in json.data.Players){ResultListValue += player[0] + "\n" + json.data.Exercises[rnd.Next(0, json.data.Exercises.Length)] 
                + " - " + Convert.ToString(Math.Round(Convert.ToDouble(player[1]) * 0.1 * (Convert.ToDouble(player[2]) + 1), 0)) + "\n\n";}
        ResultTextBox.text = ResultListValue;}
    public void ButtonReadyPressed(){
        if (InputBox.text.ToUpper().Replace('Ё', 'Е') == createProverb.Proverb[y, 0].Replace('Ё', 'Е')){
            json.data.RightAnswerQuantity += 1;
            json.SaveToJson(DataPath);
            UpdateContent();}
        else{
            /*êîðóòèíà*/
            json.SaveToJson(DataPath);}}
    public void ButtonHintPressed(){
        TextBox.text = createProverb.Proverb[y, 1];
        InputBox.ActivateInputField();
        json.data.Players[json.data.CurrentPlayer][2] = Convert.ToString(Convert.ToInt32(json.data.Players[json.data.CurrentPlayer][2]) + 1);
        json.SaveToJson(DataPath);}
    public void ButtonRestartPressed(){json.data.PartyPlayersQuantity = 0; json.data.PartyProverbsQuantity = 0;
        json.data.PartyStep = 1; json.data.CurrentPlayer = 0; json.data.UsedProverbs.Clear();
        json.data.Players.Clear(); json.SaveToJson(DataPath); SceneManager.LoadScene(2);}
    public void ButtonMenuPressed() {json.SaveToJson(DataPath);json.LoadFromJson(DataPath); SceneManager.LoadScene(0);}
}
