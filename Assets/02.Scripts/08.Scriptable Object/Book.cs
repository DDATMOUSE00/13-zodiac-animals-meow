using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "08.Scriptable Object/BookSO")]
public class Book : ScriptableObject
{
    public int id;
    public string title;
    public string desc;
}
