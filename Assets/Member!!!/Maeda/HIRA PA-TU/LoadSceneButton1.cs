using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton1 : MonoBehaviour
{
    public void Onclick(string name)
    {
        //シーン移行
        SceneManager.LoadScene(name);
    }
}