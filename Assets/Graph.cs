using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    List<GraphNode> nodes;

    GraphNode hoveringNode;
    Vector2 relativeDragPosition;

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

    public int NodeCount
    {
        get { return nodes.Count; }
    }
    
    public void CreateNode(float x, float y, float width, float height)
    {
        GraphNode newNode = new GraphNode(new Rect(x, y, width, height));
        nodes.Add(newNode);
    }

    public void Render()
    {
        for (int i = NodeCount - 1; i >= 0; i--)
        {
            nodes[i].DrawNode();
        }
    }

    public void Input()
    {
        if (Event.current.type == EventType.MouseDown && hoveringNode == null)
        {
            for (int i = 0; i < NodeCount; i++)
            {
                if (nodes[i].Rect.Contains(Event.current.mousePosition))
                {
                    GraphNode node = nodes[i];
                    hoveringNode = node;
                    relativeDragPosition = Event.current.mousePosition - node.Rect.position;
                    nodes.RemoveAt(i);
                    nodes.Insert(0, node);
                    Event.current.Use();
                    break;
                }
            }
        }
        if (Event.current.type == EventType.MouseUp && hoveringNode != null)
        {
            hoveringNode = null;
            Event.current.Use();
        }
        if (Event.current.type == EventType.MouseDrag && hoveringNode != null)
        {
            hoveringNode.SetPosition(Event.current.mousePosition - relativeDragPosition);
            Event.current.Use();
        }
    }
}
