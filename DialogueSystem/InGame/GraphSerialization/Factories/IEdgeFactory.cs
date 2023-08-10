namespace DS
{
    public interface IEdgeFactory
    {
        SerializableEdge Create(SerializableNode from, SerializableNode to);
    }
}