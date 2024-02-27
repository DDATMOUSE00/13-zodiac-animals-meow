using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGold : MonoBehaviour
{
    public int Gold { get; set; }
    private void Awake()
    {
        Gold = 0;
    }

    public void AddGold(int AddGold)
    {
        Gold += AddGold;
    }

    public void RemoveGold(int RemoveGold)
    {
        Gold -= RemoveGold;
    }
}
