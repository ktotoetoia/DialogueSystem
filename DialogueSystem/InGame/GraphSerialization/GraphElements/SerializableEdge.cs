namespace DS
{
    [System.Serializable]
    public class SerializableEdge
    {
        public string Text;

        public int From;
        public int To;

        public SerializableEdge(SerializableNode from, SerializableNode to)
        {
            From = from?.ID ?? -1;
            To = to?.ID ?? -1;
        }
    }
}