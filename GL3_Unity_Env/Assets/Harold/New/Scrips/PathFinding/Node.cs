﻿using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node> { 

    public bool walkeble;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;
    int heapIndex;

    public Node(bool _walkeble, Vector3 _worldPos, int _gridX, int _gridY) {
        walkeble = _walkeble;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int FCost {
        get {
            return gCost + hCost;
        }
    }

    public int HeapIndex {
        get {
            return heapIndex;
        }
        set {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare) {
        int compare = FCost.CompareTo(nodeToCompare.FCost);
        if(compare == 0) {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
