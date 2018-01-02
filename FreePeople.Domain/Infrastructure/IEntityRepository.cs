using System;
using System.Collections.Generic;
using System.Text;
using Optional;

namespace FreePeople.Domain.Infrastructure
{
	public interface IEntityRepository<TEntity, in TKey>
		where TEntity : class
		where TKey : IEquatable<TKey>
	{
		IReadOnlyCollection<TEntity> List();
		TEntity GetByKey(TKey id);
		Option<TEntity> FindByKey(TKey id);
		void Delete(TEntity item);
		void DeleteByKey(TKey id);
		TEntity SaveOrUpdate(TEntity item);
	}
}
