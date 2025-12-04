using System;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshGenerator : MonoBehaviour
{
    private HashSet<Vector3> cubes;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    //generates the navmesh
    //lots of rounding of points to make sure everything hashes correctly
    public NavMesh GenerateNavMesh(AIMovementParameters aiMovement)
    {
        NavMesh mesh = new NavMesh();
        cubes = new HashSet<Vector3>();


        //get cube positions
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 pos = transform.GetChild(i).position;
            cubes.Add(RoundVector3(pos));
        }

        //create valid navMeshNodes
        foreach (Vector3 pos in cubes)
        {
            if (!cubes.Contains(pos + Vector3.up) && !cubes.Contains(pos + Vector3.up * 2))
            {
                Vector3 nodePos = pos + Vector3.up;
                nodePos = RoundVector3(nodePos);
                NavMeshNode node = new NavMeshNode(nodePos);
                mesh.AddNode(nodePos, node);
            }
        }

        Dictionary<Vector3, NavMeshNode> nodes = mesh.GetNodes();
        foreach(KeyValuePair<Vector3,NavMeshNode> node in nodes)
        {
            //create connections between nodes without jumping
            List<Vector3> neighbors = GetNodeNeighbors(node.Key);
            List<Vector3> jumpNeighbors = new List<Vector3>();
            for(int i = 0;i < neighbors.Count;i++)
            {
                neighbors[i] = RoundVector3(neighbors[i]);
                if (!cubes.Contains(neighbors[i]) && nodes.ContainsKey(neighbors[i]))
                {
                    NavMeshConnection connection = new NavMeshConnection();
                    connection.start = node.Value;
                    connection.end = nodes[neighbors[i]];
                    connection.action = MoveAction.RUN;
                    connection.cost = 1; //fix this to be a correct cost
                    Debug.DrawLine(node.Key, neighbors[i], Color.red, 20.0f);
                }
                else
                {
                    jumpNeighbors.Add(neighbors[i]);
                }
            }


            //create jump connections
            //float jumpDistance 

            //Queue<Vector3> searchPoints = new Queue<Vector3>();
            //List<Vector3> jumpDirections = new List<Vector3>();
            //HashSet<Vector3> checkedPoints = new HashSet<Vector3>();

            //searchPoints.Enqueue(node.Key);
            //checkedPoints.Add(RoundVector3(node.Key));
            //for(int i = 0;i < jumpNeighbors.Count;i++)
            //{
            //    jumpDirections.Add(jumpNeighbors[i] - node.Key);
            //}


            //while(true)
            //{
            //    for(int i = 0;i < jumpDirections.Count;i++)
            //    {
            //        Vector3 point = searchPoints.Dequeue();
            //        if (!checkedPoints.Contains(RoundVector3(point + jumpDirections[i]))) 
            //    }
            //}



        }



        return mesh;
    }

    public Vector3 RoundVector3(Vector3 vec)
    {
        return new Vector3(Mathf.Round(vec.x), Mathf.Round(vec.y), Mathf.Round(vec.z));
    }
    public Vector3 GetTopOfPillar(Vector3 pos)
    {
        Vector3 currentPos = pos;
        while(!cubes.Contains(currentPos))
        {
            currentPos += Vector3.up;
        }
        return currentPos;
    }


    //gets all neighbors, neighbors do not need to be valid here
    public List<Vector3> GetNodeNeighbors(Vector3 node)
    {
        List<Vector3> neighbors = new List<Vector3>();
        neighbors.Add(node + new Vector3(-1, 0, 0));
        neighbors.Add(node + new Vector3(1, 0, 0));
        neighbors.Add(node + new Vector3(0, 0, 1));
        neighbors.Add(node + new Vector3(0, 0, -1));
        //for(int i = -1;i <= 1;i++)
        //{
        //    for(int j = -1;j <= 1;j++)
        //    {
        //        if(i != 0 || j != 0)
        //        {
        //            neighbors.Add(node + new Vector3(i, 0, j));
        //        }
        //    }
        //}
        return neighbors;
    }
}

public class NavMesh
{
    public void AddNode(Vector3 pos,NavMeshNode node)
    {
        nodes[pos] = node;
    }
    public Dictionary<Vector3,NavMeshNode> GetNodes()
    {
        return nodes;
    }

    Dictionary<Vector3,NavMeshNode> nodes = new Dictionary<Vector3, NavMeshNode>();
}

public enum MoveAction
{
    RUN,
    JUMP,
    DROP
}


public class NavMeshNode
{
    public NavMeshNode(Vector2 pos)
    {
        this.pos = pos;
        connections = new List<NavMeshConnection>();
    }

    public void AddConnection(NavMeshConnection connection)
    {
        connections.Add(connection);
    }

    private Vector2 pos;
    private List<NavMeshConnection> connections;
}

public class NavMeshConnection
{

    public NavMeshNode start;
    public NavMeshNode end;
    public MoveAction action;
    public float cost;
}

public class AIMovementParameters
{
    public float movementSpeed;
    public float jumpPower;
    public float gravityStrength;
    public float linearDamping;
}
