using System.Collections.Generic;

namespace DS
{
    [System.Serializable]
    public class SerializableGraph : SerializableGroup
    {
        public List<SerializableGroup> Groups = new List<SerializableGroup>();
    }
}