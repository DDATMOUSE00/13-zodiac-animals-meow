using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePopup_UI : MonoBehaviour
{
    [SerializeField] private GameObject _popupUI;
    void Start()
    {
        BuildingUpgradeManager.Instance.currentLevel.ToString();
    }

    void Update()
    {
        
    }
}
