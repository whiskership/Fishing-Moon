using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue_Choice_Settings : MonoBehaviour {
    static Dialogue_Choice_Settings holder;
    void Awake() {
        holder = this;
    }

    static int choosing;
    static int chosen;
    static bool axisPressed;
    void Update () {
        if (UI_Choice_Menu.ChoiceBoxActive == true) {
            if (axisPressed == false) {
                choosing = Mathf.Clamp(choosing - (int)AxisVector.y, 0, ActiveChildCount -1);
                choosing = Mathf.Clamp(choosing + (int)AxisVector.x, 0, ActiveChildCount -1);

                if (AxisVector != Vector2.zero) {
                    for (int i = 0; i < holder.transform.childCount; i++) {
                        if (i == choosing)
                            continue;

                        holder.transform.GetChild(i).GetComponent<Text>().color = Color.white;
                    }
                }

                holder.transform.GetChild(choosing).GetComponent<Text>().color = Color.red;
            }

            if (Input.GetKeyDown(KeyCode.Z)) {
                chosen = choosing;
            }

            if (AxisVector != Vector2.zero) {
                axisPressed = true;
            }
            else {
                axisPressed = false;
            }
        }
    }

    static Vector2 AxisVector {
        get {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }

    static int ActiveChildCount {
       get {
            int count = 0;
            for (int i = 0; i < holder.transform.childCount; i++) {
                if (holder.transform.GetChild(i).gameObject.activeSelf == true)
                    count++;
            }
            return count;
        }
    }

    public static int Chosen {
        get {
            return chosen;
        }
    }
}