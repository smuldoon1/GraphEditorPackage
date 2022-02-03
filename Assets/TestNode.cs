using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNode : GraphNode
{
    public TestNode(Rect rect, int inputCount, int outputCount) : base(rect, inputCount, outputCount)
    {
        color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
    }
}
