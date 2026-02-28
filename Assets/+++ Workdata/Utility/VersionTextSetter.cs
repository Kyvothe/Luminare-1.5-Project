using UnityEngine;
using TMPro;
using System;
public class VersionTextSetter : MonoBehaviour
{ 
    private void Awake()
    {
        gameObject.GetComponent<TMP_Text>().text = "Version: " + Application.version;
    }
}