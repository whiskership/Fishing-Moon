using UnityEngine;
using System.Collections;

public class UI_Potrait_And_Name : MonoBehaviour {
    public static UI_Potrait_And_Name Instance { get; set; }
    void Awake() {
        if (Instance != null && Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void DisableUI() {
        transform.parent.GetChild(1).gameObject.SetActive(false);
    }

    public void EnableUI() {
        transform.parent.GetChild(1).gameObject.SetActive(true);
    }
}
