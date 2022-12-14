using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//source: https://medium.com/geekculture/how-to-create-a-simple-behaviour-tree-in-unity-c-3964c84c060e
namespace BehaviorTree
{
    
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }
    
    public class Node
    {
        protected NodeState State;

        public Node parent;
        protected List<Node> children = new List<Node>();

        private Dictionary<String, object> _dataContext = new Dictionary<string, object>();

        public Node()
        {
            parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (var child in children)
            {
                _Attach(child);
                child.initialize();
            }
        }

        public virtual void initialize(){}
        
        private void _Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public void SetData(String key, object value)
        {
            _dataContext[key] = value;
        }

        public void SetDataRoot(String key, object value)
        {
            Node node = parent;
            while (node.parent != null)
            {
                node = node.parent;
            }
            node.SetData(key, value);
        }
        
        public object GetData(String key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value)) return value;
            Node node = parent;

            while (node != null)
            {
                value = node.GetData(key);
                if (value != null) return value;

                node = node.parent;
            }

            return value;
        }

        public bool ClearData(String key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared) return true;
                node = node.parent;
            }

            return false;
        }
        
        public virtual NodeState Evaluate() => NodeState.FAILURE;
    }

    
}