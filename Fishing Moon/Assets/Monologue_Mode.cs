using UnityEngine;
using System.Collections;

public class Monologue_Mode : MonoBehaviour {
    public static Monologue_Mode Instance { get; set; }
    void Awake() {
        if (Instance != null && Instance != null) Destroy(gameObject);
        else Instance = this;

        iTween.CameraFadeAdd();
    }

    [SerializeField] Transform dialogueFrame;
    static bool monologueModeActive = false;
    public void MonologueMode() {
        monologueModeActive = true;
        iTween.CameraFadeTo(1, 0.5f);

        dialogueFrame.GetComponent<RectTransform>().localPosition = Vector2.zero;
        UI_Potrait_And_Name.Instance.DisableUI();

        StartCoroutine(CloseWhenDone());
    }

    public void EndMonologueMode() {
        monologueModeActive = false;

        iTween.CameraFadeTo(0, 0.5f);

        dialogueFrame.GetComponent<RectTransform>().localPosition = new Vector2(0, -590);
        //UI_Potrait_And_Name.Instance.EnableUI();
    }

    IEnumerator CloseWhenDone() {
        yield return new WaitForEndOfFrame();

        while (Dialogue_Manager.IsTalking == true) {
            yield return null;
        }

        EndMonologueMode();
    }

    public static bool MonologueModeActive {
        get {
            return monologueModeActive;
        }
    }
}