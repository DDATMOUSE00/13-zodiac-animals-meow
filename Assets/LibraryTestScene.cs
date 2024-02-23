using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryTestScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LibraryManager.I.AddBooks(1);
        LibraryManager.I.AddBooks(0);
    }


}
