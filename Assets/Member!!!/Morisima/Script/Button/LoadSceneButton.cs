using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public void Onclick(string name)
    {
        //�V�[���ڍs
        SceneManager.LoadScene(name);
    }
}