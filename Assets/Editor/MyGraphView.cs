using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Editor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class MyGraphView : GraphView
{
    public RootNode _root;

    public MyGraphView() : base()
    {
        // 親のUIに従って拡大縮小を行う
        style.flexGrow = 1;
        style.flexShrink = 1;

        // ズーム率の上限
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        // 背景の設定
        Insert(0, new GridBackground());

        // ドラッグ操作などの検知
        this.AddManipulator(new SelectionDragger());

        // 右クリック
        //SetEvents();
        SetRightClickEvent();

        // ルートノード
        _root = new RootNode();
        AddElement(_root);
    }

    // ポート同士でつなげるようになる
    public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        foreach (var port in ports.ToList())
        {
            // 同じノード同士をはじく
            if (startAnchor.node == port.node)
                continue;
            // input ↔ input と  output ↔ outputを防ぐ
            if (startAnchor.direction == port.direction)
                continue;
            // portはportとしか繋げない
            if (startAnchor.portType == typeof(Port) || port.portType == typeof(Port))
            {
                if (startAnchor.portType == port.portType)
                {
                    compatiblePorts.Add(port);
                }
                continue;
            }

            // portのtypeが一致していないとつなげない 例:intとstringは繋げない
            // TODO object型は受け付けるようにしたい(どうすれば？？
            // object output - int input は NG input.IsSubclassOf(output.type)
            // int output - object input は OK output.IsSubclassOf(input.type)
            if (startAnchor.direction == Direction.Output && !startAnchor.portType.IsSubclassOf(port.portType))
                continue;
            if (port.direction == Direction.Output && !port.portType.IsSubclassOf(startAnchor.portType))
                continue;
            compatiblePorts.Add(port);
        }

        return compatiblePorts;
    }

    // 右クリック時にCreate Nodeを選択するとサーチウィンドウが開けるようになる
    private void SetRightClickEvent()
    {
        var searchWindowProvider = ScriptableObject.CreateInstance<SampleSearchWindowProvider>();
        searchWindowProvider.Initialize(this);
        nodeCreationRequest += context =>
        {
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindowProvider);
        };
    }

    // 右クリックからCreate Nodeが選べるようになる
    private void SetEvents()
    {
        Debug.Log("SetEvents");
        // SampleGraphViewのメニュー周りのイベントを設定する処理
        nodeCreationRequest += context =>
        {
            // 後述するNodeというクラスのインスタンス生成
            var node = new MyNode();
            // GraphViewの子要素として追加する
            AddElement(node);
        };
    }

    // ここは適当
    public void ExecuteAllNode()
    {
        var edge = _root.OutputPort.connections.FirstOrDefault();
        IProcessNode currentNode;
        while (edge != null)
        {
            currentNode = edge.input.node as IProcessNode;
            if (currentNode == null)
                Debug.LogError("ProcessNode以外のノードが接続されています");
            currentNode.Execute();
            edge = currentNode.OutputPort.connections.FirstOrDefault();
        }
    }
}