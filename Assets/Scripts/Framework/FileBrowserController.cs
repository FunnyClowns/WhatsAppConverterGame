using UnityEngine;
using SimpleFileBrowser;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FileBrowserController : MonoBehaviour
{  

	void Start()
	{

		FileBrowser.SetFilters( true, new FileBrowser.Filter( "Text Files", ".txt" ) );

		FileBrowser.SetDefaultFilter( ".txt" );

        FileBrowser.DisplayedEntriesFilter += (entry) => {

            if(entry.IsDirectory){
                return true;
            }
            
            if (entry.Name.StartsWith( "Chat" ) || entry.Name.StartsWith( "WhatsApp")){
                return true;
            }

            return false;
        };

		FileBrowser.SetExcludedExtensions( ".lnk", ".tmp", ".zip", ".rar", ".exe" );

		FileBrowser.AddQuickLink( "Downloads", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads", null );

		FileBrowser.ShowLoadDialog( ( paths ) => { OnSuccess(FileBrowserHelpers.ReadTextFromFile(paths[0])); }, () => { OnCancelled(); },
								   FileBrowser.PickMode.Files, false, null, null, "Select Files", "Load" );

	}

    readonly static List<string> lines = new();
    public static void OnSuccess(string fileContent){
        
        string[] fileLines = fileContent.Split(new string[] { "\n" }, StringSplitOptions.None);
        
        lines.Clear();
        for(int i = 1; i <= 50; i++){
            lines.Add(fileLines[i]);
        }

        WhatsAppConverter.ReadString(lines);
    }

    void OnCancelled(){
        Debug.Log("Cancel");
    }


    /// <summary>
    /// Called directly by the quit button in the UI prefab
    /// </summary>
    public void OnClickChooseFileButton(){
        WhatsAppConverter.ResetData();
        FileBrowser.HideDialog();
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }

}