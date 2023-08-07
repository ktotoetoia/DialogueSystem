namespace DS
{
    public class SerializableEdge
    {
        public string Text { get; set; }

        public int From { get; set; }
        public int To { get; set; }

        public SerializableEdge()
        {

        }

        public SerializableEdge(SerializableNode from, SerializableNode to)
        {
            From = from?.ID ?? -1;
            To = to?.ID ?? -1;
        }
    }
}