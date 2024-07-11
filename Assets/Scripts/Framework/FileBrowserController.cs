using UnityEngine;
using SimpleFileBrowser;
using System;
using System.Collections;

public class FileBrowserManager : MonoBehaviour
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

		FileBrowser.ShowLoadDialog( ( paths ) => { WhatsAppConverter.ReadFile(@paths[0]); }, () => { OnCancelled(); },
								   FileBrowser.PickMode.Files, false, null, null, "Select Files", "Load" );

	}

    void OnCancelled(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();

        #endif
    }

}