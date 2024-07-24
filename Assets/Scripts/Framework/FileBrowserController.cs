using UnityEngine;
using SimpleFileBrowser;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FileBrowserController : MonoBehaviour
{  

    [SerializeField] UISkin FileBrowserSkin;

	void Start(){
        FileBrowser.HideDialog();
        CreateFileBrowser();
	}

    void CreateFileBrowser(){
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

        FileBrowser.Skin = FileBrowserSkin;

		FileBrowser.ShowLoadDialog( ( paths ) => { OnSuccess(FileBrowserHelpers.ReadTextFromFile(paths[0])); }, () => { OnCancelled(); },
								   FileBrowser.PickMode.Files, false, null, null, "Select Files", "Load" );

    }

    readonly List<string> lines = new();
    public void OnSuccess(string fileContent){
        
        string[] fileLines = fileContent.Split(new string[] { "\n" }, StringSplitOptions.None);
        
        lines.Clear();
        for(int i = 1; i <= 50; i++){
            lines.Add(fileLines[i]);
        }

        MessageDataManager.ReadFileString(lines);
    }

    void OnCancelled(){
        Debug.Log("Cancel");
    }

}