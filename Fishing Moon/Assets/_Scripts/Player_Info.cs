using UnityEngine;
using System.Collections;

public class Player_Info : MonoBehaviour {
    static Player_Info holder;
    void Awake() {
        holder = this;
    }

    public static Transform Player {
        get {
            if (holder != null)
                return holder.transform;
            else return null;
        }
    }

    static string playerName;
    public static string PlayerName {
        get {
            return playerName;
        }
        set {
            playerName = value;
        }
    }
}
