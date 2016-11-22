using UnityEngine;
using System.Collections;

public class Dialogue_Output : MonoBehaviour {
    [SerializeField] TextAsset[] dialogues;
    float talkingDistance;
	void LateUpdate () {
        /*if (Player_Info.Player != null) {
            if (Vector2.Distance(transform.position, Player_Info.Player.position) == talkingDistance) {
                talkableCharacter = transform;
            }

            if (talkableCharacter == transform && (Player_Info.Player.GetComponent<TouchPad_MovementV2>().IsMoving == true || Pathfinding_Cursor.PathRunning == true))
                talkableCharacter = null;

            if (talkableCharacter == transform
            && Vector2.Distance(transform.position, Player_Info.Player.position) != talkingDistance
            && (Player_Info.Player.GetComponent<TouchPad_MovementV2>().IsMoving == true || Pathfinding_Cursor.PathRunning == true)) {
                talkableCharacter = null;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0) && Monologue_Mode.MonologueModeActive == false && (Pathfinding_Cursor.Cursor.position == transform.position
                || (Dialogue_Manager.IsTalking == true && TalkingNPC == transform))) {
                Debug.Log(talkingNPC);

                StopAllCoroutines();
                StartCoroutine(WaitToRunDialogue());
            }
        }*/

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            StopAllCoroutines();
            StartCoroutine(WaitToRunDialogue());
        } 
    }

    bool cursorOnTransform;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform == Pathfinding_Cursor.Cursor) {
            cursorOnTransform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform == Pathfinding_Cursor.Cursor) {
            cursorOnTransform = false;
        }
    }

    IEnumerator WaitToRunDialogue() {
        if (Pathfinding_Cursor.PathRunning == true) {
            while (Pathfinding_Cursor.PathRunning == true)
                yield return null;

            yield return new WaitForEndOfFrame();

            if (cursorOnTransform == true && Talkable_Character.TalkableCharacter == transform)
                RunDialogue(currentDialogue);
        }
        else if (Dialogue_Manager.IsTalking == true && talkingNPC == transform)
            RunDialogue(currentDialogue);
    }

    public virtual void RunDialogue(int dialogueToRun = 0) {
        talkingNPC = transform;
        Dialogue_Manager.InputDialogue(dialogues[dialogueToRun]);
    }

    int currentDialogue;
    public int CurrentDialogue {
        get {
            return currentDialogue;
        }
        set {
            currentDialogue = value;
        }
    }

    static Transform talkingNPC;
    public static Transform TalkingNPC {
        get {
            return talkingNPC;
        }
        set {
            talkingNPC = value;
        }
    }
}