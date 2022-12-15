using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton1 : MonoBehaviour
{
    public void Onclick(string name)
    {
        //ƒV[ƒ“ˆÚs
        SceneManager.LoadScene(name);
    }
}