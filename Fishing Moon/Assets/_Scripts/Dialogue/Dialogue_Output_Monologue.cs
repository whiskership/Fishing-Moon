using UnityEngine;
using System.Collections;

public class Dialogue_Output_Monologue : Dialogue_Output {
    public static Dialogue_Output_Monologue Instance { get; set; }
    void Awake() {
        if (Instance != null && Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Monologue_Mode.MonologueModeActive == true) {
            RunDialogue();
        }
    }

    public override void RunDialogue(int dialogueNumber = 0) {
        Monologue_Mode.Instance.MonologueMode();
        base.RunDialogue(dialogueNumber);
    }
}