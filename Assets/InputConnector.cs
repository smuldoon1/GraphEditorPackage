using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputConnector : GraphNodeConnector
{
    public List<OutputConnector> connections;

    public InputConnector()
    {
        connections = new List<OutputConnector>();
    }

    public void AddConnection(OutputConnector outputConnection)
    {
        connections.Add(outputConnection);
    }
}
