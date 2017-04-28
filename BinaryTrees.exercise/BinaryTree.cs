using System;

namespace BinaryTrees
{
    public class BinaryTree<T> where T : IComparable
    {
        private TreeNode<T> root;

        public void Add(T key)
        {
            var nodeToAdd = new TreeNode<T>(key);
            if (root == null)
            {
                root = nodeToAdd;
                return;
            }
            var currentNode = root;
            while(true)
            {
                if (currentNode.Value.CompareTo(key) > 0)
                {
                    if (currentNode.Left == null)
                    {
                        currentNode.Left = nodeToAdd;
                        break;
                    }
                    currentNode = currentNode.Left;
                }
                else
                {
                    if (currentNode.Right == null)
                    {
                        currentNode.Right = nodeToAdd;
                        break;
                    }
                    currentNode = currentNode.Right;
                }
            }
        }

        public bool Contains(T key)
        {
            var currentNode = root;
            while (currentNode != null)
            {
                var diff = currentNode.Value.CompareTo(key);
                if (diff == 0)
                    return true;
                currentNode = diff > 0 ? currentNode.Left : currentNode.Right;
            }
            return false;
        }
    }

    class TreeNode<T> where T : IComparable
    {
        public T Value { get; private set; }
        public TreeNode<T> Left, Right;

        public TreeNode(T value)
        {
            if(value == null)
                throw new ArgumentNullException(nameof(value)); 
            Value = value;
        }
    }
}