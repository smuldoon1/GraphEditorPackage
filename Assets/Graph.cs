using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    List<GraphNode> nodes; // List of nodes in the graph

    GraphNode hoveringNode; // Node currently being dragged
    Vector2 relativeDragPosition; // Relative mouse position of the rect of the last dragged node

    public Graph()
    {
        nodes = new List<GraphNode>();

        if (NodeCount == 0)
        {
            CreateNode(100, 100, 100, 100);
            CreateNode(100, 300, 100, 100);
            CreateNode(300, 100, 100, 100);
            CreateNode(300, 300, 100, 100);
        }
    }

    // Get the total number of nodes in the graph
    public int NodeCount
    {
        get { return nodes.Count; }
    }
    
    // Create a new node in the graph
    public void CreateNode(float x, float y, float width, float height)
    {
        GraphNode newNode = new GraphNode(new Rect(x, y, width, height));
        nodes.Insert(0, newNode); // New nodes are brought to the front of the list
    }

    // Render the graph window, nodes most recently interacted with are drawn on top
    public void Render()
    {
        for (int i = NodeCount - 1; i >= 0; i--)
        {
            nodes[i].DrawNode();
        }
    }

    // Retrieve and use user inputs
    public void Input()
    {
        // Left click a node to "pick it up" so it can be dragged. Will also move the node to the front of the render
        if (Event.current.type == EventType.MouseDown && Event.current.button == 0 && hoveringNode == null)
        {
            for (int i = 0; i < NodeCount; i++)
            {
                if (nodes[i].Rect.Contains(Event.current.mousePosition))
                {
                    GraphNode node = nodes[i];
                    hoveringNode = node;
                    relativeDragPosition = Event.current.mousePosition - node.Rect.position; // Calculate the relative position on the rect that was clicked
                    nodes.RemoveAt(i);
                    nodes.Insert(0, node); // Removes and reinserts the node at the front of the list
                    Event.current.Use();
                    break;
                }
            }
        }
        // Release the left mouse button to drop a held node in place
        if (Event.current.type == EventType.MouseUp && Event.current.button == 0 && hoveringNode != null)
        {
            hoveringNode = null;
            Event.current.Use();
        }
        // Drag the left mouse button to move nodes around the editor window
        if (Event.current.type == EventType.MouseDrag && Event.current.button == 0 && hoveringNode != null)
        {
            Vector2 pos = Event.current.mousePosition - relativeDragPosition;
            // If the control key is held, dragged nodes will align to a grid
            if (Event.current.modifiers == EventModifiers.Control)
                pos = new Vector2Int(Mathf.RoundToInt(pos.x / 50f) * 50, Mathf.RoundToInt(pos.y / 50f) * 50);
            hoveringNode.SetPosition(pos); // Set the position of the node, taking into account the relative position clicked on the node
            Event.current.Use();
        }

        // Right click to delete/create nodes
        if (Event.current.type == EventType.MouseDown && Event.current.button == 1)
        {
            bool deletedNode = false;
            for (int i = 0; i < NodeCount; i++)
            {
                if (nodes[i].Rect.Contains(Event.current.mousePosition))
                {
                    nodes.RemoveAt(i);
                    deletedNode = true;
                    Event.current.Use();
                    break;
                }
            }
            if (!deletedNode) // If no node is deleted, create a new node instead
            {
                Vector2 pos = Event.current.mousePosition;
                // If control is held, newly created nodes will be aligned to a grid
                if (Event.current.modifiers == EventModifiers.Control)
                    pos = new Vector2Int(Mathf.RoundToInt(pos.x / 50f) * 50, Mathf.RoundToInt(pos.y / 50f) * 50);
                CreateNode(pos.x - 50f, pos.y - 50f, 100f, 100f);
                Event.current.Use();
            }
        }
    }
}
