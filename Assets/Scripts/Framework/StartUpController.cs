using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUpController : MonoBehaviour
{   
    [SerializeField] AudioSource backgroundMusic;
    GameObject[] sceneGameObjects;
    void Awake(){
        backgroundMusic.Play();

        sceneGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();

        for(int i = 0; i < sceneGameObjects.Length; i++){
            DontDestroyOnLoad(sceneGameObjects[i]);
        }

        SceneManager.LoadScene("MainGame");
    }
}
