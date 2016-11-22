using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Potrait_Behaviour : MonoBehaviour {
    static UI_Potrait_Behaviour holder;
    void OnEnable() {
        holder = this;

        if (Dialogue_Output.TalkingNPC.GetComponent<Character_Potrait>() != null)
            DisplayPotrait(Dialogue_Output.TalkingNPC.GetComponent<Character_Potrait>().PotraitImage);
    }

    public static void DisplayPotrait(Sprite potraitImage) {
        holder.GetComponent<Image>().sprite = potraitImage;
    }
}