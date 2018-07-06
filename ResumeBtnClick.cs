using UnityEngine;

public class ResumeBtnClick : MonoBehaviour {
    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("PauseMenu").SetActive(false);
    }
}
