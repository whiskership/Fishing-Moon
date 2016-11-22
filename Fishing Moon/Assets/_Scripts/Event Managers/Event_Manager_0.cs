using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Event_Manager_0 : MonoBehaviour {
    void Start() {
        //Monologue_Mode.Instance.MonologueMode();
        Dialogue_Output_Monologue.Instance.RunDialogue(0);

        StartCoroutine(IntroScene());
    }

    [SerializeField] Transform playerDialogue;
    IEnumerator IntroScene() {
        while (Dialogue_Manager.IsTalking == true)
            yield return null;

        yield return new WaitForEndOfFrame();

        playerDialogue.GetComponent<Dialogue_Output>().RunDialogue();

        while (Dialogue_Manager.IsTalking == true)
            yield return null;

        Dialogue_Output_Monologue.Instance.RunDialogue(1);

        while (Dialogue_Manager.IsTalking == true)
            yield return null;

        yield return new WaitForEndOfFrame();

        playerDialogue.GetComponent<Dialogue_Output>().RunDialogue(4);
    }
}