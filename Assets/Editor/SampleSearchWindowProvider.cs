using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Editor
{
    public class SampleSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private MyGraphView _graphView;

        public void Initialize(MyGraphView graphView)
        {
            _graphView = graphView;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var entries = new List<SearchTreeEntry>();
            entries.Add(new SearchTreeGroupEntry(new GUIContent("Create Node")));

            // TODO 自動的に取得できるようにしたい。たとえばNodeフォルダ以下のクラスなど
            entries.Add(new SearchTreeEntry(new GUIContent("LogNode")) {level = 1, userData = typeof(LogNode)});
            entries.Add(new SearchTreeEntry(new GUIContent("StringNode")) {level = 1, userData = typeof(StringNode)});
            entries.Add(new SearchTreeEntry(new GUIContent("IntNode")) {level = 1, userData = typeof(IntNode)});
            return entries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            var type = SearchTreeEntry.userData as System.Type;
            var node = Activator.CreateInstance(type) as Node;
            // TODO ノードの場所は右クリックした位置の真下にしたい
            _graphView.AddElement(node);
            return true;
        }
    }
}