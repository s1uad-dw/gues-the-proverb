using DataController;
using System;
using System.Collections.Generic;
namespace Parser
{
    public class CreateProverb
    {
        JSONController json = new JSONController();
        public string[,] Proverb = new string[30, 4];
        public void WordsList(int ProverbNumber)
        {
            Proverb = new string[30, 4];
            Random rnd = new Random();
            string FullProverb = json.data.proverbs[ProverbNumber, 0];
            List<string> Words = new List<string>(FullProverb.Split(' '));
            string FirstWord = Words[0];
            for (int i = Words.Count - 1; i >= 1; i--){int j = rnd.Next(i + 1);var temp = Words[j];Words[j] = Words[i];Words[i] = temp;}
            string MixedFullProverb = String.Join(" ", Words.ToArray());
            Words.Reverse(); Words.Add(FirstWord); Words.Remove(FirstWord); Words.Reverse();
            string FullProverbHint = String.Join(" ", Words.ToArray());
            //[слово, подсказка, вывод]
            for (int y = 0; y < Proverb.GetLength(0); y++){
                if (Words.Count > y && Words[y].Length > 3){
                    Proverb[y, 0] = Words[y];
                    string Letters = " ";
                    for (int k = 0; k < Words[y].Length / 2; k++){
                        if (Words[y].Length / 2 == k + 1) { Proverb[y, 1] = Words[y]; }
                        int RandomChar = rnd.Next(0, Words[y].Length);
                        if (Words[y][RandomChar] != '?'){ Letters += Words[y][RandomChar] + " "; Words[y] = Words[y].Replace(Words[y][RandomChar], '?');}
                        else { k -= 1; }}
                    Proverb[y, 2] = Words[y]; Proverb[y, 3] = Letters; }
                else if (Words.Count > y) { Words.Remove(Words[y]); y -= 1; }
                else if (Words.Count == y) { Proverb[y, 2] = MixedFullProverb; Proverb[y, 0] = FullProverb; Proverb[y, 1] = FullProverbHint; Proverb[y, 3] = json.data.proverbs[ProverbNumber, 1]; }
            }
        }
    }
}