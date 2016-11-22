using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Dialogue_Choice_Manager : MonoBehaviour, IPointerDownHandler {
    /*void Update() {
        if (UI_Choice_Menu.ChoiceBoxActive == true && Input.GetKeyDown(KeyCode.Z)) {
            Dialogue_Manager.Line = optionLines[Dialogue_Choice_Settings.Chosen] + 1 - 3;
            UI_Choice_Menu.CloseChoiceBoxes();

            startLine = optionLines[Dialogue_Choice_Settings.Chosen] + 1;
            ProcessDialogueChoices();
        }
    }*/

    static List<string> newTextArray;
    static List<int> openLines;
    static List<int> optionLines;
    static List<int> endOptionLines;

    static int openCount;
    static int optionCount;
    static int startLine;
    static int endLine;

    static bool openActivated;

    public static void ProcessDialogueChoices() {
        //UI_Choice_Menu.ResetChoiceOptions();
        string[] dialogueArray = Dialogue_Manager.TextArray;

        newTextArray = new List<string>(dialogueArray);
        openLines = new List<int>();
        optionLines = new List<int>();
        endOptionLines = new List<int>();

        openCount = 0;
        optionCount = 0;
        endLine = -1;
        openActivated = false;

        for (int i = startLine; i < newTextArray.Count; i++) {
            if (newTextArray[i] != null) {
                if (newTextArray[i].Contains("<open>")) {
                    openCount++;
                    openLines.Add(i);
                    openActivated = true;

                    if (openCount == 1) {
                        newTextArray[i] = newTextArray[i].Remove(newTextArray[i].IndexOf("<open>"), "<open>".Length);
                    }
                }
                else if (newTextArray[i].Contains("</open>")) {
                    if (openCount == 1) {
                        newTextArray[i] = newTextArray[i].Remove(newTextArray[i].IndexOf("</open>"), "</open>".Length);
                    }

                    openCount--;
                }

                if (newTextArray[i].Contains("<option>")) {
                    if ((i + 1) % 3 != 0) {
                        newTextArray.Insert(i, "");
                        continue;
                    }

                    optionCount++;

                    if (openCount == 1) {
                        optionLines.Add(i);

                        UI_Choice_Menu.AddChoiceOption(newTextArray[i].Remove(newTextArray[i].IndexOf("<option>"), "<option>".Length).TrimStart());
                        newTextArray[i] = "";
                    }
                }
                else if (newTextArray[i].Contains("</option>")) {
                    optionCount--;

                    if (optionCount == -1) {
                        endLine = i;
                        newTextArray[i] = newTextArray[i].Remove(newTextArray[i].IndexOf("</option>"), "</option>".Length);
                        newTextArray[i + 1] = "";
                        newTextArray[i + 2] = "";
                        break;
                    }

                    if (openCount == 1) {
                        endOptionLines.Add(i);
                    }
                }
                if (openActivated && openCount == 0) {
                    endLine = i;
                    break;
                }

                newTextArray[i] = newTextArray[i].TrimStart();
            }
        }

        Dialogue_Manager.TextArray = newTextArray.ToArray();
    }

    static int chosenBox;
    public void OnPointerDown(PointerEventData pointerData) {
        if (pointerData.pointerCurrentRaycast.gameObject.transform.parent.tag == "Choice") {
            chosenBox = int.Parse(pointerData.pointerCurrentRaycast.gameObject.transform.parent.name);

            UI_Choice_Menu.CloseChoiceBoxes();

            Dialogue_Manager.Line = optionLines[chosenBox] + 1 - 3;
            startLine = optionLines[chosenBox] + 1;

            ProcessDialogueChoices();
        }
    }


    public static int OpenLine {
        get {
            return openLines.Count > 0 ? openLines[0] : -1;
        }
    }

    public static int EndLine {
        get {
            return endLine;
        }
    }

    public static int StartLine {
        set {
            startLine = value;
        }
    }
}