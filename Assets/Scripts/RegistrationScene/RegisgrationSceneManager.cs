using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DataController;
using Coroutines;

public class RegisgrationSceneManager : MonoBehaviour
{
    JSONController json = new JSONController();
    Cor cor = new Cor();
    public Text TextBox;
    public InputField InputBox;
    public Button ButtonNext;
    public Button ButtonPrevious;
    public string DataPath;
    public void Start(){
        DataPath = Path.Combine(Application.persistentDataPath, "Data.json");

        if (json.data.Players.Count < json.data.PartyPlayersQuantity || json.data.Players.Count == 0) { UpdateTextBox(); }
        else { SceneManager.LoadScene(3); }}
    public void UpdateAll(){ UpdateData();
        if (json.data.Players.Count < json.data.PartyPlayersQuantity || json.data.Players.Count == 0) { UpdateTextBox(); }
        else { SceneManager.LoadScene(3); }}
    public void UpdateTextBox(){
        TouchScreenKeyboard.Android.closeKeyboardOnOutsideTap = false;
        switch (json.data.PartyStep){
            case 1:
                TextBox.text = "ВВЕДИТЕ КОЛИЧЕСТВО ИГРОКОВ\n(2-3)"; InputBox.contentType = InputField.ContentType.IntegerNumber; InputBox.ActivateInputField(); break;
            case 2:
                TextBox.text = "КОЛИЧЕСТВО ПОСЛОВИЦ НА КАЖДОГО ИГРОКА\n(максимум 1)"; InputBox.contentType = InputField.ContentType.IntegerNumber; InputBox.ActivateInputField(); break;
            default:
                TextBox.text = "ВВЕДИТЕ ИМЯ ИГРОКА №" + (json.data.Players.Count + 1); InputBox.contentType = InputField.ContentType.Name; InputBox.ActivateInputField(); break;}}
    public void UpdateData(){
        switch (TextBox.text){
            case "ВВЕДИТЕ КОЛИЧЕСТВО ИГРОКОВ\n(2-3)":
                if (InputBox.text == "1"){json.data.PartyPlayersQuantity = 0; json.data.PartyProverbsQuantity = 0;
                    json.data.PartyStep = 1; json.data.CurrentPlayer = 0; json.data.UsedProverbs.Clear();
                    json.data.Players.Clear(); json.SaveToJson(DataPath); SceneManager.LoadScene(1);}
                if (InputBox.text == "2" || InputBox.text == "3") { json.data.PartyPlayersQuantity = Convert.ToInt32(InputBox.text); InputBox.text = ""; UpdateTextBox();}
                else{ /*StartCoroutine(cor.MessageCoroutine(2f, "Íåâåðíî ââåäåíû äàííûå" new Button[]{ButtonNext, ButtonPrevious }));*/ }break;
            case "КОЛИЧЕСТВО ПОСЛОВИЦ НА КАЖДОГО ИГРОКА\n(максимум 1)":
                if (InputBox.text == "1") { json.data.PartyProverbsQuantity = Convert.ToInt32(InputBox.text); InputBox.text = ""; UpdateTextBox();}
                else {/*êîðóòèíà*/}break;
            default:
                if (InputBox.text != "") { json.data.Players.Add(new string[] { InputBox.text, "0", "0" }); InputBox.text = ""; UpdateTextBox();}
                else {/*êîðóòèíà*/}break;}json.SaveToJson(DataPath);}
    public void AddPlayer(){
        if (InputBox.text != ""){
            json.data.Players.Add(new string[] { InputBox.text, "0", "0", "0" }/*[èìÿ èãðîêà, ïîñëîâèöû, âðåìÿ(ñåêóíäû), ïîäñêàçêè]*/);
            json.SaveToJson(DataPath);}
        else { json.data.PartyStep -= 1; }}
    public void ButtonNextStepPressed(){json.data.PartyStep += InputBox.text != "" ? 1 : 0; json.SaveToJson(DataPath); UpdateAll(); InputBox.text = "";}
    public void ButtonPreviousStepPressed(){InputBox.text = "";
        if (json.data.PartyStep>=3){json.data.PartyStep = 3; json.data.Players.Clear(); }
        else{json.data.PartyStep -= json.data.PartyStep > 1 ? 1 : 0;}
        json.SaveToJson(DataPath); UpdateAll();}
    public void ButtonMenuPressed(){
        json.data.PartyStep = 1;
        json.data.Players.Clear();
        json.SaveToJson(DataPath);
        json.LoadFromJson(DataPath);
        SceneManager.LoadScene(0);}
    public void ButtonRestartPressed(){json.data.PartyPlayersQuantity = 0; json.data.PartyProverbsQuantity = 0;
        json.data.PartyStep = 1; json.data.CurrentPlayer = 0; json.data.UsedProverbs.Clear();
        json.data.Players.Clear(); json.SaveToJson(DataPath); SceneManager.LoadScene(2);}
}
