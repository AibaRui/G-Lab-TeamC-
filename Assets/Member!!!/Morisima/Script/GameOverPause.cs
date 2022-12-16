using UnityEngine.SceneManagement;
using UnityEngine;

using static PauseManager;
using UnityEngine.UI;

public class GameOverPause : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseUI;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (Mathf.Approximately(Time.timeScale, 1f))
            {
                
                Time.timeScale = 0f;
                pauseUI.SetActive(true);

            }
            else
            {
                Time.timeScale = 1f;
                
            }
        }
    }
    public void PauseButton()
    {
        Time.timeScale = 1f;
        pauseUI.SetActive(false);
    }
}
