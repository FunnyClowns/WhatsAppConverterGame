using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class CustomChatManager : MonoBehaviour
{

    [SerializeField] GameObject CharacterNamingPanel;
    [SerializeField] GameObject MessageInputPanel;
    [SerializeField] GameObject EditChatButton;
    [SerializeField] GameObject CancelButton;

    UnityEngine.UI.Image PanelImage;

    [SerializeField] TMP_InputField NameInputField;
    [SerializeField] TextMeshProUGUI CharacterText;
    string[] CharacterNames = new string[2];

    [SerializeField] TMP_Dropdown CharacterDropdown;
    int CharacterDropdownValue;
    
    [SerializeField] TMP_InputField MessageInputField;
    string MessageFieldValue;

    List<string> CharacterMessages = new List<string>();

    void Start(){
        PanelImage = GetComponent<UnityEngine.UI.Image>();
    }

    /// <summary>
    /// Called directly by the button in the UI prefab
    /// </summary>
    public void OnCLickCharacterOKButton(){
        if(NameInputField.text == null | NameInputField.text == ""){
            ErrorHandler.Error("Error: Input field is empty.");
            return;
        }

        if(CharacterNames[0] == null){
            CharacterNames[0] = NameInputField.text[..1].ToUpper() + NameInputField.text[1..];

            CharacterText.text = "Character 2 Name";
            NameInputField.text = "";

            return;
        }

        CharacterNames[1] = NameInputField.text[..1].ToUpper() + NameInputField.text[1..];

        if(CharacterNames[0] == CharacterNames[1]){
            ErrorHandler.Error("Error: Name cant be the same as Character 1");
            return;
        }

        MessageInputPanel.SetActive(true);
        CharacterNamingPanel.SetActive(false);

        StartCharacterDropdown();

        MessageDataManager.SaveNameData(CharacterNames);

        Debug.Log("Char 1 : "  + CharacterNames[0]);
        Debug.Log("Char 2 : "  + CharacterNames[1]);

        
    }

    void StartCharacterDropdown(){
        CharacterDropdown.ClearOptions();
        CharacterDropdown.AddOptions(CharacterNames.ToList<string>());
        CharacterDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Called directly by the button in the UI prefab
    /// </summary>
    public void OnClickMessageOKButton(){

        string data;

        CharacterDropdownValue = CharacterDropdown.value;

        if (CharacterDropdownValue == 0){
            CharacterDropdownValue = 1;
        } else if (CharacterDropdownValue == 1){
            CharacterDropdownValue = 2;
        }

        MessageFieldValue = MessageInputField.text;

        data = CharacterDropdownValue + MessageFieldValue;

        CharacterMessages.Add(data);

        MessageInputField.text = "";
    }

    /// <summary>
    /// Called directly by the button in the UI prefab
    /// </summary>
    public void OnClicKEditChatButton(){
        SetPanelVisible();
        EditChatButton.SetActive(false);
    }

    /// <summary>
    /// Called directly by the button in the UI prefab
    /// </summary>
    public void OnClickFinishButton(){
        MessageDataManager.SaveMessageDataByList(CharacterMessages);

        SetPanelInvisible();
        EditChatButton.SetActive(true);
    }

    /// <summary>
    /// Called directly by the button in the UI prefab
    /// </summary>
    public void OnClickResetButton(){
        MessageDataManager.ResetData();
    }

    /// <summary>
    /// Called directly by the button in the UI prefab
    /// </summary>
    public void OnClickCancelButton(){
        SetPanelInvisible();
    }

    void SetPanelVisible(){
        MessageInputPanel.SetActive(true);
        CancelButton.SetActive(true);

        PanelImage.enabled = true;
    }

    void SetPanelInvisible(){
        for(int i = 0; i < transform.childCount; i++){
            GameObject child = transform.GetChild(i).gameObject;

            child.SetActive(false);
        }

        PanelImage.enabled = false;
    }

}
