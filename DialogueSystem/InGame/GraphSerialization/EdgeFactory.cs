namespace DS
{
    public class EdgeFactory : IEdgeFactory
    {
        public SerializableEdge Create(SerializableNode from, SerializableNode to)
        {
            SerializableEdge edge = new SerializableEdge(from, to);
            return edge;
        }
    }
}