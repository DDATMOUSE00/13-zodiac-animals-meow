using System;
using System.Collections.Generic;
using UnityEngine;

public class BinarySpacePartitioner
{
    RoomNode rootNode;

    public BinarySpacePartitioner(int dungeonWidth, int dungeonHeight)
    {
        this.rootNode = new RoomNode(new Vector2Int(0, 0), new Vector2Int(dungeonWidth, dungeonHeight), null, 0);
    }

    public RoomNode RootNode { get => rootNode; }

    internal List<RoomNode> PrepareNodesCollection(int maxIterations, int roomWidthMin, int roomHeightMin)
    {
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> listToReturn = new List<RoomNode>();
        graph.Enqueue(this.rootNode);
        listToReturn.Add(this.rootNode);

        int iterations = 0;
        while(iterations < maxIterations && graph.Count > 0)
        {
            iterations++;
            RoomNode currentNode = graph.Dequeue();

            if(currentNode.Width>=roomWidthMin*2 || currentNode.Height >= roomHeightMin * 2)
            {
                SplitTheSpace(currentNode, listToReturn, roomHeightMin, roomWidthMin, graph);
            }

        }
    }

    private void SplitTheSpace(RoomNode currentNode, List<RoomNode> listToReturn, int roomHeightMin, int roomWidthMin, Queue<RoomNode> graph)
    {
        Line line = GetLineDividingSpace(currentNode.BottomLeftAreawCorner, currentNode.TopRightAreaCorner, roomWidthMin, roomHeightMin);
    }

    private Line GetLineDividingSpace(Vector2Int bottomLeftAreawCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomHeightMin)
    {
        Orientation orientation;
        bool heightStatus = (topRightAreaCorner.y - bottomLeftAreawCorner.y) >= 2 * roomHeightMin;
        bool widthStatus = (topRightAreaCorner.x - bottomLeftAreawCorner.x) >= 2 * roomWidthMin;
        if(heightStatus&& widthStatus)
        {
            orientation = (Orientation)(UnityEngine.Random.Range(0, 2));
        }else if (widthStatus)
        {
            orientation = Orientation.Vertical;
        }
        else
        {
            orientation = Orientation.Horizontal;
        }

        return new Line(orientation, GetCoordinatesForOrientation(orientation, bottomLeftAreawCorner, topRightAreaCorner, roomWidthMin, roomHeightMin));

    }

    private Vector2Int GetCoordinatesForOrientation(Orientation orientation, Vector2Int bottomLeftAreawCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomHeightMin)
    {
        Vector2Int coordinates = Vector2Int.zero;
        if(orientation == Orientation.Horizontal)
        {
            coordinates = new Vector2Int(0, UnityEngine.Random.Range(
                (bottomLeftAreawCorner.y + roomHeightMin),
                (topRightAreaCorner.y - roomHeightMin)));
        }
    }
}