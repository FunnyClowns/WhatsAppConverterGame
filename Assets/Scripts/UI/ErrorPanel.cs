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
    
    enum ButtonClickedType{
        ResetScene,
        ClosePanel
    }

    [SerializeField] ButtonClickedType OKButtonClickedType;

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
    /// Called directly by the button in the UI prefab
    /// </summary>
    public void OnClickOKButton(){
        MessageDataManager.ResetData();
        FileBrowser.HideDialog();

        ErrorHandler.errorMessage = null;
        StartCoroutine(WaitError());

        if (OKButtonClickedType == ButtonClickedType.ResetScene){
            SceneManager.LoadScene("ChatFileBasedGame", LoadSceneMode.Single);
        } else {
            SetPanelInvisible();
        }

    }

    /// <summary>
    /// Called directly by the button in the UI prefab
    /// </summary>
    public void OnClickCancelButton(){
        ErrorHandler.errorMessage = null;
        StartCoroutine(WaitError());
        SetPanelInvisible();
    }


}
