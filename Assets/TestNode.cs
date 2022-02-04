using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestNode : GraphNode
{
    public TestNode(Rect rect, int inputCount, int outputCount) : base(rect, inputCount, outputCount)
    {
        border = new RectOffset(5, 5, 5, 5);
    }

    public override void DrawContent()
    {
        
    }
}
