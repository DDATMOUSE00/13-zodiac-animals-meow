using System;

public class DungeonGenerator
{
    RoomNode rootNode;
   // List<RoomNode> allSpaceNodes = new List<RoomNode>();

    private int dungeonWidth;
    private int dungeonHeight;
    
    public DungeonGenerator(int dungeonWidth, int dungeonHeight)
    {
        this.dungeonWidth = dungeonWidth;
        this.dungeonHeight = dungeonHeight;
    }

    internal object CalculateRooms(int maxIterations, int roomWidthMin, int roomHeightMin)
    {
        throw new NotImplementedException();
    }
}