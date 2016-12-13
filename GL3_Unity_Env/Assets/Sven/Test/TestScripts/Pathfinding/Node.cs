using UnityEngine;
using System.Collections;

public class Node : IHeapItem<Node> {

	public int xPos;
	public int yPos;
	public Vector3 worldPosition;
	public bool walkable;

	public int gCost;
	public int hCost;

	private int heapIndex;

	public Node parent;

	public Node (int x, int y, Vector3 pos, bool canWalk){

		xPos = x;
		yPos = y;
		worldPosition = pos;
		walkable = canWalk;

	}

	public int fCost {

		get{
			return gCost + hCost;
		}
	}

	public int HeapIndex {

		get{
			return heapIndex;
		}	

		set{
			heapIndex = value;
		}
	}

	public int CompareTo(Node nodeToCompare) {

		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if(compare == 0){
			compare = hCost.CompareTo(nodeToCompare.hCost);
		}

		return -compare;
	}
}
