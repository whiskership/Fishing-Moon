using UnityEngine;
using System.Collections;

public class UI_Touchpad_ControllerV2 : MonoBehaviour {
    static UI_Touchpad_ControllerV2 holder;
    void Awake() {
        holder = this;
        touchpad = holder.transform;
    }

    void Update() {
        if (touchPadActive == true)
            TouchpadMovement();
    }

    void TouchpadMovement() {
        transform.position += (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * 2 * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    static bool touchPadActive;
    public static void ActivateTouchPadUI() {
        holder.transform.GetComponent<SpriteRenderer>().enabled = true;
        touchPadActive = true;
    }

    public static void DeactivateTouchPadUI() {
        holder.transform.GetComponent<SpriteRenderer>().enabled = false;
        touchPadActive = false;
    }

    static Transform touchpad;
    public static Transform Touchpad {
        get {
            return touchpad;
        }
    }

    public static bool TouchPadActive {
        get {
            return touchPadActive;
        }
    }
}
