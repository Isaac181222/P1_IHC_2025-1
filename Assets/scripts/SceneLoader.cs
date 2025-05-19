using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadARScene()
    {
        SceneManager.LoadScene("TestArscene"); // Asegúrate de que el nombre coincida con tu escena en Build Settings
    }
}
