using Azure.Data.Tables;
using System.Xml;

namespace StargateAPI_DAL
{

    public interface IRepository<T> where T : class
    {
        Task AddAsync(T item);
        Task UpdateAsync(T item);
        //Task Remove(T item); Removing for now. Initially will be using soft delete 
        Task<List<T>> GetAllEntitiesAsync<T>() where T : class, ITableEntity, new();
        Task<T> GetSingleEntityAsync<T>(Func<T, bool> predicate) where T : class, ITableEntity, new();

    };

    public class TableClientRepository<T> : IRepository<T> where T : class, ITableEntity
    {
        public TableClient _tableClient;
        public TableClientRepository(TableServiceClient tableServiceClient)
        {
            _tableClient = tableServiceClient.GetTableClient(typeof(T).Name);
            _tableClient.CreateIfNotExistsAsync();
        }

        public async Task<List<T>> GetAllEntitiesAsync<T>() where T : class, ITableEntity, new()
        {
            return await _tableClient.QueryAsync<T>(maxPerPage: 1000).ToListAsync().ConfigureAwait(false);
        }
        public async Task<T> GetSingleEntityAsync<T>(Func<T,bool> predicate) where T : class, ITableEntity, new()
        {
            return await _tableClient.QueryAsync<T>(maxPerPage: 1000).Where(predicate).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task AddAsync(T item)
        {

            var ent = new TableEntity(item.PartitionKey, Guid.NewGuid().ToString())// PartionKey/RowKey here needs some work for if large data.
            {{ typeof(T).Name,item }};


            await _tableClient.AddEntityAsync(item);
        }

        public async Task UpdateAsync(T item)
        {
            await _tableClient.UpdateEntityAsync(item, Azure.ETag.All, TableUpdateMode.Merge);
        }

        public async Task Remove(T item)
        {
            await _tableClient.DeleteEntityAsync(item.PartitionKey, item.RowKey);
        }
    }
}

