using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Full_Screen_Cover_Image_Effects : MonoBehaviour {
    static Full_Screen_Cover_Image_Effects holder;
    void Awake() {
        holder = this;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)){
           FadeIn(3);
        }
    }

    public static void FadeOut(float fadeSpeed) {
        holder.StartCoroutine(holder.FadeOutCR(fadeSpeed));
    }

    IEnumerator FadeOutCR(float fadeSpeedCR) {
        while (holder.GetComponent<Image>().color.a > 0) {
            Image image = holder.GetComponent<Image>();
            Text imageChild = holder.transform.GetChild(0).GetComponent<Text>();

            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - fadeSpeedCR * Time.deltaTime);
            imageChild.color = new Color(imageChild.color.r, imageChild.color.g, imageChild.color.b, imageChild.color.a - fadeSpeedCR * Time.deltaTime);

            yield return null;
        }
    }

    public static void FadeIn(float fadeSpeed) {
        holder.StartCoroutine(holder.FadeInCR(fadeSpeed));
    }

    IEnumerator FadeInCR(float fadeSpeedCR) {
        while (holder.GetComponent<Image>().color.a < 1) {
            Image image = holder.GetComponent<Image>();
            Text imageChild = holder.transform.GetChild(0).GetComponent<Text>();

            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + fadeSpeedCR * Time.deltaTime);
            imageChild.color = new Color(imageChild.color.r, imageChild.color.g, imageChild.color.b, imageChild.color.a + fadeSpeedCR * Time.deltaTime);

            yield return null;
        }
    }

    public static string FullScreenText {
        set {
            holder.transform.GetChild(0).GetComponent<Text>().text = value;
        }
    }
}