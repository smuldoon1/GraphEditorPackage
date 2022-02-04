using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    List<GraphNode> nodes; // List of nodes in the graph

    GraphNode hoveringNode; // Node currently being dragged
    Vector2 relativeDragPosition; // Relative mouse position of the rect of the last dragged node

    OutputConnector selectedConnector;

    public Graph()
    {
        nodes = new List<GraphNode>();
    }

    // Get the total number of nodes in the graph
    public int NodeCount
    {
        get { return nodes.Count; }
    }

    // Create a new node in the graph
    public void CreateNode(Vector2 position)
    {
        float width = 300f;
        float height = 200f;
        int inputs = 2;
        int outputs = 3;
        GraphNode node = new TestNode(new Rect(position.x - width * .5f, position.y - height * .5f, width, height), inputs, outputs);
        nodes.Insert(0, node); // New nodes are brought to the front of the list
    }

    // Render the graph window, nodes most recently interacted with are drawn on top
    public void Render()
    {
        for (int i = NodeCount - 1; i >= 0; i--)
        {
            nodes[i].Draw();
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
                for (int j = 0; j < nodes[i].outputConnectors.Count; j++)
                {
                    if (nodes[i].outputConnectors[j].Rect.Contains(Event.current.mousePosition) && selectedConnector == null)
                    {
                        selectedConnector = nodes[i].outputConnectors[j];
                        selectedConnector.Selected = true;
                        BringNodeForward(nodes[i]);
                        Event.current.Use();
                        return;
                    }
                }
                for (int j = 0; j < nodes[i].inputConnectors.Count; j++)
                {
                    if (nodes[i].inputConnectors[j].Rect.Contains(Event.current.mousePosition) && selectedConnector != null)
                    {
                        selectedConnector.Selected = false;
                        selectedConnector.connection = nodes[i].inputConnectors[j];
                        selectedConnector = null;
                        BringNodeForward(nodes[i]);
                        Event.current.Use();
                        return;
                    }
                }
            }
            for (int i = 0; i < NodeCount; i++)
            {
                if (selectedConnector != null)
                {
                    selectedConnector.Selected = false;
                    selectedConnector = null;
                    Event.current.Use();
                    return;
                }
                if (nodes[i].Rect.Contains(Event.current.mousePosition))
                {
                    hoveringNode = nodes[i];
                    relativeDragPosition = Event.current.mousePosition - nodes[i].Rect.position; // Calculate the relative position on the rect that was clicked
                    BringNodeForward(nodes[i]); // Removes and reinserts the node at the front of the list
                    Event.current.Use();
                    return;
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
            hoveringNode.UpdateConnectors(); // Update the nodes connectors positions
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
                    DeleteNode(nodes[i]);
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
                //CreateNode(pos.x - 50f, pos.y - 50f, 100f, 100f);
                CreateNode(pos);
                Event.current.Use();
            }
        }
    }

    // Delete a node and remove it from the graph
    public void DeleteNode(GraphNode node)
    {
        node.Delete();
        nodes.Remove(node);
    }

    // Brings a node to the front of the render by removing and reinserting it at position 0
    public void BringNodeForward(GraphNode node)
    {
        nodes.Remove(node);
        nodes.Insert(0, node);
    }
}
