﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathRequestManager : MonoBehaviour {

	Queue <PathRequest> pathRequestQueue = new Queue <PathRequest>();
	PathRequest currentPathRequest;

	static PathRequestManager instance;

	Pathfinding pathfinding;
	public static Grid grid;

	bool isProcessingPath;

	void Awake () {

		instance = this;
		pathfinding = GetComponent<Pathfinding>();
		grid = GetComponent<Grid>();
	}

	public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callBack){

		PathRequest newRequest = new PathRequest(pathStart, pathEnd, callBack);
		instance.pathRequestQueue.Enqueue(newRequest);
		instance.TryProcessNext ();
		grid.CreateGrid ();

	}

	void TryProcessNext (){

		if (isProcessingPath == false && pathRequestQueue.Count > 0){
			currentPathRequest = pathRequestQueue.Dequeue();
			isProcessingPath = true;
			pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
		}
	}

	public void FinishedProcessingPath (Vector3[] path, bool succes){

		currentPathRequest.callBack(path, succes);
		isProcessingPath = false;
		TryProcessNext();
	}

	struct PathRequest {
		public Vector3 pathStart;
		public Vector3 pathEnd;
		public Action<Vector3[], bool> callBack;

		public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3 [], bool> _callBack){
			pathStart = _start;
			pathEnd = _end;
			callBack = _callBack;
		}
	}
}
