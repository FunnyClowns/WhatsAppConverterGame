using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using SimpleFileBrowser;
using UnityEngine.SceneManagement;

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
        WhatsAppConverter.ResetData();
        FileBrowser.HideDialog();

        ErrorHandler.errorMessage = null;
        StartCoroutine(WaitError());
        
        SetPanelInvisible();

        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }

    /// <summary>
    /// Called directly by the quit button in the UI prefab
    /// </summary>
    public void OnClickCancelButton(){
        ErrorHandler.errorMessage = null;
        StartCoroutine(WaitError());
        SetPanelInvisible();
    }


}
