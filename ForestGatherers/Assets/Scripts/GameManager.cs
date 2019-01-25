using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager m;

    public Transform uiObj;

    private void Start() {
        m = this;
    }

    internal void ShowUI(string uiName) {
        for (int i = 0; i < uiObj.childCount; i++) {
            uiObj.GetChild(i).gameObject.SetActive(false);
        }
        uiObj.Find(uiName).gameObject.SetActive(true);

    }
}
