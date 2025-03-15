using UnityEngine;
using UnityEngine.SceneManagement;

public class manager_lose : MonoBehaviour
{
    public GameObject player;
    public GameObject lose_ui;
    public GameObject ui;
    void Start()
    {
        Time.timeScale = 1;
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (player.GetComponent<PlayerMovements>().loseState == true)
        {
            Time.timeScale = 0;
            ui.SetActive(false);
            lose_ui.SetActive(true);
        }
    }
}
