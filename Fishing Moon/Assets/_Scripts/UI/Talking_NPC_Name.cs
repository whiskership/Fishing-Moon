using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Talking_NPC_Name : MonoBehaviour {
    static Talking_NPC_Name holder;
	void OnEnable () {
        holder = this;
        SetName(Dialogue_Output.TalkingNPC.name);
	}
     
    public static void SetName(string name) {
        if (Dialogue_Output.TalkingNPC != null)
            holder.GetComponent<Text>().text = Dialogue_Output.TalkingNPC.name;
        else Debug.LogWarning("There is not a talking NPC.");
    }
}