using System;
using System.Collections.Generic;

namespace FreePeople.Domain
{
	public interface IEntity<out T>
	{
		T Id { get; }
	}

	public abstract class Entity<T> : IEntity<T>, IEquatable<Entity<T>>
	{
		protected Entity(T id)
		{
			Id = id;
		}

		public T Id { get; }

		public bool Equals(Entity<T> other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return EqualityComparer<T>.Default.Equals(Id, other.Id);
		}

		public override string ToString()
		{
			var id = Id.ToString();
			var last = Math.Max(id.Length - 4, 0);
			return $"{GetType().Name}#{id.Substring(last)}";
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((Entity<T>) obj);
		}

		public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Id);
		public static bool operator ==(Entity<T> left, Entity<T> right) => Equals(left, right);
		public static bool operator !=(Entity<T> left, Entity<T> right) => !Equals(left, right);
	}
}
