using UnityEngine;
using SimpleFileBrowser;

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

		// Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
		// It is sufficient to add a quick link just once
		// Name: Users
		// Path: C:\Users
		// Icon: default (folder icon)
		FileBrowser.AddQuickLink( "Users", "C:\\Users", null );
		

		FileBrowser.ShowLoadDialog( ( paths ) => { OnSelectedFiles(paths); }, () => { OnCancelled(); },
								   FileBrowser.PickMode.Files, false, null, null, "Select Files", "Load" );

	}

	void OnSelectedFiles( string[] filePaths )
	{
		for( int i = 0; i < filePaths.Length; i++ )
			Debug.Log( filePaths[i] );

		string filePath = filePaths[0];
        
        WhatsAppConverter.ReadFile(@filePath);
	}

    void OnCancelled(){
        Debug.Log("Canceled");

        FileBrowser.ClearQuickLinks();
    }
}