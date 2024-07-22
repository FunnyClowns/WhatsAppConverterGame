using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleFileBrowser;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string SceneToLoad;

    /// <summary>
    /// Called directly by the button in the UI prefab
    /// </summary>
    public void OnClickButton(){
        WhatsAppConverter.ResetData();
        FileBrowser.HideDialog();
        SceneManager.LoadScene(SceneToLoad, LoadSceneMode.Single);
    }
}
