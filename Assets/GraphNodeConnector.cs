using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphNodeConnector
{
    // Draw the connector relative to the graph node
    public void Draw(Vector2 position)
    {
        EditorGUI.DrawRect(new Rect(position.x - 5f, position.y - 5f, 10f, 10f), Color.white);
    }
}
