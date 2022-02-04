using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestNode : GraphNode
{
    string name;

    public TestNode(Rect rect, int inputCount, int outputCount) : base(rect, inputCount, outputCount)
    {
        border = new RectOffset(5, 5, 5, 5);

        name = "Enter name";
    }

    public override void OnNodeGUI()
    {
        name = EditorGUI.TextField(new Rect(0, 0, ContentRect.width, ContentRect.height), "Name: ", name);
    }
}
