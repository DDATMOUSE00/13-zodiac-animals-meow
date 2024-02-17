using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType
{
    StartRoom,
    BossRoom,
    DungeonRoom
}

[CreateAssetMenu(fileName = " RoomData", menuName = "New RoomData")]
public class RoomData : ScriptableObject
{
    [Header("#Room_Info")]
    public string roomName;

    [Header("#Room_Enemys")]
    public int maxEnemyCount;
    public GameObject[] enemys;
    public GameObject Boss;

    [Header("#RoomType")]
    public RoomType roomType;
}
