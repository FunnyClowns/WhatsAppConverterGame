using UnityEngine;

public class ErrorHandler : MonoBehaviour
{

    public static string errorMessage = null;

    public static void Error(string errorType){
        errorMessage ??= errorType; 
        
    }

}
