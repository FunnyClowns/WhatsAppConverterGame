using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class WhatsAppConverter : MonoBehaviour
{

    static public void ReadFile(string FilePath){
        int lineCount = 0;
        string timelinePattern = @"\d{2}/\d{2}/\d{2}, \d{1,2}:\d{2} (?:am|pm) - ";
        string namePattern = @"^[a-zA-Z]+: ";
        string mediaMessage = @"^\<Media omitted\>";
        string editedMessage = @"\<This message was edited\>";

        string[] characters = new string[2];

        try{
            using (StreamReader sr = new(FilePath)){
                string line;


                while ((line = sr.ReadLine()) != null && lineCount < 50){
                    lineCount++;

                    if (lineCount != 1 && lineCount <= 50){
                        string lineCleared = Regex.Replace(line, timelinePattern, "");

                        Match lineContents = Regex.Match(line, timelinePattern);
                        if (lineContents.Success){
                            Match nameContents = Regex.Match(lineCleared, namePattern);
                            if (nameContents.Success){
                                string name = nameContents.Groups[0].Value;
                                string message = lineCleared.Substring(nameContents.Length);

                                name = name.Remove(name.Length - 2);
                                name = name.Substring(0, 1).ToUpper() + name.Substring(1);

                                if (characters[0] == null)
                                {
                                    characters[0] = name;
                                }
                                else if (characters[0] != name && (characters[1] == null))
                                {
                                    characters[1] = name;
                                }

                                Match messageContents = Regex.Match(message, mediaMessage);

                                message = Regex.Replace(message, editedMessage, "");
                                
                                if(!messageContents.Success){
                                    Debug.Log(message);
                                    lineCount -= 1;
                                }

                            } else{
                                Debug.Log("Error: Player name can't have special symbols.");
                                break;
                            }
                            
                        } else{
                            Debug.Log(lineCleared);
                            lineCount -= 1;
                        }

                    } else if (lineCount == 50) {
                        break;
                    }
                    
                }
            }

            if (!string.IsNullOrEmpty(characters[0]) && !string.IsNullOrEmpty(characters[1])){
                Debug.Log($"There are 2 players chatting here: {characters[0]} and {characters[1]}");
            }
            else{
                Debug.Log("Error loading players.");
            }
        } catch (Exception e) {
            Debug.Log($"Error reading file: {e.Message}");
        }

    }
}
