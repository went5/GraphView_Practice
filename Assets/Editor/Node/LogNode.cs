using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LogNode : Node, IProcessNode
{
    public Port InputPort { get; }
    public Port OutputPort { get; }
    private Port _inputParamPort { get; }

    public LogNode() : base()
    {
        title = "Log";

        InputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,
            typeof(Port));
        InputPort.portName = "In";
        inputContainer.Add(InputPort);

        OutputPort =
            Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(string));
        OutputPort.portName = "Out";
        outputContainer.Add(OutputPort);

        _inputParamPort =
            Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(object));
        inputContainer.Add(_inputParamPort);
    }


    public void Execute()
    {
        var edge = _inputParamPort.connections.FirstOrDefault();
        //苦肉の策 ほんとうだったらそのままinputのパラメーター取得したい
        var node = edge.output.node as IParamNode;

        if (node == null)
            return;
        Debug.Log(node.Param);
    }
}