using UnityEngine;

namespace DS
{
    public class SerializableNode
    {
        public string AdditionalInformation { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public Vector2 Position { get; set; }
        public int ID { get; set; }
        public bool OneLine { get; set; }
    }
}