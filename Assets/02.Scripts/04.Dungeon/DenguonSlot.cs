using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DenguonSlot : MonoBehaviour
{
    public TMP_Text denguonNameText;
    public string denguonName;
    public string sceneName;

    //public Sprite denguonIcon;

    private void Start()
    {
        denguonNameText.text = denguonName;
    }

    public void OnButton()
    {
        DungeonScrollView.Instance.SealectDenguonSlot(this);
    }

    public string SceneName()
    {
        return sceneName;
    }
}
