namespace Infrastructure.Services.Invenentory
{
    public interface IInventoryCell<T>
    {
        public bool IsEmpty
        {
            get;
        }

        public T StoredEntity
        {
            get;
        }

        public void Put(T entity);

        public T Take();
    }
}