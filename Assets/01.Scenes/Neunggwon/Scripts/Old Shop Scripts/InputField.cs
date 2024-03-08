using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InputField : MonoBehaviour
{
    public TMP_InputField inputField;
    [SerializeField] private Button Eixt;

    private void OnEnable()
    {
        Cancle();
    }

    public void Cancle()
    {
        inputField.onSubmit.RemoveAllListeners();
    }
}
