using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LodeScene : MonoBehaviour
{
    [SerializeField] string _sceneName = "";
    
   public void SceneLode()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
