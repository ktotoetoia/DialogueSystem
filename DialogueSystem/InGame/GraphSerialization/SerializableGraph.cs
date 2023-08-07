using System.Collections.Generic;

namespace DS
{
    public class SerializableGraph : SerializableGroup
    {
        public List<SerializableGroup> Groups { get; set; } = new List<SerializableGroup>();
    }
}