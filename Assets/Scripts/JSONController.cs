using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace DataController
{
    public class JSONController
    {

        public Data data = new Data();
        public void LoadFromJson(string DataPath)
        {
            //data = File.Exists(DataPath) ? JsonUtility.FromJson<Data>(File.ReadAllText(DataPath)) : new Data();
            data = File.Exists(DataPath) ? JsonConvert.DeserializeObject<Data>(File.ReadAllText(DataPath)) : new Data();
        }
        
        public void SaveToJson(string DataPath)
        {
            File.WriteAllText(DataPath, JsonConvert.SerializeObject(data));
        }

    }
    [System.Serializable]
    public class Data{
        public int level = 0;
        public int RightAnswerQuantity;
        public int HintQuantity;
        public int UnrightAnswerQuantity;
        public int PartyPlayersQuantity;
        public int PartyProverbsQuantity;
        public int PartyStep = 1;
        public int CurrentPlayer;
        public List<int> UsedProverbs = new List<int>();
        public List<string[]> Players = new List<string[]>();
        public string[] Exercises =
        {"ОТЖИМАНИЯ", "ПРИСЕДАНИЯ"};
        public string[,] proverbs =  
        {
            { "НЕ ПЛЮЙ В КОЛОДЕЦ ПРИГОДИТСЯ ВОДЫ НАПИТЬСЯ", "НЕ СТОИТ ПОРТИТЬ ЧТО-ТО ПРОСТО ТАК, ВЕДЬ ОНО МОЖЕТ ПОНАДОБИТЬСЯ." },
            { "АППЕТИТ ПРИХОДИТ ВО ВРЕМЯ ЕДЫ", "ЧЕМ ГЛУБЖЕ ВНИКАЕШЬ К ЧЕМУ-ЛИБО, ТЕМ БОЛЬШЕ ЭТО УЗНАЁШЬ." },
            { "БАБА С ВОЗУ КОБЫЛЕ ЛЕГЧЕ", "ОБ УХОДЕ НЕНУЖНОГО ЧЕЛОВЕКА, НЕ СТОЛЬ ПОЛЕЗНОГО ЧЕМУ-ЛИБО." },
            { "БЕДА НИКОГДА НЕ ПРИХОДИТ ОДНА", "ОНА ОБЯЗАТЕЛЬНО «ПРИХВАТИТ» ЗА СОБОЙ ЕЩЁ ОДНУ." }
    };
    }
}