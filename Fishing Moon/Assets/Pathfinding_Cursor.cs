using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding_Cursor : MonoBehaviour {
    static Pathfinding_Cursor holder;

    Pathfinding path;
    Grid grid;
    void Start() {
        path = FindObjectOfType(typeof(Pathfinding)) as Pathfinding;
        grid = FindObjectOfType(typeof(Grid)) as Grid;

        holder = this;
        cursor = holder.transform;
    }

    [SerializeField]
    LayerMask unwalkable;
    static bool canMove = true;
    void Update() {
        if (Input.GetKeyUp(KeyCode.Mouse0) && UI_Touchpad_ControllerV2.TouchPadActive == false && canMove == true) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(Mathf.Round(mousePos.x / 16) * 16, Mathf.Round(mousePos.y / 16) * 16);

            if (Physics2D.OverlapCircle(transform.position, 4, unwalkable) == true) {
                FollowPath(seeker.position, FindClosestWalkableTile());
            }
            else
                FollowPath(seeker.position, transform.position);
        }
    }

    Vector2 FindClosestWalkableTile() {
        List<Node> walkableNodes = new List<Node>();
        List<float> nodeDistance = new List<float>();

        for (int y = -1; y <= 1; y++) {
            for (int x = -1; x <= 1; x++) {
                if (x != 0 && y != 0) {
                    continue;
                }

                Vector2 nodePosition = (Vector2)transform.position + new Vector2(x * 16, y * 16);
                if (grid.NodeFromWorldPoint(nodePosition).walkable == true) {
                    walkableNodes.Add(grid.NodeFromWorldPoint(nodePosition));
                    nodeDistance.Add(Vector2.Distance(nodePosition, Player_Info.Player.position));
                }
            }
        }

        Vector2 lowestFoundPosition = Vector2.zero;
        float lowestDistance = 160;

        for (int i = 0; i < walkableNodes.Count; i++) {
            if (nodeDistance[i] < lowestDistance) {
                lowestDistance = nodeDistance[i];
                lowestFoundPosition = walkableNodes[i].worldPosition;
            }
        }

        if (lowestFoundPosition == Vector2.zero) {
            lowestFoundPosition = Player_Info.Player.position;
        }

        return lowestFoundPosition;
    }

    [SerializeField]
    Transform seeker;
    void FollowPath(Vector2 startPos, Vector2 targetPos) {
        StopAllCoroutines();
        StartCoroutine(FollowPathCR(startPos, targetPos));
    }

    static bool pathRunning;
    IEnumerator FollowPathCR(Vector2 _startPos, Vector2 _targetPos) {
        Vector2[] pathToFollow = path.FindPath(seeker.position, _targetPos);
        int current = 0;

        pathRunning = true;
        ShowCursor();

        while (pathToFollow != null && pathToFollow.Length != 0 && (Vector2)seeker.position != pathToFollow[pathToFollow.Length - 1]) {
            seeker.position = Vector2.MoveTowards(seeker.position, pathToFollow[current], 120 * Time.deltaTime);

            yield return null;

            if ((Vector2)seeker.position == pathToFollow[current])
                current++;
        }

        HideCursor();
        pathRunning = false;
    }
    
    public static void ShowCursor() {
        holder.GetComponent<SpriteRenderer>().enabled = true;
    }

    public static void HideCursor() {
        holder.GetComponent<SpriteRenderer>().enabled = false;
    }

    public static bool PathRunning {
        get {
            return pathRunning;
        }
    }

    static Transform cursor;
    public static Transform Cursor {
        get {
            return cursor;
        }
    }

    public static bool CanMove {
        set {
            canMove = value;
        }
    }
}