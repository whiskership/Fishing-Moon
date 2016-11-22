using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Choice_Menu : MonoBehaviour {
    static UI_Choice_Menu holder;
    void Awake() {
        holder = this;
        CloseChoiceBoxes();
        //OpenChoiceBoxes();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q))
            AddChoiceOption("sample");
    }

    static bool choiceBoxActive;
    public static void OpenChoiceBoxes() {
        //holder.GetComponent<Image>().enabled = true;
        for (int i = 0; i < holder.transform.childCount; i++) {
            holder.transform.GetChild(i).GetComponent<Image>().enabled = true;
            holder.transform.GetChild(i).GetChild(0).GetComponent<Text>().enabled = true;
        }
        choiceBoxActive = true;
    }

    public static void CloseChoiceBoxes() {
        //holder.GetComponent<Image>().enabled = false;
        for (int i = 0; i < holder.transform.childCount; i++) {
            holder.transform.GetChild(i).GetComponent<Image>().enabled = false;
            holder.transform.GetChild(i).GetChild(0).GetComponent<Text>().enabled = false;
        }

        ResetChoiceOptions();
        choiceBoxActive = false;
    }

    static int currentChild;
    public static void AddChoiceOption(string optionString) {
        if (currentChild + 1 <= holder.transform.childCount) {
            holder.transform.GetChild(currentChild).gameObject.SetActive(true);
            holder.transform.GetComponent<RectTransform>().localPosition = new Vector2(holder.transform.GetComponent<RectTransform>().localPosition.x, holder.transform.GetComponent<RectTransform>().localPosition.y - 63);
            holder.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(holder.transform.GetComponent<RectTransform>().sizeDelta.x, holder.transform.GetComponent<RectTransform>().sizeDelta.y + 126);

            holder.transform.GetChild(currentChild).GetChild(0).GetComponent<Text>().text = optionString;
        }
        else Debug.LogWarning("Max choice count.");

        currentChild++;
    }

    public static void ResetChoiceOptions() {
        currentChild = 0;
        for (int i = 0; i < holder.transform.childCount; i++) {
            holder.transform.GetChild(i).gameObject.SetActive(false);
            holder.transform.GetComponent<RectTransform>().localPosition = new Vector2(-3.5f, 1295.5f);
            holder.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(774, 0);
        }
    }

    public static bool ChoiceBoxActive {
        get {
            return choiceBoxActive;
        }
    }
}