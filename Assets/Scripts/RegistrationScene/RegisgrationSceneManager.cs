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
    public void Start(){
        DataPath = Application.persistentDataPath + "/Data.json"; json.LoadFromJson(DataPath);
        if (json.data.Players.Count < json.data.PartyPlayersQuantity || json.data.Players.Count == 0) { UpdateTextBox(); }
        else { SceneManager.LoadScene(3); }}
    public void UpdateAll(){ UpdateData();
        if (json.data.Players.Count < json.data.PartyPlayersQuantity || json.data.Players.Count == 0) { UpdateTextBox(); }
        else { SceneManager.LoadScene(3); }}
    public void UpdateTextBox(){
        TouchScreenKeyboard.Android.closeKeyboardOnOutsideTap = false;
        switch (json.data.PartyStep){
            case 1:
                TextBox.text = "������� ����������� �������"; InputBox.contentType = InputField.ContentType.IntegerNumber; InputBox.ActivateInputField(); break;
            case 2:
                TextBox.text = "������� ����������� �������� ��� ������� ������"; InputBox.contentType = InputField.ContentType.IntegerNumber; InputBox.ActivateInputField(); break;
            default:
                TextBox.text = "������� ��� ������ �" + (json.data.Players.Count + 1); InputBox.contentType = InputField.ContentType.Name; InputBox.ActivateInputField(); break;}}
    public void UpdateData(){
        switch (TextBox.text){
            case "������� ����������� �������":
                if (InputBox.text != ""){ json.data.PartyPlayersQuantity = Convert.ToInt32(InputBox.text); InputBox.text = ""; UpdateTextBox();}
                else{/*��������*/}break;
            case "������� ����������� �������� ��� ������� ������":
                if (InputBox.text != "") { json.data.PartyProverbsQuantity = Convert.ToInt32(InputBox.text); InputBox.text = ""; UpdateTextBox();}
                else {/*��������*/}break;
            default:
                if (InputBox.text != "") { json.data.Players.Add(new string[] { InputBox.text, "0", "0" }); InputBox.text = ""; UpdateTextBox();}
                else {/*��������*/}break;}json.SaveToJson(DataPath);}
    public void AddPlayer(){
        if (InputBox.text != ""){
            json.data.Players.Add(new string[] { InputBox.text, "0", "0", "0" }/*[��� ������, ���������, �����(�������), ���������]*/);
            json.SaveToJson(DataPath);}
        else { json.data.PartyStep -= 1; }}
    public void ButtonNextStepPressed(){json.data.PartyStep += InputBox.text != "" ? 1 : 0;json.SaveToJson(DataPath); json.LoadFromJson(DataPath); UpdateAll(); InputBox.text = "";}
    public void ButtonPreviousStepPressed(){InputBox.text = "";
        if (json.data.PartyStep>=3){json.data.PartyStep = 3; json.data.Players.Clear(); }
        else{json.data.PartyStep -= json.data.PartyStep > 1 ? 1 : 0;}
        json.SaveToJson(DataPath); json.LoadFromJson(DataPath); UpdateAll();}
    public void ButtonMenuPressed(){
        json.data.PartyStep = 1;
        json.data.Players.Clear();
        json.SaveToJson(DataPath);
        json.LoadFromJson(DataPath);
        SceneManager.LoadScene(0);}
}
