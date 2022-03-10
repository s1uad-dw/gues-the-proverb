using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

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
        public int PartyStep = 1;
        public int CurrentPlayer;
        public List<int> UsedProverbs = new List<int>();
        public List<string[]> Players = new List<string[]>();
        public string[,] proverbs =  
        {
            { "�� ���� � ������� ���������� ���� ��������", "�� ����� ������� ��, ��� ����� ���� �����������." },
            { "��� ������� ������ � ���� �� ��ר�", "��� ������������ ������� �������� ��������." },
            { "��� ����� �� �������� � ����� �� �����", "��� ���������� ���� ������� ������ ����� ������������ ������." },
            { "������� �� ����������� ���� �������", "����� ��������� ����� ��������� ������ �����!" }
        };
    }
}