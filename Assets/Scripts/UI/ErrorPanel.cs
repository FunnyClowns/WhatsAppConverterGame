using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SimpleFileBrowser;

public class ErrorPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI errorText;
    [SerializeField] UnityEngine.UI.Image panelImage;

    void Awake(){
        StartCoroutine(WaitError());

        panelImage.enabled = false;
    }

    IEnumerator WaitError(){
        while(ErrorHandler.errorMessage == null){
            yield return null;
        }

        SetPanelVisible();
    }

    void SetPanelVisible(){

        for(int i = 0; i < transform.childCount; i++){
            Transform child = transform.GetChild(i);

            child.gameObject.SetActive(true);
        }

        panelImage.enabled = true;
        errorText.text = ErrorHandler.errorMessage;
    }

    void SetPanelInvisible(){
        for(int i = 0; i < transform.childCount; i++){
            Transform child = transform.GetChild(i);

            child.gameObject.SetActive(false);
        }

        panelImage.enabled = false;
        errorText.text = "";
    }

    /// <summary>
    /// Called directly by the quit button in the UI prefab
    /// </summary>
    public void OnClickOKButton(){
        FileBrowser.ShowLoadDialog( ( paths ) => { WhatsAppConverter.ReadFile(@paths[0]); }, () => { OnClickExitButton(); },
								   FileBrowser.PickMode.Files, false, null, null, "Select Files", "Load" );

        ErrorHandler.errorMessage = null;
        StartCoroutine(WaitError());
        
        SetPanelInvisible();
    }

    /// <summary>
    /// Called directly by the quit button in the UI prefab
    /// </summary>
    public void OnClickExitButton(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


}
