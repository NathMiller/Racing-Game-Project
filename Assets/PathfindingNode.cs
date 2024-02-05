//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PathfindingNode : MonoBehaviour
//{
//    List<PathfindingNode> allNeighbours = new List<PathfindingNode>();

//    public float traversalCost;

//    public float g;
//    public float h;
//    public PathfindingNode parent;

//    enum NodeType
//    {
//        Track,
//        Gravel,
//        Obstacle,
//        MAX
//    }
//    NodeType nodeType = NodeType.Track;

//    public bool IsObstacle()
//    {
//        return nodeType == NodeType.Obstacle;
//    }
//    public float GetFScore()
//    {
//        return g + h;
//    }

//    static Vector3[] directions =
//    {
//        new Vector3(-1, 0, 0),
//        new Vector3(1, 0, 0),
//        new Vector3(0, 0, -1),
//        new Vector3(0, 0, 1)
//    };

//    void FindNeighbours(bool reconnectingNeighbour = false)
//    {
//        allNeighbours.Clear();
//        foreach (Vector3 direction in directions)
//        {
//            RaycastHit info;
//            if (Physics.Raycast(transform.position, direciton, out info))
//            {
//                PathfindingNode node =
//                    info.transform.gameObject.GetComponent<PathfindingNode>();
//                if (node)
//                {
//                    allNeighbours.Add(node);
//                }
//            }
//        }
//    }

//    void SetupNode()
//    {
//        switch(nodeType)
//    }
//}
