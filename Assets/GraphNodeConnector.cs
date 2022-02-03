using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphNodeConnector
{
    Rect rect;
    protected Color color;

    public GraphNodeConnector()
    {
        color = Color.white;
    }

    public Rect Rect
    {
        get { return rect; }
        set { rect = value; }
    }

    // Draw the connector relative to the graph node
    public void Draw()
    {
        GUI.DrawTexture(Rect, AssetDatabase.LoadAssetAtPath<Texture>("Assets/nodeConnector.png"), ScaleMode.StretchToFill, true, 0, color, 0, 0);
    }
}
