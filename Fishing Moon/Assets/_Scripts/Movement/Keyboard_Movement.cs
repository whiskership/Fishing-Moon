using UnityEngine;
using System.Collections;

public class Keyboard_Movement : MonoBehaviour {
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
        if (transform.position.x % 16 == 0 && transform.position.y % 16 == 0)
            targetPos = (Vector2)transform.position + AxisVector * 16;

        if (Dialogue_Manager.IsTalking == false && (Vector2)transform.position != targetPos 
            && Physics2D.OverlapCircle(targetPos, 4, mask) == false && (AxisVector.x == 0 || AxisVector.y == 0)) {
            isMoving = true;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, step * Time.deltaTime);
        }
        else isMoving = false;

        anim.SetBool("isMoving", AxisVector != Vector2.zero);
	}

    public bool IsMoving {
        get {
            return isMoving;
        }
    }

    public static Vector2 AxisVector {
        get {
            return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}