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
        while(!WhatsAppConverter.IsReady()){
            yield return null;
        }

        StartCoroutine(SetCharacterData());
        SetCharacterName(WhatsAppConverter.characterNames);
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

    IEnumerator SetCharacterData(){

        List<string> datas = GetCharacterData();
        Debug.Log("Data length " + datas.Count);

        for (int i = 0; i < datas.Count; i++){


            yield return new WaitForSeconds(1.5f);
            
            if(datas[i][..1] == "1"){
                SetCharacterText(1, datas[i][1..]);
                BubbleChat1.SetActive(true);
                BubbleChat2.SetActive(false);
            } else {
                SetCharacterText(2, datas[i][1..]);
                BubbleChat1.SetActive(false);
                BubbleChat2.SetActive(true);
            }


            Debug.Log(datas[i][1..]);
        }
    }

    List<string> GetCharacterData(){

        return WhatsAppConverter.characterDatas;
    }
}
