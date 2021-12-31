using UnityEditor.Experimental.GraphView;

public interface IProcessNode
{
    public Port InputPort { get; }
    public Port OutputPort { get; }


    public abstract void Execute();
}