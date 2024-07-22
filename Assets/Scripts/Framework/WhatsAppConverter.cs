using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class WhatsAppConverter : MonoBehaviour
{

    static public List<string> characterDatas = new List<string>();
    static public string[] characterNames{ get; private set; } = new string[2];
    static bool isDataReady;


    // Regex patterns
    static string timelinePattern = @"^\d{2}/\d{2}/\d{2}(,)? \d{1,2}(?::|\.)\d{2}\s*(?:am|pm)? - ";
    static string namePattern = @"^[a-zA-Z]+(?: [a-zA-Z]+)?: ";
    static string mediaMessage = @"^\<Media omitted\>$|^\<Media tidak disertakan\>$";
    static string editedMessage = @"\<This message was edited\>|\<Pesan ini diedit\>";
    static string deletedMessage = @"^This message was deleted$|^You deleted this message$|^Pesan ini dihapus$|^Anda menghapus pesan ini$";
    static string fileMessage = @"(file attached)|(file terlampir)";

    public static void ReadFileString(List<string> lines){

        try{

            foreach(string line in lines){

                string lineCleared = Regex.Replace(line, timelinePattern, "");

                Match lineContents = Regex.Match(line, timelinePattern);
                Match nameContents = Regex.Match(lineCleared, namePattern);

                string name = nameContents.Groups[0].Value;
                string message = lineCleared.Substring(nameContents.Length);
                        
                if (lineContents.Success){
                    if (nameContents.Success){

                        name = FilterName(name);

                        if (characterNames[0] == null){
                            characterNames[0] = name;
                        } else if (characterNames[0] != name && (characterNames[1] == null)){
                            characterNames[1] = name;
                        }

                        message = FilterMessage(message);

                        if(message != null){

                            SaveMessageDataByLine(name, message);
                            Debug.Log(message);
                                    
                        }

                    } else{
                        Debug.Log("Error: Player name can't have special symbols.");
                        ErrorHandler.Error("Error: Name cant have any special symbols.");
                        break;
                    }
                            
                } else{
                    message = lineCleared;
                    message = FilterMessage(message);
                    SaveMessageDataByLine(name, message);
                }

            }

            if (!string.IsNullOrEmpty(characterNames[0]) && !string.IsNullOrEmpty(characterNames[1])){
                //Debug.Log($"There are 2 players chatting here: {characterNames[0]} and {characterNames[1]}");
                isDataReady = true;
                Debug.Log("Data length " + characterDatas.Count);
            } else {
                ErrorHandler.Error("Error: Cant load character name.");
            }

        } catch (Exception e) {
            Debug.Log($"Error reading file: {e.Message}");
            ErrorHandler.Error($"Error reading files: {e.Message}");
        }

    }




    static int charNum;
    static void SaveMessageDataByLine(string name, string message){

        string data;

        if(characterNames[0] == name){
            charNum = 1;
        } else if(characterNames[1] == name){
            charNum = 2;
        }

        data = charNum.ToString() + message;

        characterDatas.Add(data);

    }

    public static void SaveMessageDataByList(List<string> MessagesList){
        characterDatas = MessagesList;
        isDataReady = true;
    }

    public static void SaveNameData(string[] CharNamesInput){
        characterNames = CharNamesInput;
    }

    static public void ResetData(){
        isDataReady = false;
        characterNames[0] = null;
        characterNames[1] = null;
        characterDatas.Clear();
        PlayerController.characterDatas.Clear();

        //Debug.Log("Data reseted.");
        
    }

    static public bool IsReady(){
        return isDataReady;
    }

    static string FilterName(string name){
        name = name.Remove(name.Length - 2);
        name = name[..1].ToUpper() + name[1..];

        return name;
    }

    static string FilterMessage(string message){
        message = Regex.Replace(message, editedMessage, "");

        if (isAutoGeneratedMessages(message)){
            return null;
        }

        return message;

    }

    static bool isAutoGeneratedMessages(string message){
        Match mediaContent = Regex.Match(message, mediaMessage);
        Match deletedContent = Regex.Match(message, deletedMessage);
        Match fileContent = Regex.Match(message, fileMessage);
        

        return mediaContent.Success || deletedContent.Success || fileContent.Success;
    }
}
