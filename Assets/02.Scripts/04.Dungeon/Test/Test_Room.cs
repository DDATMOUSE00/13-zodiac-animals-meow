using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class Test_Room : MonoBehaviour
{
    public enum Directions
    {
        Up,
        Right,
        Down,
        Left,
    }

    [System.Serializable]
    public struct Doors
    {
        [HideInInspector] public bool active;

        public Directions direction;
        public Room leadsTo;
    }

    public Doors[] roomDoors = new Doors[4];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
