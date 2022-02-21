using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DataController;

public class RegisgrationSceneManager : MonoBehaviour
{
    JSONController json = new JSONController();

    public Text TextBox;
    public InputField InputBox;
    public string DataPath;
    public void Start()
    {
        DataPath = Application.persistentDataPath + "/Data.json";
        json.LoadFromJson(DataPath);
        UpdateAll(json.data.PartyStep);
    }
    public void UpdateAll(int Step){
        print(Step);
        TouchScreenKeyboard.Android.closeKeyboardOnOutsideTap = false;
        switch (Step){
            case 1:
                TextBox.text = "ббедхре йнкхвеярбн хцпнйнб";
                InputBox.contentType = InputField.ContentType.IntegerNumber; InputBox.ActivateInputField();break;
            case 2:
                json.data.PartyPlayersQuantity = InputBox.text != "" ? Convert.ToInt32(InputBox.text) : 0;
                TextBox.text = "ббедхре йнкхвеярбн онякнбхж дкъ йюфднцн хцпнйю";
                InputBox.contentType = InputField.ContentType.IntegerNumber; InputBox.ActivateInputField();break;
            case 3:
                json.data.PartyProverbsQuantity = InputBox.text != "" ? Convert.ToInt32(InputBox.text) : 0;
                TextBox.text = "ббедхре хлъ хцпнйю ╧" + json.data.RegistrationCurrentPlayer; InputBox.contentType = InputField.ContentType.Name;
                InputBox.ActivateInputField(); break;
            default:
                if (json.data.Players.Count<json.data.PartyPlayersQuantity){
                    AddPlayer();
                    TextBox.text = "ббедхре хлъ хцпнйю ╧" + json.data.RegistrationCurrentPlayer;
                    InputBox.contentType = InputField.ContentType.Name;}
                else { SceneManager.LoadScene(3); }
                break;
        }
    }
                

    public void AddPlayer()
    {
        if (InputBox.text != ""){
            json.data.Players.Add(new string[] { InputBox.text, "0", "0", "0" });
            json.data.RegistrationCurrentPlayer += 1;
            json.SaveToJson(DataPath);}
        else { json.data.PartyStep -= 1; }
    }


    public void ButonNextStepPressed(){
        json.data.PartyStep += InputBox.text != "" ? 1 : 0;
        json.SaveToJson(DataPath);
        json.LoadFromJson(DataPath);
        UpdateAll(json.data.PartyStep);
        InputBox.text = "";}
    public void ButonPreviousStepPressed(){
        InputBox.text = "";
        json.data.PartyStep -= json.data.PartyStep > 1 ? 1 : 0;
        json.data.RegistrationCurrentPlayer -= json.data.RegistrationCurrentPlayer > 1 ? 1 : 0;
        json.SaveToJson(DataPath);
        json.LoadFromJson(DataPath);
        UpdateAll(json.data.PartyStep);}
    public void ButtonMenuPressed()
    {
        json.data.PartyStep = 1;
        json.data.RegistrationCurrentPlayer = 1;
        json.data.Players.Clear();
        json.SaveToJson(DataPath);
        json.LoadFromJson(DataPath);
        SceneManager.LoadScene(0);
    }
}
