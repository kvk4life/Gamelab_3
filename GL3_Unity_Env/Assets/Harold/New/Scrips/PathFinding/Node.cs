using UnityEngine;
using System.Collections;

public class Node {

    public bool walkeble;
    public Vector3 worldPosition;

    public Node(bool _walkeble, Vector3 _worldPos) {
        walkeble = _walkeble;
        worldPosition = _worldPos;
    }

}
