namespace SGame
{
    public static class EntityExtensions {
        public static Entity WithComponent(this Entity entity, IComponent component) {
            component.Entity = entity;
            entity.Components.Attach(component);
            return entity;
        }
    }
}