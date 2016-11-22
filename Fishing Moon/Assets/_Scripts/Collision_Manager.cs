using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Collision_Manager : MonoBehaviour {
	void Update () {
        DisableCollisionRenderer(8);
	}

    void DisableCollisionRenderer(int layer) {
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];

        for (int i = 0; i < goArray.Length; i++) {
            if (goArray[i].layer == layer && goArray[i].GetComponent<PolygonCollider2D>() != null /*goArray[i].transform.parent.GetChild(0).GetComponent<MeshRenderer>() != null*/) {
                goArray[i].transform.parent.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}