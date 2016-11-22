using UnityEngine;
using System.Collections;

public class UI_Controller : MonoBehaviour {
    static UI_Controller holder;
    void Awake() {
        holder = this;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            //GetComponent<SpriteRenderer>().enabled = true;
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
        else if (Input.GetKey(KeyCode.Mouse0) && GetComponent<SpriteRenderer>().enabled == true) {
            transform.position += (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * 2 * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
        /*if (Input.GetKeyDown(KeyCode.Mouse0)) {
            StopAllCoroutines();
            StartCoroutine(DelayTouch());
        }*/
        else if (Input.GetKeyUp(KeyCode.Mouse0)) {
            GetComponent<SpriteRenderer>().enabled = false;
            touchPadActive = false;
        }
    }

    static bool touchPadActive;
    public static void DisplayTouchPad() {
        touchPadActive = true;
        holder.transform.GetComponent<SpriteRenderer>().enabled = true;
    }

    /*IEnumerator DelayTouch() {
        while (Input.GetKey(KeyCode.Mouse0) && GetComponent<SpriteRenderer>().enabled == false) {
            yield return new WaitForSeconds(0.5f);

            if (!Input.GetKey(KeyCode.Mouse0))
                break;

            GetComponent<SpriteRenderer>().enabled = true;
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }

        while (Input.GetKey(KeyCode.Mouse0)) {
            transform.position += (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized * 2 * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            yield return null;
        }
    }*/

    static Transform touchController;
    public static Transform TouchController {
        get {
            touchController = holder.transform;
            return touchController;
        }
    }

    public static bool TouchPadActive {
        get {
            return touchPadActive;
        }
    }
}