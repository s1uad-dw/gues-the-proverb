using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace DataController
{
    public class JSONController
    {

        public Data data = new Data();
        public void LoadFromJson(string DataPath)
        {
            data = File.Exists(DataPath) ? JsonConvert.DeserializeObject<Data>(File.ReadAllText(DataPath)) : new Data();
        }
        
        public void SaveToJson(string DataPath)
        {
            File.WriteAllText(DataPath, JsonConvert.SerializeObject(data));
        }

    }
    [System.Serializable]
    public class Data
    {
        public int level = 0;
        public int RightAnswerQuantity;
        public int HintQuantity;
        public int UnrightAnswerQuantity;
        public int PartyPlayersQuantity;
        public int PartyProverbsQuantity;
        public string[,] proverbs =  
        {
            { "�� ���� � ������� ���������� ���� ��������", "�� ����� ������� ��, ��� ����� ���� �����������." },
            { "��� ������� ������ � ���� �� ��ר�", "��� ������������ ������� �������� ��������." },
            { "��� ����� �� �������� � ����� �� �����", "��� ���������� ���� ������� ������ ����� ������������ ������." },
            { "������� �� ����������� ���� �������", "����� ��������� ����� ��������� ������ �����!" }
        };
    }
}