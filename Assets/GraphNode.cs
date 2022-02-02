using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphNode
{
    Rect rect;
    Color color;

    GraphNodeConnector[] inputConnectors; // Drag connections onto input connectors to connect to an output
    GraphNodeConnector[] outputConnectors; // Drag connections from output connectors to connect to an input

    public Rect Rect
    {
        get { return rect; }
        set { rect = value; }
    }

    public GraphNode(Rect rect)
    {
        this.rect = rect;
        color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);

        inputConnectors = new GraphNodeConnector[2] { new GraphNodeConnector(), new GraphNodeConnector() };
        outputConnectors = new GraphNodeConnector[5] { new GraphNodeConnector(), new GraphNodeConnector(), new GraphNodeConnector(), new GraphNodeConnector(), new GraphNodeConnector() };
    }

    // Draw the graph node
    public void Draw()
    {
        EditorGUI.DrawRect(rect, color);
        for (int i = 0; i < inputConnectors.Length; i++)
        {
            inputConnectors[i].Draw(new Vector2(rect.x, rect.y + (rect.height / (inputConnectors.Length + 1) * (i + 1))));
        }
        for (int i = 0; i < outputConnectors.Length; i++)
        {
            outputConnectors[i].Draw(new Vector2(rect.x + rect.width, rect.y + (rect.height / (outputConnectors.Length + 1) * (i + 1))));
        }
    }

    // Set the position of the graph node
    public void SetPosition(Vector2 position)
    {
        SetPosition(position.x, position.y);
    }
    
    // Set the position of the graph node
    public void SetPosition(float x, float y)
    {
        rect.x = x;
        rect.y = y;
    }

    // Set the size of the graph node
    public void SetSize(Vector2 size)
    {
        SetSize(size.x, size.y);
    }

    // Set the size of the graph node
    public void SetSize(float width, float height)
    {
        rect.width = width;
        rect.height = height;
    }
}
