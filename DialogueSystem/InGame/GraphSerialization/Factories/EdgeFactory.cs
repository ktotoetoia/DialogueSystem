namespace DS
{
    public class EdgeFactory : IEdgeFactory
    {
        public SerializableEdge Create(SerializableNode from, SerializableNode to)
        {
            return new SerializableEdge(from, to);
        }
    }
}