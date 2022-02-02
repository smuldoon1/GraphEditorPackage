using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GraphNode
{
    Rect rect;
    Color color;

    public Rect Rect
    {
        get { return rect; }
        set { rect = value; }
    }

    public void SetPosition(Vector2 position)
    {
        SetPosition(position.x, position.y);
    }

    public void SetPosition(float x, float y)
    {
        rect.x = x;
        rect.y = y;
    }

    public void SetSize(Vector2 size)
    {
        SetSize(size.x, size.y);
    }

    public void SetSize(float width, float height)
    {
        rect.width = width;
        rect.height = height;
    }

    public GraphNode(Rect rect)
    {
        this.rect = rect;
        color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
    }

    public void DrawNode()
    {
        EditorGUI.DrawRect(rect, color);
    }
}
