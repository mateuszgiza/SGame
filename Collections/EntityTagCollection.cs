using System.Collections.Generic;

namespace SGame
{
    public class EntityTagCollection
    {
        private List<string> tags = new List<string>();

        public bool HasTag(string tag)
        {
            return tags.Contains(tag);
        }

        public void AddTag(string tag)
        {
            if (!tags.Contains(tag))
            {
                tags.Add(tag);
            }
        }
    }
}