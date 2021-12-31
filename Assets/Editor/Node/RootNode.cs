using UnityEditor.Experimental.GraphView;


public class RootNode : Node
{
    public readonly Port OutputPort;

    public RootNode()
    {
        title = "Root";
        // 削除できなくする
        capabilities -= Capabilities.Deletable;


        // var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,
        //     typeof(Port));
        // inputPort.portName = "In";
        // inputContainer.Add(inputPort);

        OutputPort =
            Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
        OutputPort.portName = "Out";
        outputContainer.Add(OutputPort);
    }
}