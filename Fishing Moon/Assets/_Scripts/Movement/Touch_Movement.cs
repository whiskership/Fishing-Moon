using UnityEngine;
using System.Collections;

public class Touch_Movement : MonoBehaviour {
    Animator anim;
    void Start() {
        targetPos = transform.position;
        anim = GetComponent<Animator>();
    }

    Vector2 targetPos;
    bool isMoving;
    [SerializeField] float step = 70;
    [SerializeField] LayerMask mask;
    void Update() {
        if (UI_Controller.TouchPadActive == false && transform.position.x % 16 == 0 && transform.position.y % 16 == 0) {
            targetPos = transform.position;
        }

        if (UI_Controller.TouchPadActive == true) {
            if (transform.position.x % 16 == 0 && transform.position.y % 16 == 0)
                targetPos = (Vector2)transform.position + TouchAxisVector * 16;

            if (Dialogue_Manager.IsTalking == false && (Vector2)transform.position != targetPos
                && Physics2D.OverlapCircle(targetPos, 4, mask) == false && (TouchAxisVector.x == 0 || TouchAxisVector.y == 0)
                && Pathfinding_Cursor.PathRunning == false) {
                isMoving = true;
                transform.position = Vector2.MoveTowards(transform.position, targetPos, step * Time.deltaTime);
            }
            else isMoving = false;
        }

        anim.SetBool("isMoving", TouchAxisVector != Vector2.zero);
    }

    public bool IsMoving {
        get {
            return isMoving;
        }
    }

    Vector2 originalPosition;
    Vector2 currentPosition;
    float deadDist = 3f;
    Vector2 TouchAxisVector {
        get {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetKey(KeyCode.Mouse0)) {
                originalPosition = UI_Controller.TouchController.position;
                currentPosition = mousePos;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0)) {
                originalPosition = Vector2.zero;
                currentPosition = Vector2.zero;
            }

            if (Vector2.Distance(originalPosition, currentPosition) > deadDist /*&& UI_Controller.TouchController.GetComponent<SpriteRenderer>().enabled == true*/) {
                UI_Controller.DisplayTouchPad();

                Vector2 mouseVector = currentPosition - originalPosition;

                if (Mathf.Abs(mouseVector.x) > Mathf.Abs(mouseVector.y))
                    mouseVector = new Vector2(Mathf.RoundToInt(mouseVector.normalized.x), 0);
                else if (Mathf.Abs(mouseVector.x) < Mathf.Abs(mouseVector.y))
                    mouseVector = new Vector2(0, Mathf.RoundToInt(mouseVector.normalized.y));

                //touch ui controller animation
                Animator touchAnim = UI_Controller.TouchController.GetComponent<Animator>();
                touchAnim.SetFloat("x_axis", mouseVector.x);
                touchAnim.SetFloat("y_axis", mouseVector.y);

                return mouseVector;

            }
            else return Vector2.zero;
        }
    }
}