using UnityEngine;
using System.Collections;

public class UI_Dialogue_Position : MonoBehaviour {
    static UI_Dialogue_Position holder;
    void Awake() {
        holder = this;
    }

    public static void RepositionDialogueUI() {
        RectTransform dialogueFrame = holder.transform.parent.GetChild(0).GetComponent<RectTransform>();
        RectTransform dialogueUI = holder.transform.GetComponent<RectTransform>();

        if (Dialogue_Output.TalkingNPC.position.x > Camera.main.transform.position.x) {
            dialogueFrame.position = new Vector2(-Mathf.Abs(dialogueFrame.localPosition.x), dialogueFrame.localPosition.y);
            dialogueUI.position = new Vector2(-Mathf.Abs(dialogueUI.localPosition.x), dialogueUI.localPosition.y);
        }
        else {
            Debug.Log("Dialogue should be on right side.");
            dialogueFrame.localPosition = new Vector2(Mathf.Abs(dialogueFrame.localPosition.x), dialogueFrame.localPosition.y);
            dialogueUI.localPosition= new Vector2(Mathf.Abs(dialogueUI.localPosition.x), dialogueUI.localPosition.y);
        }
    }
}
