using UnityEngine;
using System.Collections;

public class Character_Functions : MonoBehaviour {
    [SerializeField] Transform[] pathPoints;
    public void FollowPath(bool reverse = false, Transform[] pathPointss = null, float moveSpeed = 60) {
        if (pathPointss == null && pathPoints != null) {
            pathPointss = pathPoints;
        }
        else Debug.LogWarning("No path bro.");

        StartCoroutine(FollowPathCR(reverse, pathPointss, moveSpeed));
    }

    Animator anim;
    bool followingPath;
    IEnumerator FollowPathCR(bool reverseCR, Transform[] pathPointssCR = null, float moveSpeedCR = 60) {
        int current = 0;
        if (reverseCR == true)
            current = pathPointssCR.Length - 2;

        if (GetComponent<Animator>() != null) {
            anim = GetComponent<Animator>();
            anim.SetBool("isMoving", true);
        }
        else Debug.LogWarning("No animator component.");

        while (transform.position != pathPointssCR[current].position) {
            followingPath = true;

            transform.position = Vector2.MoveTowards(transform.position, pathPointssCR[current].position, moveSpeedCR * Time.deltaTime);
            yield return null;

            if (/*current + 1 != pathPointssCR.Length &&*/ transform.position == pathPointssCR[current].position) {
                if (reverseCR == false && current + 1 != pathPointssCR.Length)
                    current++;
                else if (reverseCR == true && current - 1 != -1)
                    current--;
            }
        }

        if (anim != null)
            anim.SetBool("isMoving", false);

        followingPath = false;
    }

    public bool FollowingPath {
        get {
            return followingPath;
        }
    }
}