using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Edelstein.Database.Utils
{
    public class QueuedDataBatch : IDataBatch
    {
        private readonly IDataSession _session;
        private readonly Queue<Action<IDataSession>> _queue;

        public QueuedDataBatch(IDataSession session)
        {
            _session = session;
            _queue = new Queue<Action<IDataSession>>();
        }

        public void Insert<T>(T entity) where T : class, IDataEntity
            => _queue.Enqueue(s => s.Insert(entity));

        public void Update<T>(T entity) where T : class, IDataEntity
            => _queue.Enqueue(s => s.Update(entity));

        public void Delete<T>(T entity) where T : class, IDataEntity
            => _queue.Enqueue(s => s.Delete(entity));

        public Task InsertAsync<T>(T entity) where T : class, IDataEntity
        {
            Insert<T>(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync<T>(T entity) where T : class, IDataEntity
        {
            Update<T>(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync<T>(T entity) where T : class, IDataEntity
        {
            Delete<T>(entity);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // Do nothing
        }

        public void SaveChanges()
        {
            foreach (var action in _queue)
                action.Invoke(_session);
        }

        public Task SaveChangesAsync()
            => Task.Run(SaveChanges);
    }
}