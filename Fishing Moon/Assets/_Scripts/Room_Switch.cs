using UnityEngine;
using System.Collections;

public class Room_Switch : MonoBehaviour {
    [SerializeField] Transform firstRoom;
    Grid grid;
    void Start() {
        if (firstRoom != null)
            activeRoom = firstRoom;

        grid = FindObjectOfType(typeof(Grid)) as Grid;
    }

    static Transform activeRoom;
    [SerializeField] Transform[] rooms;
    int currentRoom;
    void OnTriggerExit2D(Collider2D coll) {
        if (coll.transform == Player_Info.Player) {
            currentRoom = rooms[0] == activeRoom ? currentRoom = 1 : currentRoom = 0;

            activeRoom = rooms[currentRoom];

            /*Material offRoomMat = currentRoom == 0 ? rooms[1].GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material : rooms[0].GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material;
            offRoomMat.color = new Color(offRoomMat.color.r, offRoomMat.color.g, offRoomMat.color.b, 0);

            Material activeRoomMat = activeRoom.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material;
            activeRoomMat.color = new Color(offRoomMat.color.r, offRoomMat.color.g, offRoomMat.color.b, 1);*/

            if (currentRoom == 0) {
                rooms[1].gameObject.SetActive(false);
            }
            else if (currentRoom == 1) {
                rooms[0].gameObject.SetActive(false);
            }

            activeRoom.gameObject.SetActive(true);

            grid.RefreshGrid();
        }
    }

    public static Transform ActiveRoom {
        get {
            return activeRoom;
        }
    }
}