namespace SGame
{
    [System.Serializable]
    public class LayerAlreadyExistsException : System.Exception
    {
        public LayerAlreadyExistsException(string layerName) : base($"Layer {layerName} already exists!") { }
        protected LayerAlreadyExistsException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}