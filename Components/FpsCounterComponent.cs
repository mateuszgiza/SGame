namespace SGame.Components
{
    public class FpsCounterComponent : IComponent
    {
        public Entity Entity { get; set; }
        public FpsCounter DrawCounter { get; } = new FpsCounter();
        public FpsCounter UpdateCounter { get; } = new FpsCounter();
        public FpsCounter FixedUpdateCounter { get; } = new FpsCounter();
    }
}