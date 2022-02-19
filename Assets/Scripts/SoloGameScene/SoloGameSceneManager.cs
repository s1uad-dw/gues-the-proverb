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
    CreateProverb createProverb = new CreateProverb();
    JSONController json = new JSONController();
    System.Random rnd = new System.Random();
    private string DataPath;
    void Start()
    {
        DataPath = Application.persistentDataPath + "/Data.json";
        x = 0; y = 0;
        json.LoadFromJson(DataPath);
        if (json.data.level>json.data.proverbs.GetLength(0)){TextBox.text = "Поздравляю, вы прошли игру...";}
        else
        {
            createProverb.WordsList(json.data.level);
            UpdateTextBox();
        }
        
    }

    // Update is called once per frame
    public void UpdateTextBox(){
        if (createProverb.Proverb[y, x] != null){ TextBox.text = createProverb.Proverb[y, 2]; }
        else { LevelUp(); createProverb.WordsList(json.data.level); TextBox.text = createProverb.Proverb[0, 2]; }
    }
    public void LevelUp(){
        json.data.level += 1;
        json.SaveToJson(DataPath);
    }
    public void ButtonReadyPressed()
    {
        if (InputBox.text.ToUpper().Replace('Ё', 'Е') == createProverb.Proverb[y, 0].Replace('Ё', 'Е')){ y += 1; UpdateTextBox(); InputBox.text = ""; }
    }
    public void ButtonHintPressed()
    {
        TextBox.text = createProverb.Proverb[y, 1];
    }
}
