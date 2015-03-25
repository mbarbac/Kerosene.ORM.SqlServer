// ======================================================== QueryCommand.cs
namespace Kerosene.ORM.SqlServer.v2012.Concrete
{
	using Kerosene.Tools;
	using System;

	// ==================================================== 
	/// <summary>
	/// Represents a query operation against the underlying database.
	/// </summary>
	public class QueryCommand : Core.Concrete.QueryCommand, IQueryCommand
	{
		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// <param name="link">The link this instance will be associated with.</param>
		public QueryCommand(Core.IDataLink link)
			: base(link)
		{
			var engine = link.Engine as IDataEngine;
			if (engine == null) throw new InvalidOperationException(
				"Engine '{0}' of link '{1}' is not a valid SQL Server 2012 compatible one."
				.FormatWith(link.Engine, link));
		}

		/// <summary>
		/// Returns a new instance that is a copy of the original one.
		/// </summary>
		/// <returns>A new instance.</returns>
		public new QueryCommand Clone()
		{
			if (IsDisposed) throw new ObjectDisposedException(this.ToString());
			var cloned = new QueryCommand(Link);
			OnClone(cloned); return cloned;
		}
		IQueryCommand IQueryCommand.Clone()
		{
			return this.Clone();
		}
		Core.IQueryCommand Core.IQueryCommand.Clone()
		{
			return this.Clone();
		}
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>
		/// Generates a string containing the command to be executed on the underlying database.
		/// <para>The text returned might be incomplete and should not be used until the value of
		/// the '<see cref="CanBeExecuted"/>' property is true.</para>
		/// </summary>
		/// <param name="iterable">True to generate the iterable version, false to generate the
		/// scalar one.</param>
		/// <returns>The requested command string.</returns>
		/// <remarks>This method must not throw an exception if this instance is disposed.</remarks>
		public override string GetCommandText(bool iterable)
		{
			var str = base.GetCommandText(iterable);

			if (str != null && IsValidForNativeSkipTake())
			{
				var old = TheOrderByData;

				string temp = string.Format("{0} OFFSET {1} ROWS", old, TheSkipData > 0 ? TheSkipData : 0);
				if (TheTakeData > 1) temp = string.Format("{0} FETCH NEXT {1} ROWS ONLY", temp, TheTakeData);

				TheOrderByData = temp;
				str = base.GetCommandText(iterable);
				TheOrderByData = old;
			}

			return str;
		}

		/// <summary>
		/// Defines the contents of the SELECT clause or append the new ones to any previous
		/// specification.
		/// </summary>
		/// <param name="selects">The collection of lambda expressions that resolve into the
		/// elements to include into this clause:
		/// <para>- A string, as in 'x => "name AS alias"', where the alias part is optional.</para>
		/// <para>- A table and column specification, as in 'x => x.Table.Column.As(alias)', where
		/// both the table and alias parts are optional.</para>
		/// <para>- A specification for all columns of a table using the 'x => x.Table.All()' syntax.</para>
		/// <para>- Any expression that can be parsed into a valid SQL sentence for this clause.</para>
		/// </param>
		/// <returns>A self-reference to permit a fluent syntax chaining.</returns>
		public new QueryCommand Select(params Func<dynamic, object>[] selects)
		{
			base.Select(selects); return this;
		}
		IQueryCommand IQueryCommand.Select(params Func<dynamic, object>[] selects)
		{
			return this.Select(selects);
		}
		Core.IQueryCommand Core.IQueryCommand.Select(params Func<dynamic, object>[] selects)
		{
			return this.Select(selects);
		}

		/// <summary>
		/// Adds or removes a DISTINCT clause to the SELECT one of this command.
		/// </summary>
		/// <param name="distinct">True to add this clause, false to remove it.</param>
		/// <returns>A self-reference to permit a fluent syntax chaining.</returns>
		public new QueryCommand Distinct(bool distinct = true)
		{
			base.Distinct(distinct); return this;
		}
		IQueryCommand IQueryCommand.Distinct(bool distinct)
		{
			return this.Distinct(distinct);
		}
		Core.IQueryCommand Core.IQueryCommand.Distinct(bool distinct)
		{
			return this.Distinct(distinct);
		}

		/// <summary>
		/// Defines the contents of the TOP clause. Any previous ones are removed.
		/// </summary>
		/// <param name="top">An integer with the value to set for the TOP clause. A value of cero
		/// or negative merely removes this clause.</param>
		/// <returns>A self-reference to permit a fluent syntax chaining.</returns>
		public new QueryCommand Top(int top)
		{
			base.Top(top); return this;
		}
		IQueryCommand IQueryCommand.Top(int top)
		{
			return this.Top(top);
		}
		Core.IQueryCommand Core.IQueryCommand.Top(int top)
		{
			return this.Top(top);
		}

		/// <summary>
		/// Defines the contents of the FROM clause or append the new ones to any previous
		/// specification.
		/// </summary>
		/// <param name="froms">The collection of lambda expressions that resolve into the
		/// elements to include in this clause:
		/// <para>- A string, as in 'x => "name AS alias"', where the alias part is optional.</para>
		/// <para>- A table specification, as in 'x => x.Table.As(alias)', where both the alias part
		/// is optional.</para>
		/// <para>- Any expression that can be parsed into a valid SQL sentence for this clause.</para>
		/// </param>
		/// <returns>A self-reference to permit a fluent syntax chaining.</returns>
		public new QueryCommand From(params Func<dynamic, object>[] froms)
		{
			base.From(froms); return this;
		}
		IQueryCommand IQueryCommand.From(params Func<dynamic, object>[] froms)
		{
			return this.From(froms);
		}
		Core.IQueryCommand Core.IQueryCommand.From(params Func<dynamic, object>[] froms)
		{
			return this.From(froms);
		}

		/// <summary>
		/// Defines the contents of the WHERE clause or append the new ones to any previous
		/// specification.
		/// <para>By default if any previous contents exist the new ones are appended using an AND
		/// operator. However, the virtual extension methods 'x => x.And(...)' and 'x => x.Or(...)'
		/// can be used to specify what logical operator to use.</para>
		/// </summary>
		/// <param name="where">The dynamic lambda expression that resolves into the contents of
		/// this clause.</param>
		/// <returns>A self-reference to permit a fluent syntax chaining.</returns>
		public new QueryCommand Where(Func<dynamic, object> where)
		{
			base.Where(where); return this;
		}
		IQueryCommand IQueryCommand.Where(Func<dynamic, object> where)
		{
			return this.Where(where);
		}
		Core.IQueryCommand Core.IQueryCommand.Where(Func<dynamic, object> where)
		{
			return this.Where(where);
		}

		/// <summary>
		/// Defines the contents of the JOIN clause or append the new ones to any previous
		/// specification.
		/// </summary>
		/// <param name="join">The dynamic lambda expression that resolves into the contents of
		/// this clause:
		/// <para>- A string, as in 'x => "jointype table AS alias ON condition"', where both the
		/// jointype and the alias parts are optional. If no jointype is used then a default JOIN
		/// one is used.</para>
		/// <para>- A dynamic specification as in 'x => x.Table.As(Alias).On(condition)' where
		/// the alias part is optional.</para>
		/// <para>- A dynamic specification containing a non-default join operation can be
		/// specified using the 'x => x(jointype).Table...' syntax, where the orphan invocation
		/// must be the first one in the chain, and whose parameter is a string containing the
		/// join clause to use.</para>
		/// <returns>A self-reference to permit a fluent syntax chaining.</returns>
		public new QueryCommand Join(Func<dynamic, object> join)
		{
			base.Join(join); return this;
		}
		IQueryCommand IQueryCommand.Join(Func<dynamic, object> join)
		{
			return this.Join(join);
		}
		Core.IQueryCommand Core.IQueryCommand.Join(Func<dynamic, object> join)
		{
			return this.Join(join);
		}

		/// <summary>
		/// Defines the contents of the GROUP BY clause or append the new ones to any previous
		/// specification.
		/// </summary>
		/// <param name="groupbys">The collection of dynamic lambda expressions that resolve into
		/// the contents of this clause:
		/// <para>- A string as in 'x => "Table.Column"', where the table part is optional.</para>
		/// </param>
		/// <returns>A self-reference to permit a fluent syntax chaining.</returns>
		public new QueryCommand GroupBy(params Func<dynamic, object>[] groupbys)
		{
			base.GroupBy(groupbys); return this;
		}
		IQueryCommand IQueryCommand.GroupBy(params Func<dynamic, object>[] groupbys)
		{
			return this.GroupBy(groupbys);
		}
		Core.IQueryCommand Core.IQueryCommand.GroupBy(params Func<dynamic, object>[] groupbys)
		{
			return this.GroupBy(groupbys);
		}

		/// <summary>
		/// Defines the contents of the HAVING clause that follows the GROUP BY one, or append the
		/// new ones to any previous specification.
		/// <para>By default if any previous contents exist the new ones are appended using an AND
		/// operator. However, the virtual extension methods 'x => x.And(...)' and 'x => x.Or(...)'
		/// can be used to specify what logical operator to use.</para>
		/// </summary>
		/// <param name="having">The dynamic lambda expression that resolves into the contents of
		/// this clause.</param>
		/// <returns>A self-reference to permit a fluent syntax chaining.</returns>
		public new QueryCommand Having(Func<dynamic, object> having)
		{
			base.Having(having); return this;
		}
		IQueryCommand IQueryCommand.Having(Func<dynamic, object> having)
		{
			return this.Having(having);
		}
		Core.IQueryCommand Core.IQueryCommand.Having(Func<dynamic, object> having)
		{
			return this.Having(having);
		}

		/// <summary>
		/// Defines the contents of the ORDER BY clause or append the new ones to any previous
		/// specification.
		/// </summary>
		/// <param name="orderbys">The collection of dynamic lambda expressions that resolve into
		/// the contents of this clause:
		/// <para>- A string as in 'x => x.Table.Column ORDER' where both the table and order
		/// parts are optional. If no order part is present then a default ASCENDING one is used.</para>
		/// <para>- A string as in 'x => "Table.Column.Order()"', where both the table and order
		/// parts are optional. The order part can be any among the 'Asc()', 'Ascending()',
		/// 'Desc()' or 'Descending()' ones.</para>
		/// </param>
		/// <returns>A self-reference to permit a fluent syntax chaining.</returns>
		public new QueryCommand OrderBy(params Func<dynamic, object>[] orderbys)
		{
			base.OrderBy(orderbys); return this;
		}
		IQueryCommand IQueryCommand.OrderBy(params Func<dynamic, object>[] orderbys)
		{
			return this.OrderBy(orderbys);
		}
		Core.IQueryCommand Core.IQueryCommand.OrderBy(params Func<dynamic, object>[] orderbys)
		{
			return this.OrderBy(orderbys);
		}

		/// <summary>
		/// Defines the contents of the SKIP clause. Any previous ones are removed.
		/// </summary>
		/// <param name="skip">An integer with the value to set for the SKIP clause. A value of cero
		/// or negative merely removes this clause.</param>
		/// <returns>A self-reference to permit a fluent syntax chaining.</returns>
		public new QueryCommand Skip(int skip)
		{
			base.Skip(skip); return this;
		}
		IQueryCommand IQueryCommand.Skip(int skip)
		{
			return this.Skip(skip);
		}
		Core.IQueryCommand Core.IQueryCommand.Skip(int skip)
		{
			return this.Skip(skip);
		}

		/// <summary>
		/// Defines the contents of the TAKE clause. Any previous ones are removed.
		/// </summary>
		/// <param name="take">An integer with the value to set for the TAKE clause. A value of cero
		/// or negative merely removes this clause.</param>
		/// <returns>A self-reference to permit a fluent syntax chaining.</returns>
		public new QueryCommand Take(int take)
		{
			base.Take(take); return this;
		}
		IQueryCommand IQueryCommand.Take(int take)
		{
			return this.Take(take);
		}
		Core.IQueryCommand Core.IQueryCommand.Take(int take)
		{
			return this.Take(take);
		}

		/// <summary>
		/// Gets whether the current state of the command is valid for a native Skip/Take
		/// implementation. If not it will be emulated by software.
		/// </summary>
		public override bool IsValidForNativeSkipTake()
		{
			if (IsDisposed) return false;
			if (Link.IsDisposed) return false;
			if (Link.Engine.IsDisposed) return false;

			if (!Link.Engine.SupportsNativeSkipTake) return false;

			if (TheSkipData >= 0 && TheTakeData >= 1 && TheOrderByData != null) return true;
			return false;
		}
	}
}
// ======================================================== 
