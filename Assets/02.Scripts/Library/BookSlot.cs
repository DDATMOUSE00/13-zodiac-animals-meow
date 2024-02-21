using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookSlot : MonoBehaviour
{
    public Book book;
    public GameObject storyBox;
    public GameObject img;


    public void AddStroyBook()
    {
       img.SetActive(true);
    }

    public void ShowDesc()
    {
        StoryBox sBox = storyBox.GetComponent<StoryBox>();
        sBox.Setting(book.title, book.desc);
        storyBox.SetActive(true);
    }
}
