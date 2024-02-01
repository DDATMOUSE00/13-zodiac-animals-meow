using UnityEngine;

public class RoomNode : Node
{
    public RoomNode(Vector2Int bottomLeftAreaCorner,  Vector2Int topRightAreaCorner, Node parentNode, int index) : base(parentNode)
    {
        this.BottomLeftAreawCorner = bottomLeftAreaCorner;
        this.TopLeftAreaCorner = topRightAreaCorner;
        this.BottomRightAreawCorner = new Vector2Int(topRightAreaCorner.x, bottomLeftAreaCorner.y);
        this.TopLeftAreaCorner = new Vector2Int(bottomLeftAreaCorner.x, TopRightAreaCorner.y);

        this.TreeLayerIndex = index;
;    }

    public int Width { get => (int)(TopRightAreaCorner.x - BottomLeftAreawCorner.x); }
    public int Height { get => (int)(TopRightAreaCorner.y - BottomLeftAreawCorner.y); }
}