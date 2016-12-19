using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueContainer : IComparable<DialogueContainer> {
  
  public string name;
  public string[] dialogeText;

  public DialogueContainer(string newName, string[] newDialogueText) {
    name = newName;
    dialogeText = newDialogueText;
  }

  public int CompareTo (DialogueContainer other) {
    if (other == null) {
      return 1;
    }
    return 0;
  }
}