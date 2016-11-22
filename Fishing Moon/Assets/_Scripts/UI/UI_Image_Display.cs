using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Image_Display : MonoBehaviour {
    static Transform displayTransform;
    void Awake() {
        displayTransform = transform;
    }

    public static Transform DisplayTransform {
        get {
            return displayTransform;
        }
    }

    public static Sprite DisplayImage {
        set {
            displayTransform.GetComponent<Image>().enabled = true;
            displayTransform.GetComponent<Image>().sprite = value;
        }
    }
}