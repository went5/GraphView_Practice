using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;

public class IntNode : Node, IParamNode
{
    public object Param { get; }
    private IntegerField _intField = new IntegerField();


    public IntNode()
    {
        title = "Int";

        // var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,
        //     typeof(string));
        // inputPort.portName = "In";
        // inputContainer.Add(inputPort);

        var outputPort =
            Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(int));
        outputPort.portName = "Out";
        outputContainer.Add(outputPort);

        mainContainer.Add(_intField);
    }
}