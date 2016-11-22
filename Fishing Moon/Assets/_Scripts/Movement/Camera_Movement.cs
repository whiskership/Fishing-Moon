using UnityEngine;
using System.Collections;

public class Camera_Movement : MonoBehaviour {
    void Start() {
        
    }

	void Update () {
        FollowTarget(targetToFollow);
        //FollowTargetCenter();
        //FollowTargetItween();
	}

    [SerializeField] Transform targetToFollow;
    void FollowTarget(Transform target) {
        float mapLength = Room_Switch.ActiveRoom.GetComponent<Tiled2Unity.TiledMap>().MapWidthInPixels;
        float mapHeight = Room_Switch.ActiveRoom.GetComponent<Tiled2Unity.TiledMap>().MapHeightInPixels;

        if (mapLength > mapHeight)
            transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(target.position.x, Room_Switch.ActiveRoom.position.x + mapLength / 7, Room_Switch.ActiveRoom.position.x + mapLength - mapLength / 7), Room_Switch.ActiveRoom.position.y - mapHeight / 2, -10), 0.1f);
        else
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, Mathf.Clamp(target.position.y, Room_Switch.ActiveRoom.position.y - mapHeight + (mapHeight / 4), Room_Switch.ActiveRoom.position.y - mapHeight /4), -10), 0.1f);

        //transform.position = new Vector3(Mathf.Clamp(target.position.x, 8, 264), transform.position.y, -10);
    }

    void FollowTargetCenter() {
        transform.position = Vector3.Lerp(transform.position, new Vector3(targetToFollow.position.x, targetToFollow.position.y, -10), 0.2f);
    }

    void FollowTargetItween() {
        float mapLength = Room_Switch.ActiveRoom.GetComponent<Tiled2Unity.TiledMap>().MapWidthInPixels;
        float mapHeight = Room_Switch.ActiveRoom.GetComponent<Tiled2Unity.TiledMap>().MapHeightInPixels;

        iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(Mathf.Clamp(targetToFollow.position.x, Room_Switch.ActiveRoom.position.x + mapLength / 7, Room_Switch.ActiveRoom.position.x + mapLength - mapLength / 7), Room_Switch.ActiveRoom.position.y - mapHeight / 2, -10), "easetype", iTween.EaseType.spring));
    }
}