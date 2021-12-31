using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class StringNode : Node, IParamNode
{
    // textfieldから文字列を設定できる
    private TextField textField;

    public object Param
    {
        get { return textField.value; }
    }

    public StringNode()
    {
        title = "String";

        // var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,
        //     typeof(string));
        // inputPort.portName = "In";
        // inputContainer.Add(inputPort);

        var outputPort =
            Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(string));
        outputPort.portName = "Out";
        outputContainer.Add(outputPort);

        textField = new TextField();
        mainContainer.Add(textField);
    }
}