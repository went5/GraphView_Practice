using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class MyNode : Node
{
    public MyNode()
    {
        title = "Sample";
        var inputPort =
            Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Port));
        inputPort.portName = "Input";
        inputContainer.Add(inputPort);

        // Capacity
        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single,
            typeof(Port));
        outputPort.portName = "Output";
        outputContainer.Add(outputPort);

        var text = new TextField("Text Field");
        mainContainer.Add(text);
    }
}