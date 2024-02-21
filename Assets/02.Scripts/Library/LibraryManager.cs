using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LibraryManager : MonoBehaviour
{
    public static LibraryManager I;

    private bool[] Books =new bool[12];
    public List<Book> totalBooks = new List<Book>();
    public List<BookSlot> bSlots = new List<BookSlot>();
    public Slider slider;
    public TMP_Text cntTxt;

    private int cnt = 0;
   

    private void Start()
    {

        for(int i =0; i < Books.Length; i++)
        {
            Books[i] = false;
        }
    }
    private void Awake()
    {
        I = this;
    }

    private Book findBookWithId(int id)
    {
        foreach(var b in totalBooks)
        {
            if(b.id == id)
            {
                return b;
            }
        }
        return null;
    }

    private BookSlot findBookSlotWithId(int id)
    {
        foreach(var bSlot in bSlots)
        {
            if(bSlot.book.id == id)
            {
                return bSlot;
            }
  
        }
        return null;
    }
  
    private bool IsFull()
    {
        foreach(var b in Books)
        {
            if (b == false)
                return false;
        }
        return true;
    }
    public void AddBooks(int id)
    {

        if (Books[id] == false)
        {
            Books[id] = true;
            BookSlot b = findBookSlotWithId(id);
            Book book = findBookWithId(id);
            Debug.Log(book.title);
            b.AddStroyBook();

            cnt++;
            slider.value = cnt;
            cntTxt.text = $"{cnt}/12";
            
        }
        else
        {
            Debug.Log("이미 수집한 아이템");
        }
    }

}
