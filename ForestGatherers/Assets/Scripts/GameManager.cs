using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager m;

    public Transform uiObj;
    public Transform player;
    public Transform trapPref;
    public Transform bulletPref;
    public Transform cam;

    private void Start() {
        m = this;
    }

    internal void ShowUI(string uiName) {
        for (int i = 0; i < uiObj.childCount; i++) {
            uiObj.GetChild(i).gameObject.SetActive(false);
        }
        uiObj.Find(uiName).gameObject.SetActive(true);

    }

    public void OpenGameplayScene() {
        SceneManager.LoadScene("MapEditingScene UseTHIS");
    }

    public void OpenMainMenuScene() {
        SceneManager.LoadScene("MainMenu");
    }

    public static void OpenWinScene() {
        SceneManager.LoadScene("ShowWin");
    }
    public static void OpenLoseScene() {
        SceneManager.LoadScene("ShowLose");
    }
}
