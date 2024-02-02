using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorNode : Node
{
    private Node node1;
    private Node node2;
    private int corridorWidth;
    private int modifierDistanceFromWall = 1;

    public CorridorNode(Node node1, Node node2, int corridorWidth) :base(null)
    {
        this.node1 = node1;
        this.node2 = node2;
        this.corridorWidth = corridorWidth;
        GenerateCorridor();
    }

    private void GenerateCorridor()
    {
        var relativePositionOfStructrue2 = CheckPositionStructure2AgainstStructure1();
        switch (relativePositionOfStructrue2)
        {
            case RelativePosition.Up:
                ProcessRoomInRelationUpOrDown(this.node1, this.node2);
                break;
            case RelativePosition.Down:
                ProcessRoomInRelationUpOrDown(this.node2, this.node1);
                break;
            case RelativePosition.Right:
                ProcessRoomInRelationRightOrLeft(this.node1, this.node2);
                break;
            case RelativePosition.Left:
                ProcessRoomInRelationRightOrLeft(this.node2, this.node1);
                break;
            default:
                break;
        }
    }

    private void ProcessRoomInRelationRightOrLeft(Node node1, Node node2)
    {
        Node leftStructure = null;
        List<Node> leftStructureChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(node1);
        Node rightStructure = null;
        List<Node> rightStructureChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(node2);

        var sortedLeftStructure = leftStructureChildren.OrderByDescending(child => child.TopRightAreaCorner.x).ToList();
        if(sortedLeftStructure.Count == 1)
        {
            leftStructure = sortedLeftStructure[0];
        }
        else
        {
            int maxX = sortedLeftStructure[0].TopRightAreaCorner.x;
            sortedLeftStructure = sortedLeftStructure.Where(children => Math.Abs(maxX - children.TopRightAreaCorner.x) < 10).ToList();
            int index = UnityEngine.Random.Range(0, sortedLeftStructure.Count);
            leftStructure = sortedLeftStructure[index];
        }

        var possibleNeighboursInRightStructureList = rightStructureChildren.Where(
            child => GetValidYForNeighoursLeftRight(
                leftStructure.TopRightAreaCorner,
                leftStructure.BottomRightAreaCorner,
                child.TopLeftAreaCorner,
                child.BottomLeftAreaCorner) != -1
                ).OrderBy(child=> child.BottomRightAreaCorner.x).ToList();

        if(possibleNeighboursInRightStructureList.Count <= 0)
        {
            rightStructure = node2;

        }
        else
        {
            rightStructure = possibleNeighboursInRightStructureList[0];
        }

        int y = GetValidYForNeighoursLeftRight(leftStructure.TopLeftAreaCorner, leftStructure.BottomRightAreaCorner,
            rightStructure.TopLeftAreaCorner,
           rightStructure.BottomLeftAreaCorner);

        while(y==-1 && sortedLeftStructure.Count > 0)
        {
            sortedLeftStructure = sortedLeftStructure.Where(child => child.TopLeftAreaCorner.y != leftStructure.TopLeftAreaCorner.y).ToList();
            leftStructure = sortedLeftStructure[0];
            y = GetValidYForNeighoursLeftRight(leftStructure.TopLeftAreaCorner, leftStructure.BottomRightAreaCorner,
            rightStructure.TopLeftAreaCorner,
           rightStructure.BottomLeftAreaCorner);
        }
        BottomLeftAreaCorner = new Vector2Int(leftStructure.BottomRightAreaCorner.x, y);
        TopRightAreaCorner = new Vector2Int(rightStructure.TopLeftAreaCorner.x, y + this.corridorWidth);
    }


    private int GetValidYForNeighoursLeftRight(Vector2Int leftNodeUp, Vector2Int leftNodeDown, Vector2Int RightNodeUp, Vector2Int RightNodeDown)
    {
        if(RightNodeUp.y >= leftNodeUp.y && leftNodeDown.y >= RightNodeDown.y)
        {
            return StructureHelper.CalculateMiddlePoint(
                leftNodeDown + new Vector2Int(0, modifierDistanceFromWall),
                leftNodeUp - new Vector2Int(0, modifierDistanceFromWall + this.corridorWidth)).y;
        }
        if(RightNodeUp.y <= leftNodeUp.y && leftNodeDown.y <= RightNodeDown.y)
        {
            return StructureHelper.CalculateMiddlePoint(
                RightNodeDown + new Vector2Int(0, modifierDistanceFromWall),
                RightNodeUp - new Vector2Int(0, modifierDistanceFromWall + this.corridorWidth)).y;
        }
        if (leftNodeUp.y >= RightNodeDown.y && leftNodeUp.y <= RightNodeUp.y)
        {
            return StructureHelper.CalculateMiddlePoint(
                RightNodeDown + new Vector2Int(0, modifierDistanceFromWall),
                leftNodeUp - new Vector2Int(0, modifierDistanceFromWall)).y;
        }
        if(leftNodeDown.y >= RightNodeDown.y && leftNodeDown.y <= RightNodeUp.y)
        {
            return StructureHelper.CalculateMiddlePoint(
                leftNodeDown + new Vector2Int(0, modifierDistanceFromWall),
                RightNodeUp - new Vector2Int(0, modifierDistanceFromWall + this.corridorWidth)).y;
        }

        return -1;
    }


    private void ProcessRoomInRelationUpOrDown(Node node1, Node node2)
    {
        Node bottomStructure = null;
        List<Node> structureBottomChildren = StructureHelper.TraverseGraphToExtractLowestLeafes(node1);
        Node topStructure = null;
        List<Node> structureAboveChildren  = StructureHelper.TraverseGraphToExtractLowestLeafes(node2);

        var sortedBottomStructure = structureBottomChildren.OrderByDescending(child => child.TopRightAreaCorner.y).ToList();
        if(sortedBottomStructure.Count == 1)
        {
            bottomStructure = structureBottomChildren[0];

        }
        else
        {

            int maxY = sortedBottomStructure[0].TopLeftAreaCorner.y;
            sortedBottomStructure = sortedBottomStructure.Where(child => Mathf.Abs(maxY - child.TopLeftAreaCorner.y) < 10).ToList();
            int index = UnityEngine.Random.Range(0, sortedBottomStructure.Count);
            bottomStructure = sortedBottomStructure[index];
        }

        var possibleNeighboursInTopStructure = structureAboveChildren.Where(
            child => GetValidYForNeighoursUPDown(
                bottomStructure.TopLeftAreaCorner,
                bottomStructure.TopRightAreaCorner,
                child.BottomLeftAreaCorner,
                child.BottomRightAreaCorner
                ) != -1).OrderBy(child => child.BottomRightAreaCorner.y).ToList();

        if(possibleNeighboursInTopStructure.Count == 0)
        {
            topStructure = node2;

        }
        else
        {
            topStructure = possibleNeighboursInTopStructure[0];

        }
        int x = GetValidYForNeighoursUPDown(
            bottomStructure.TopLeftAreaCorner,
            bottomStructure.TopRightAreaCorner,
            topStructure.BottomLeftAreaCorner,
            topStructure.BottomRightAreaCorner
            ); 

        while(x==-1 && sortedBottomStructure.Count > 1)
        {
            sortedBottomStructure = sortedBottomStructure.Where(child => child.TopLeftAreaCorner.x != topStructure.TopLeftAreaCorner.x).ToList();
            bottomStructure = sortedBottomStructure[0];
            x = GetValidYForNeighoursUPDown(
                bottomStructure.TopLeftAreaCorner,
                bottomStructure.TopRightAreaCorner,
                topStructure.BottomLeftAreaCorner,
                topStructure.BottomRightAreaCorner
                );
        }
        BottomLeftAreaCorner = new Vector2Int(x, bottomStructure.TopLeftAreaCorner.y);
        TopRightAreaCorner = new Vector2Int(x + this.corridorWidth, topStructure.BottomLeftAreaCorner.y);
    }

    private int GetValidYForNeighoursUPDown(Vector2Int bottomNodeLeft, Vector2Int bottomNodRight, Vector2Int topNodeLeft, Vector2Int topNodeRight)
    {
       if(topNodeLeft.x < bottomNodeLeft.x && bottomNodRight.x < topNodeRight.x)
        {
            return StructureHelper.CalculateMiddlePoint(
                bottomNodeLeft+new Vector2Int(modifierDistanceFromWall,0),
                bottomNodRight - new Vector2Int(this.corridorWidth+modifierDistanceFromWall,0)
                ).x;
        }
       if(topNodeLeft.x >= bottomNodeLeft.x && bottomNodRight.x >= topNodeRight.x)
        {
            return StructureHelper.CalculateMiddlePoint(
                    topNodeLeft + new Vector2Int(modifierDistanceFromWall, 0),
                    topNodeRight - new Vector2Int(this.corridorWidth + modifierDistanceFromWall, 0)
    ).x;
        }
       if(bottomNodeLeft.x >= (topNodeLeft.x)&&bottomNodeLeft.x <= topNodeRight.x)
        {
            return StructureHelper.CalculateMiddlePoint(
                   bottomNodeLeft + new Vector2Int(modifierDistanceFromWall, 0),
                   topNodeRight - new Vector2Int(this.corridorWidth + modifierDistanceFromWall, 0)
                    ).x;

        }
       if(bottomNodRight.x <= topNodeRight.x && bottomNodRight.x >= topNodeLeft.x)
        {
            return StructureHelper.CalculateMiddlePoint(
                    topNodeLeft + new Vector2Int(modifierDistanceFromWall, 0),
                    bottomNodRight - new Vector2Int(this.corridorWidth + modifierDistanceFromWall, 0)
                    ).x;
        }

        return -1;
    }

    private RelativePosition CheckPositionStructure2AgainstStructure1()
    {
        Vector2 middlePointStructure1Temp = ((Vector2)node1.TopRightAreaCorner+node1.BottomLeftAreaCorner)/ 2;
        Vector2 middlePointStructure2Temp = ((Vector2)node2.TopRightAreaCorner + node2.BottomLeftAreaCorner) / 2;

        float angle = CalculateAngle(middlePointStructure1Temp, middlePointStructure2Temp);

        if((angle < 45 && angle >= 0) || (angle>-45 && angle < 0))
        {
            return RelativePosition.Right;
        }
        else if (angle > 45 && angle < 135)
        {
            return RelativePosition.Up;
        }
        else if (angle >-135 && angle < -45)
        {
            return RelativePosition.Down;
        }
        else
        {
            return RelativePosition.Left;
        }
    }

    private float CalculateAngle(Vector2 middlePointStructure1Temp, Vector2 middlePointStructure2Temp)
    {
        return Mathf.Atan2(middlePointStructure2Temp.y - middlePointStructure1Temp.y, middlePointStructure2Temp.x - middlePointStructure1Temp.x)*Mathf.Rad2Deg;
    }
}