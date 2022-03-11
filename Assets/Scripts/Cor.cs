using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Coroutines
{public class Cor : MonoBehaviour{
        public Text TextBox;
        public IEnumerator MessageCoroutine(float Time, string CorText/*, Button[] Buttons*/){string Text = TextBox.text;
            TextBox.text = CorText; //foreach (var btn in Buttons){btn.interactable = false;}
            yield return new WaitForSeconds(Time);
            TextBox.text = Text; /*foreach (var btn in Buttons) { btn.interactable = true; }*/}
    }
}
