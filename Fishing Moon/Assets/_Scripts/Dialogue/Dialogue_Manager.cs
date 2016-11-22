using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour {
    static Dialogue_Manager holder;
    void Awake() {
        holder = this;
    }

    static bool canTalk = true;
    public static void InputDialogue(TextAsset dialogueText) {
        if (canTalk == true) ProcessDialogue(dialogueText);
        else Debug.Log("canTalk is false or monologue mode is active.");
    }

    static string[] textArray;
    static int line = 0;
    static bool isTalking;
    static void ProcessDialogue (TextAsset textAsset) {
        /*if (Input.GetKeyDown(KeyCode.X) && letter != outputString.Length) {
            letter = outputString.Length;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.X))
            return;*/

        if (textArray == null || textArray.Length == 0) {
            //opens up ui
            holder.transform.parent.GetComponent<Image>().enabled = true;
            holder.GetComponent<Text>().enabled = true;

            if (Monologue_Mode.MonologueModeActive == false && Dialogue_Output.TalkingNPC.GetComponent<Character_Potrait>() != null)
                UI_Potrait_And_Name.Instance.EnableUI();
            else if (Dialogue_Output.TalkingNPC.GetComponent<Character_Potrait>() != null)
                UI_Potrait_And_Name.Instance.DisableUI();

            //UI_Dialogue_Position.RepositionDialogueUI();

            textArray = textAsset.ToString().Split('\n');

            isTalking = true;
            Pathfinding_Cursor.CanMove = false;

            while (textArray.Length % 3 != 0) {
                System.Array.Resize(ref textArray, textArray.Length + 1);
            }

            Dialogue_Choice_Manager.StartLine = 0;
            Dialogue_Choice_Manager.ProcessDialogueChoices();
        }
        else if (letter != outputString.Length) {
            letter = outputString.Length;
            return;
        }
        else if (UI_Choice_Menu.ChoiceBoxActive == true) {
            return;
        }
        else if (Dialogue_Choice_Manager.EndLine >= line && Dialogue_Choice_Manager.EndLine < line + 3) {
            holder.StartCoroutine(holder.EndDialogue());
            return;
        }
        else if (line + 3 != textArray.Length) {
            line += 3;
        }
        else {
            holder.StartCoroutine(holder.EndDialogue());
            return;
        }

        outputString = (textArray[line] + "\n" + textArray[line + 1] + "\n" + textArray[line + 2]).TrimStart();
        DialogueCommands();

        holder.StartCoroutine(holder.IterateDialogue());
    }

    static bool dialogueDone;
    IEnumerator EndDialogue() {
        dialogueDone = true;

        yield return null;

        holder.transform.parent.GetComponent<Image>().enabled = false;
        holder.GetComponent<Text>().enabled = false;

        holder.transform.parent.parent.GetChild(1).gameObject.SetActive(false);

        Dialogue_Output.TalkingNPC = null;
        isTalking = false;
        Pathfinding_Cursor.CanMove = true ;

        line = 0;
        textArray = null;
    }

    static string outputString;
    static int letter;
    IEnumerator IterateDialogue() {
        letter = 0;
        GetComponent<Text>().text = "";

        while (letter < outputString.Length) {
            GetComponent<Text>().text += outputString[letter];

            letter++;

            yield return new WaitForSeconds(0.03f);
        }

        GetComponent<Text>().text = outputString;

        if (Dialogue_Choice_Manager.OpenLine != -1 && Dialogue_Choice_Manager.OpenLine >= line && Dialogue_Choice_Manager.OpenLine < line + 3) {
            UI_Choice_Menu.OpenChoiceBoxes();
        }
    }

    [SerializeField] Transform television;
    static void DialogueCommands() {
        if (outputString.Contains("</porn>")) {
            holder.television.GetComponent<Animator>().SetBool("wPorn", true);
            outputString = outputString.Remove(outputString.IndexOf("</porn>"), "</porn>".Length);
        }
        if (outputString.Contains("</next>")) {
            Dialogue_Output.TalkingNPC.GetComponent<Dialogue_Output>().CurrentDialogue++;
            outputString = outputString.Remove(outputString.IndexOf("</next>"), "</next>".Length);
        }
        if (outputString.Contains("<playertalk>")) {
            //Dialogue_Output.TalkingNPC = Player_Info.Player;
            Talking_NPC_Name.SetName(Player_Info.Player.name);
            UI_Potrait_Behaviour.DisplayPotrait(Player_Info.Player.GetComponent<Character_Potrait>().PotraitImage);
            outputString = outputString.Remove(outputString.IndexOf("<playertalk>"), "<playertalk>".Length);
        }
        else if (outputString.Contains("</playertalk>")) {
            //Dialogue_Output.TalkingNPC = Dialogue_Output.TalkableCharacter;
            Talking_NPC_Name.SetName(Dialogue_Output.TalkingNPC.name);
            UI_Potrait_Behaviour.DisplayPotrait(Dialogue_Output.TalkingNPC.GetComponent<Character_Potrait>().PotraitImage);
            outputString = outputString.Remove(outputString.IndexOf("</playertalk>"), "</playertalk>".Length);
        }

        if (outputString.Contains("/player")) {
            outputString = outputString.Replace("/player", Player_Info.PlayerName);
        }

        //events
        if (outputString.Contains("</jonnyevent>")) {
            //vent_Manager_0.JonnyMistyEvent();
            //outputString = outputString.Remove(outputString.IndexOf("</jonnyevent>"), "</jonnyevent>".Length);
        }
    }

    public static bool DialogueDone {
        get {
            return dialogueDone;
        }
    }

    public static bool IsTalking {
        get {
            return isTalking;
        }
    }

    public static bool CanTalk {
        get {
            return canTalk;
        }
        set {
            canTalk = value;
        }
    }

    public static string[] TextArray {
        get {
            return textArray;
        }
        set {
            textArray = value;
        }
    }

    public static int Line {
        get {
            return line;
        }
        set {
            line = value;
        }
    }
}