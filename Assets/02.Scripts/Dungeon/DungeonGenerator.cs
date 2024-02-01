using System;
using System.Collections.Generic;
using UnityEngine;
public class DungeonGenerator
{
    RoomNode rootNode;
    List<RoomNode> allSpaceNodes = new List<RoomNode>();

    private int dungeonWidth;
    private int dungeonHeight;
    
    public DungeonGenerator(int dungeonWidth, int dungeonHeight)
    {
        this.dungeonWidth = dungeonWidth;
        this.dungeonHeight = dungeonHeight;
    }

    public List<Node> CalculateRooms(int maxIterations, int roomWidthMin, int roomHeightMin)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(dungeonWidth, dungeonHeight);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomHeightMin);
    }
}