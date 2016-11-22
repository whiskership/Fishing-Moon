using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
    public Transform player;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start() {
        RefreshGrid();
    }

    public void RefreshGrid() {
        float mapLength = Room_Switch.ActiveRoom.GetComponent<Tiled2Unity.TiledMap>().MapWidthInPixels;
        float mapHeight = Room_Switch.ActiveRoom.GetComponent<Tiled2Unity.TiledMap>().MapHeightInPixels;

        gridWorldSize = new Vector2(mapLength, mapHeight);

        transform.position = new Vector2(Room_Switch.ActiveRoom.position.x + mapLength / 2, (Room_Switch.ActiveRoom.position.y - mapHeight / 2));

        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    public int MaxSize {
        get {
            return gridSizeX * gridSizeY;
        }
    }

    void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);

                bool walkable = !Physics2D.OverlapCircle(worldPoint, nodeRadius / 2, unwalkableMask);
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node) {
        List<Node> neigbours = new List<Node>();

        for (int y = -1; y <= 1; y++) {
            for (int x = -1; x <= 1; x++) {
                
                if (x != 0 && y != 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    neigbours.Add(grid[checkX, checkY]);

            }
        }

        return neigbours;
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition) {
        worldPosition = worldPosition - (Vector2)transform.position;
        float percentX = worldPosition.x / gridWorldSize.x + 0.5f;
        float percentY = worldPosition.y / gridWorldSize.y + 0.5f;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public List<Node> path;
    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 0));

        if (grid != null) {
            Node playerNode = NodeFromWorldPoint(player.position);

            foreach (Node n in grid) {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                
                if (playerNode == n)
                    Gizmos.color = Color.cyan;

                if (path != null && path.Contains(n))
                    Gizmos.color = Color.black;

                Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.4f);

                Gizmos.DrawCube(n.worldPosition, Vector2.one * (nodeDiameter - 1f));
            }
        }
    }
}