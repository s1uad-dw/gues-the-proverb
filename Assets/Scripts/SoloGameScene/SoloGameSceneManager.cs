using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Parser;
using DataController;

public class SoloGameSceneManager : MonoBehaviour
{
    public int x, y;
    public Text TextBox;
    public InputField InputBox;
    public Text LevelInfoTextBox;
    public Text LettersTextBox;
    CreateProverb createProverb = new CreateProverb();
    JSONController json = new JSONController();
    System.Random rnd = new System.Random();
    private string DataPath;
    void Start()
    {
        DataPath = Application.persistentDataPath + "/Data.json";
        x = 0; y = 0;
        json.LoadFromJson(DataPath);
        if (json.data.level>=json.data.proverbs.GetLength(0)){TextBox.text = "Поздравляю, вы прошли игру...";}
        else
        {
            createProverb.WordsList(json.data.level);
            LevelInfoTextBox.text = "УРОВЕНЬ " + (json.data.level+1);
            UpdateTextBox();
            TouchScreenKeyboard.Android.closeKeyboardOnOutsideTap = false;
        }
        
    }

    // Update is called once per frame
    public void UpdateTextBox(){
        InputBoxManager();
        if (createProverb.Proverb[y, 2] != null && json.data.level < json.data.proverbs.GetLength(0)) { TextBox.text = createProverb.Proverb[y, 2]; AnnotationManager(); InputBox.ActivateInputField();; }
        else  if (json.data.level < json.data.proverbs.GetLength(0)){ y = 0; LevelUp(); createProverb.WordsList(json.data.level); TextBox.text = createProverb.Proverb[y, 2]; AnnotationManager(); InputBox.ActivateInputField(); }
    }
    public void LevelUp(){
        json.data.level += 1;
        LevelInfoTextBox.text = "УРОВЕНЬ " + (json.data.level+1);
        json.SaveToJson(DataPath);

    }
    public void ButtonReadyPressed()
    {
        if (InputBox.text.ToUpper().Replace('Ё', 'Е') == createProverb.Proverb[y, 0].Replace('Ё', 'Е')){ y += 1; UpdateTextBox(); InputBoxManager(); }
    }
    public void ButtonHintPressed()
    {
        TextBox.text = createProverb.Proverb[y, 1];
        InputBox.ActivateInputField();
    }
    public void InputBoxManager(){
        InputBox.GetComponentInChildren<Text>().fontSize = createProverb.Proverb[y, 1].Length < 15 ? 50 : 30;
        InputBox.text = "";}
    public void AnnotationManager(){
        LettersTextBox.fontSize = createProverb.Proverb[y, 3].Length < 15 ? 50 : 30;
        LettersTextBox.text = createProverb.Proverb[y, 3];
        print(createProverb.Proverb[y, 3]);
    }
}
