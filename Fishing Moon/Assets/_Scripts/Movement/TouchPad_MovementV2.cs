using UnityEngine;
using System.Collections;

public class TouchPad_MovementV2 : MonoBehaviour {
    Animator anim;
    void Start() {
        anim = GetComponent<Animator>();
        targetPos = transform.position;
    }

    [SerializeField] LayerMask unwalkableMask;
    Vector2 targetPos;
    bool isMoving;
    float step = 70;
    void LateUpdate() {
        //COULD USE OPTIMIZATION
        if (UI_Touchpad_ControllerV2.TouchPadActive == true) {
            if (transform.position.x % 16 == 0 && transform.position.y % 16 == 0) {
                targetPos = (Vector2)transform.position + TouchAxisVector * 16;
            }
        }
        else if (Pathfinding_Cursor.PathRunning == true) {
            targetPos = transform.position;
        }

        if (Dialogue_Manager.IsTalking == false && (Vector2)transform.position != targetPos
            && Physics2D.OverlapCircle(targetPos, 4, unwalkableMask) == false && (TouchAxisVector.x == 0 || TouchAxisVector.y == 0)
            && Pathfinding_Cursor.PathRunning == false) {
            isMoving = true;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, step * Time.deltaTime);
        }
        else isMoving = false;

        anim.SetBool("isMoving", TouchAxisVector != Vector2.zero);
    }

    Vector2 originalPosition;
    Vector2 currentPosition;
    float deadDist = 3;
    Vector2 TouchAxisVector {
        get {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                UI_Touchpad_ControllerV2.Touchpad.position = mousePos;
                originalPosition = mousePos;
            }

            if (Input.GetKey(KeyCode.Mouse0)) {
                originalPosition = UI_Touchpad_ControllerV2.Touchpad.position;
                currentPosition = mousePos;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0)) {
                UI_Touchpad_ControllerV2.DeactivateTouchPadUI();

                originalPosition = Vector2.zero;
                currentPosition = Vector2.zero;
            }   

            if (Vector2.Distance(originalPosition, currentPosition) > deadDist) {
                UI_Touchpad_ControllerV2.ActivateTouchPadUI();

                Vector2 mouseVector = currentPosition - originalPosition;

                if (Mathf.Abs(mouseVector.x) > Mathf.Abs(mouseVector.y))
                    mouseVector = new Vector2(Mathf.RoundToInt(mouseVector.normalized.x), 0);
                else if (Mathf.Abs(mouseVector.x) < Mathf.Abs(mouseVector.y))
                    mouseVector = new Vector2(0, Mathf.RoundToInt(mouseVector.normalized.y));

                //touchpad animation
                Animator touchAnim = UI_Touchpad_ControllerV2.Touchpad.GetComponent<Animator>();
                touchAnim.SetFloat("x_axis", mouseVector.x);
                touchAnim.SetFloat("y_axis", mouseVector.y);

                return mouseVector;
            }
            else return Vector2.zero;
        }
    }

    public bool IsMoving {
        get {
            return isMoving;
        }
    }
}
