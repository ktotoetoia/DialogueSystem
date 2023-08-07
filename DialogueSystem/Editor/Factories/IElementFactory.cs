using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DS
{
    public interface IElementFactory
    {
        public GraphElement Create(Vector2 position);
        public GraphElement AddNewToGraphView(Vector2 position);
    }
}