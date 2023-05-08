namespace Infrastructure.Services.Invenentory
{
    public class InventoryCell<T> : IInventoryCell<T>
    {
        public bool IsEmpty { get; private set; } = true;

        public T StoredEntity { get; private set; }

        public void Put(T entity)
        {
            if (!IsEmpty || entity == null)
                return;
            IsEmpty = false;
            StoredEntity = entity;
        }

        public T Take()
        {
            var tempEntity = StoredEntity;
            StoredEntity = default(T);
            IsEmpty = true;
            return tempEntity;
        }
    }
}