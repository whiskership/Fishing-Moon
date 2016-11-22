using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character_Potrait : MonoBehaviour {
    [SerializeField] Sprite potraitImage;
    public Sprite PotraitImage {
        get {
            return potraitImage;
        }
    }
}