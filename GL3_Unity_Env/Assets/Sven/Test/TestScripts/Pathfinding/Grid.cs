using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {

	public Node [,] grid;

	public bool showPath;

	public float nodeRadius;
	public float nodeDiameter;

	public int gridSizeX, gridSizeY;

	public float worldPointDevider;

	public Vector2 worldSize;
	public Vector3 mapStartPoint;

	public LayerMask unWalkableMask;

	void Start () {

		nodeDiameter = nodeRadius * 2;
		mapStartPoint = transform.position - Vector3.right * worldSize.x / 2 - Vector3.forward * worldSize.y / 2;

		gridSizeX = Mathf.RoundToInt(worldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(worldSize.y / nodeDiameter);

		CreateGrid();
	}

	public int MaxSize {
		get {
			return gridSizeX * gridSizeY;
		}
	}

	
	void CreateGrid() {

		grid = new Node[gridSizeX,gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * worldSize.x/2 - Vector3.forward * worldSize.y/2;
 
		for (int x = 0; x < gridSizeX; x ++) {
            for (int y = 0; y < gridSizeY; y ++) {
            	Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = (Physics.CheckSphere(worldPoint, nodeRadius, unWalkableMask)) && !(Physics.CheckSphere(worldPoint, nodeRadius, LayerMask.GetMask("unwalkable")));

                grid[x,y] = new Node(x, y, worldPoint, walkable);
                }
           	}
       }

	public List<Node> GetNeighbours(Node node){

		List<Node> neighbours = new List<Node>();

		for (int i = -1; i <= 1; i ++){
			for(int j = -1; j <= 1; j ++){
				if(i == 0 && j == 0)
					continue;

					int checkX = node.xPos + i;
					int checkY = node.yPos + j;

					if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY){
						neighbours.Add(grid[checkX, checkY]);
					
				}
			}
		}

		return neighbours;
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition){
		float percentX = (worldPosition.x + worldSize.x / worldPointDevider) / worldSize.x;
		float percentY = (worldPosition.z + worldSize.y / worldPointDevider) / worldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

		return grid [x,y];

	}

	void OnDrawGizmos() {

		Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, 1, worldSize.y));

		if(grid != null && showPath == true){
			foreach (Node n in grid){
				if(n.walkable){
					Gizmos.color = Color.white;
				}
				else{
					Gizmos.color = Color.red;
				}
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
			}
		}
	}	
}
