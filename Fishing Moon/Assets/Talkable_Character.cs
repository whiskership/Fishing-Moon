using UnityEngine;
using System.Collections;

public class Talkable_Character : MonoBehaviour {
    static Transform talkableCharacter;
    bool playerInRange;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform == Player_Info.Player) {
            playerInRange = true;

            talkableCharacter = transform.parent;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.transform == Player_Info.Player) {
            playerInRange = false;

            talkableCharacter = null;
        }
    }

    public bool PlayerInRange {
        get {
            return playerInRange;
        }
    }

    public static Transform TalkableCharacter {
        get {
            return talkableCharacter;
        }
    }
}