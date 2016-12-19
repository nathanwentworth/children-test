using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DialogueParse : MonoBehaviour {

  private List<DialogueContainer> dialogue;

  private void Start () {
    dialogue.Clear();
  }

  private bool ParseDialogue() {
    try {
      string line;
      StreamReader file = new StreamReader(Application.dataPath + "/Scripts/Dialogue.txt");

      using (file) {
        do {
          line = file.ReadLine();

          if (line != null) {
            string[] lineArr = line.Split('/');
            if (lineArr.Length > 1) {
              string[] dialogueArr = {};
              for (int i = 1; i < lineArr.Length; i ++) {
                dialogueArr[i - 1] = lineArr[i];
              }
              dialogue.Add(new DialogueContainer(lineArr[0], dialogueArr));
              
            }
          }
        }
  
        while (line != null);
        file.Close();
        return true;
    
      }


    }
    catch (Exception e) {
      Console.WriteLine("{0}\n", e.Message);
      return false;
    }


  }
}
