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
    public List<int> bookIDs;
    public GameObject catSlot;
    public Slider slider;
    public TMP_Text cntTxt;
    public GameObject UI;

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

    public void SaveLibraryData()
    {
        Debug.Log("save library data");
        DataManager.I.SaveJsonData<List<int>>(bookIDs, "BookData");
    }
    public void LoadLibraryData()
    {
        Debug.Log("load library data");
        bookIDs = DataManager.I.LoadJsonData<List<int>>("BookData");
        Debug.Log(bookIDs.Count);
        for(int i = bookIDs.Count-1; i>=0; i--)
        {
            Debug.Log(findBookWithId(i).title);
            BookSlot b = findBookSlotWithId(i);
            b.AddStroyBook();
            AddBooks(i);
        }
    }
    public Book findBookWithId(int id)
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

    public BookSlot findBookSlotWithId(int id)
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

    public void OpenUI()
    {
        UI.SetActive(true);
    }
    public void AddBooks(int id)
    {

        if (Books[id] == false)
        {
            Books[id] = true;
            BookSlot b = findBookSlotWithId(id);
            Book book = findBookWithId(id);
            b.AddStroyBook();
            bookIDs.Add(id);
            cnt++;
            slider.value = cnt;
            cntTxt.text = $"{cnt}/12";

            if(slider.value == 12)
            {
                //BookSlot catSlot = findBookSlotWithId(13);
                catSlot.GetComponent<BookSlot>().AddStroyBook();
                bookIDs.Add(13);
            }
            
        }
        else
        {
            Debug.Log("이미 수집한 아이템");
        }
    }

}
