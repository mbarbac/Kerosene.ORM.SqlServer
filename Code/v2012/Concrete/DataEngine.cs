using Kerosene.Tools;
using System;
using System.Collections.Generic;

namespace Kerosene.ORM.SqlServer.v2012.Concrete
{
	// ==================================================== 
	/// <summary>
	/// Represents an underlying SQL SERVER 2012 database engine, maintaining its main
	/// characteristics and acting as a factory to create objects adapted to it.
	/// </summary>
	public class DataEngine : v2008.Concrete.DataEngine, IDataEngine
	{
		const string SQLSERVER_2012_VERSION = "11";
		const bool SQLSERVER_2012_SUPPORT_NATIVE_SKIP_TAKE = true;
		const bool SQLSERVER_2012_SUPPORT_NATIVE_CTE = true;

		/// <summary>
		/// Initializes a new engine.
		/// </summary>
		public DataEngine()
			: base()
		{
			ServerVersion = SQLSERVER_2012_VERSION;
			SupportsNativeSkipTake = SQLSERVER_2012_SUPPORT_NATIVE_SKIP_TAKE;
		}

		/// <summary>
		/// Invoked to obtain a string with identification of this string for representation
		/// purposes.
		/// </summary>
		protected override string ToStringType()
		{
			return "SqlServerEngine2012";
		}

		/// <summary>
		/// Returns a new instance that is a copy of the original one.
		/// </summary>
		/// <returns>A new instance.</returns>
		public new DataEngine Clone()
		{
			if (IsDisposed) throw new ObjectDisposedException(this.ToString());
			var cloned = new DataEngine(); OnClone(cloned, null);
			return cloned;
		}
		IDataEngine IDataEngine.Clone()
		{
			return this.Clone();
		}
		v2008.IDataEngine v2008.IDataEngine.Clone()
		{
			return this.Clone();
		}
		Direct.IDataEngine Direct.IDataEngine.Clone()
		{
			return this.Clone();
		}
		Core.IDataEngine Core.IDataEngine.Clone()
		{
			return this.Clone();
		}
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>
		/// Returns a new instance that is a copy of the original one.
		/// </summary>
		/// <param name="settings">A dictionary containing the names of the properties whose
		/// values are to be changed with respect to the original instance, or null to not
		/// modify any of those.</param>
		/// <returns>A new instance.</returns>
		public new DataEngine Clone(IDictionary<string, object> settings)
		{
			if (IsDisposed) throw new ObjectDisposedException(this.ToString());
			var cloned = new DataEngine(); OnClone(cloned, settings);
			return cloned;
		}
		IDataEngine IDataEngine.Clone(IDictionary<string, object> settings)
		{
			return this.Clone(settings);
		}
		v2008.IDataEngine v2008.IDataEngine.Clone(IDictionary<string, object> settings)
		{
			return this.Clone(settings);
		}
		Direct.IDataEngine Direct.IDataEngine.Clone(IDictionary<string, object> settings)
		{
			return this.Clone(settings);
		}
		Core.IDataEngine Core.IDataEngine.Clone(IDictionary<string, object> settings)
		{
			return this.Clone(settings);
		}

		/// <summary>
		/// Invoked to create a parser associated with this instance.
		/// </summary>
		protected override Core.IParser CreateParser()
		{
			return new Parser(this);
		}

		/// <summary>
		/// Creates a new query command adapted to this engine.
		/// </summary>
		/// <param name="link">The link associated with the new command.</param>
		/// <returns>The new command.</returns>
		public new QueryCommand CreateQueryCommand(Core.IDataLink link)
		{
			return new QueryCommand(link);
		}
		IQueryCommand IDataEngine.CreateQueryCommand(Core.IDataLink link)
		{
			return this.CreateQueryCommand(link);
		}
		Core.IQueryCommand Core.IDataEngine.CreateQueryCommand(Core.IDataLink link)
		{
			return this.CreateQueryCommand(link);
		}
	}
}
