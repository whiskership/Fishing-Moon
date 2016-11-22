using UnityEngine;
using System.Collections;

public class Talkable_Sprite_Behaviour : MonoBehaviour {

	void Update () {
        if (Talkable_Character.TalkableCharacter != null) {
            GetComponent<SpriteRenderer>().enabled = true;
            transform.position = new Vector2(Talkable_Character.TalkableCharacter.position.x, Talkable_Character.TalkableCharacter.position.y + Talkable_Character.TalkableCharacter.GetComponent<BoxCollider2D>().size.y / 2 + 8);
        }
        else GetComponent<SpriteRenderer>().enabled = false;
	}
}