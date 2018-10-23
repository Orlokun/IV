using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool gamePaused = false;

    public GameObject pauseMenuHUD;
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resumir();
            }
            else
            {
                Pausar();
            }
        }
	}

    public void Resumir()
    {
        pauseMenuHUD.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        GameManager.HideCursor();
    }

    public void Pausar()
    {
        pauseMenuHUD.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        GameManager.ShowCursor();
    }

    public void BotonMenuPrincipal()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        Time.timeScale = 1f;
        gamePaused = false;
    }
}
