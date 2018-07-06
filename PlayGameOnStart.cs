using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameOnStart : MonoBehaviour {

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
