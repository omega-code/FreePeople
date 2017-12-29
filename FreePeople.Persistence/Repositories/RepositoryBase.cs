using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Optional;
using Optional.Utilities;

namespace FreePeople.Persistence.Repositories
{
	public interface IFactory<TE, TDTO>
	{
		TE CreateEntity(TDTO dto);
		TDTO CreateDTO(TE e);
	}

	public abstract class RepositoryBase<TEntity, TKey, TDTO, TFactory>
		where TEntity : class
		where TDTO : class
		where TFactory : class, IFactory<TEntity, TDTO>
		where TKey : IEquatable<TKey>
	{
		private readonly string _keyPropertyName;
		protected readonly string ConnectionString;
		protected readonly TFactory Factory;
		protected readonly Func<TEntity, TKey> KeyGetter;

		protected RepositoryBase(string keyPropertyName, TFactory factory, string connectionString)
		{
			Contract.Requires(!string.IsNullOrWhiteSpace(keyPropertyName));
			Contract.Requires(factory != null);
			Contract.Requires(!string.IsNullOrWhiteSpace(connectionString));

			_keyPropertyName = keyPropertyName;
			KeyGetter = CreateKeyGetter().Compile();
			Factory = factory;
			ConnectionString = connectionString;
		}

		public virtual IReadOnlyCollection<TEntity> List()
		{
			return Query(_ => true);
		}

		protected int Count(Expression<Func<TDTO, bool>> filter) =>
			Execute(data => data.Count(filter));

		protected T Execute<T>(Func<DbSet<TDTO>, T> op)
		{
			using (var ctx = CreateContext())
			{
				return op(ctx.Set<TDTO>());
			}
		}

		protected IReadOnlyCollection<TEntity> Query(Expression<Func<TDTO, bool>> filter,
			Func<IQueryable<TDTO>, IQueryable<TDTO>> queryModifier = null)
		{
			if (queryModifier == null)
				queryModifier = src => src;

			using (var ctx = CreateContext())
			{
				return queryModifier(
						Include(ctx.Set<TDTO>())
							.Where(filter)
					)
					.ToArray()
					.Select(Factory.CreateEntity)
					.ToArray();
			}
		}

		protected Option<TEntity> Find(Expression<Func<TDTO, bool>> filter, Func<IQueryable<TDTO>, IQueryable<TDTO>> queryModifier = null) =>
			Query(filter, queryModifier)
				.FirstOrDefault()
				.SomeNotNull();

		public TEntity GetByKey(TKey key) => Optional.Unsafe.OptionUnsafeExtensions.ValueOrFailure(
			FindByKey(key),
			$"Not found {nameof(TEntity)} by '{key}'"
		);

		public Option<TEntity> FindByKey(TKey key)
		{
			using (var ctx = CreateContext())
			{
				return FindDTOByKey(ctx, key)
					.Map(Factory.CreateEntity);
			}
		}

		protected Option<TDTO> FindDTOByKey(DbContext ctx, TKey key) =>
			Include(ctx.Set<TDTO>())
				.FirstOrDefault(CreateKeyComparison(key))
				.SomeNotNull();

		protected DbContext CreateContext()
		{
			var opts = new DbContextOptionsBuilder<DataContext>();
			opts.UseSqlServer(ConnectionString);
			return new DataContext(opts.Options);
		}

		protected virtual IQueryable<TDTO> Include(IQueryable<TDTO> src) => src;

		private Expression<Func<TEntity, TKey>> CreateKeyGetter()
		{
			var p0 = Expression.Parameter(typeof(TEntity), "dto");
			return Expression.Lambda<Func<TEntity, TKey>>(Expression.Property(p0, _keyPropertyName), p0);
		}

		private Expression<Func<TDTO, bool>> CreateKeyComparison(TKey key)
		{
			var p0 = Expression.Parameter(typeof(TDTO), "dto");
			return Expression.Lambda<Func<TDTO, bool>>(
				Expression.Equal(
					Expression.Property(p0, _keyPropertyName),
					Expression.Constant(key)),
				p0);
		}

		public virtual TEntity SaveOrUpdate(TEntity item)
		{
			try
			{
				using (var ctx = CreateContext())
				{
					var newVersionDTO = Factory.CreateDTO(item);
					var key = KeyGetter(item);
					return FindDTOByKey(ctx, key)
						.Map(existingEntity =>
						{
							ctx.Entry(existingEntity).CurrentValues.SetValues(newVersionDTO);
							ctx.SaveChanges();
							return item;
						})
						.ValueOr(() =>
						{
							ctx.Set<TDTO>().Add(newVersionDTO);
							ctx.SaveChanges();
							return item;
						});
				}

			}
			catch (DbUpdateException ex)
			{
				if (ex.InnerException.SomeNotNull().HasValue)
					throw FindInner(ex);
				throw;
			}
		}

		public void Delete(TEntity item)
		{
			DeleteByKey(KeyGetter(item));
		}

		public void DeleteByKey(TKey key)
		{
			try
			{
				using (var ctx = CreateContext())
				{
					FindDTOByKey(ctx, key).MatchSome(existingEntity =>
					{
						OnDeleting(ctx, existingEntity);
						ctx.Entry(existingEntity).State = EntityState.Deleted;
						ctx.SaveChanges();
					});
				}

			}
			catch (DbUpdateException ex)
			{
				if (ex.InnerException.SomeNotNull().HasValue)
					throw FindInner(ex);
				throw;
			}
		}

		public bool ExistsByKey(TKey key)
		{
			using (var ctx = CreateContext())
			{
				var existingEntity = FindDTOByKey(ctx, key);
				return existingEntity.HasValue;
			}
		}

		protected virtual void OnDeleting(DbContext ctx, TDTO value)
		{
		}

		private static Exception FindInner(Exception ex)
		{
			var more = ex;
			while (more.InnerException.SomeNotNull().HasValue)
			{
				more = more.InnerException;
			}
			return more;
		}
	}
}
