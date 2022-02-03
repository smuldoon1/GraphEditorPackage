using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class GraphNode
{
    Rect rect;
    protected Color color;

    public List<InputConnector> inputConnectors; // Drag connections onto input connectors to connect to an output
    public List<OutputConnector> outputConnectors; // Drag connections from output connectors to connect to an input

    public Rect Rect
    {
        get { return rect; }
        set { rect = value; }
    }

    public GraphNode(Rect rect, int inputCount, int outputCount)
    {
        this.rect = rect;

        inputConnectors = new List<InputConnector>();
        outputConnectors = new List<OutputConnector>();

        for (int i = 0; i < inputCount; i++)
            inputConnectors.Add(new InputConnector());
        for (int i = 0; i < outputCount; i++)
            outputConnectors.Add(new OutputConnector());

        UpdateConnectors();
    }

    // Draw the graph node
    public void Draw()
    {
        EditorGUI.DrawRect(rect, color);
        for (int i = 0; i < inputConnectors.Count; i++)
        {
            inputConnectors[i].Draw();
        }
        for (int i = 0; i < outputConnectors.Count; i++)
        {
            outputConnectors[i].Draw();
        }
    }

    // Update the rects of all connectors
    public void UpdateConnectors()
    {
        for (int i = 0; i < inputConnectors.Count; i++)
        {
            inputConnectors[i].Rect = new Rect(rect.x - 7.5f, rect.y + (rect.height / (inputConnectors.Count + 1) * (i + 1)) - 7.5f, 15f, 15f);
        }
        for (int i = 0; i < outputConnectors.Count; i++)
        {
            outputConnectors[i].Rect = new Rect(rect.x + rect.width - 7.5f, rect.y + (rect.height / (outputConnectors.Count + 1) * (i + 1)) - 7.5f, 15f, 15f);
        }
    }

    // Delete the node
    public void Delete()
    {
        // Clear the connections of all input nodes, as well as removing their reference to this node
        foreach (InputConnector input in inputConnectors)
        {
            foreach (OutputConnector connection in input.connections)
            {
                if (connection.connection != null)
                    connection.connection = null;
            }
        }
        // Clear the connection of any output nodes
        foreach (OutputConnector output in outputConnectors)
        {
            output.connection = null;
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
