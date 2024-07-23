using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI m_1CharacterName;
    [SerializeField] TextMeshProUGUI m_2CharacterName;

    [SerializeField] TextMeshProUGUI m_1CharacterMessages;
    [SerializeField] TextMeshProUGUI m_2CharacterMessages;

    [SerializeField] GameObject BubbleChat1;
    [SerializeField] GameObject BubbleChat2;


    void Start(){
        StartCoroutine(WaitForFileReady());

        BubbleChat1.SetActive(false);
        BubbleChat2.SetActive(false);
        
        m_1CharacterName.text = "";
        m_2CharacterName.text = "";

        m_1CharacterMessages.text = "";
        m_2CharacterMessages.text = "";
    }

    IEnumerator WaitForFileReady(){
        while(!MessageDataManager.IsReady()){
            yield return null;
        }

        StartCoroutine(SetCharacterData());
        SetCharacterName(MessageDataManager.characterNames);
    }

    public void SetCharacterName(string[] name){
        m_1CharacterName.text = name[0];
        m_2CharacterName.text = name[1];
    }

    void SetCharacterText(int chara, string message){
        if(chara == 1){
            m_1CharacterMessages.text = message;
        } else {
            m_2CharacterMessages.text = message;
        }
    }

    static public List<string> characterDatas = new List<string>();
    IEnumerator SetCharacterData(){
    
        characterDatas = GetCharacterData();
        Debug.Log("Data length " + characterDatas.Count);
        

        for (int i = 0; i < characterDatas.Count; i++){
            
            yield return new WaitForSeconds(3f);
            
            if(characterDatas[i][..1] == "1"){
                SetCharacterText(1, characterDatas[i][1..]);
                BubbleChat1.SetActive(true);
                BubbleChat2.SetActive(false);
            } else {
                SetCharacterText(2, characterDatas[i][1..]);
                BubbleChat1.SetActive(false);
                BubbleChat2.SetActive(true);
            }


            //Debug.Log(datas[i][1..]);
        }

        BubbleChat1.SetActive(false);
        BubbleChat2.SetActive(false);

        StartCoroutine(WaitForFileReady());
    }

    List<string> GetCharacterData(){

        return MessageDataManager.characterDatas;
    }
}
