using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDelete : MonoBehaviour
{
    TextMeshProUGUI textMesh;

    void Awake(){
        textMesh = GetComponent<TextMeshProUGUI>();

        textMesh.text = "";
    }
}
