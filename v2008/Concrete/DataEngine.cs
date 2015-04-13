// ======================================================== DataEngine.cs
namespace Kerosene.ORM.SqlServer.v2008.Concrete
{
	using Kerosene.Tools;
	using System;
	using System.Collections.Generic;

	// ==================================================== 
	/// <summary>
	/// Represents an underlying SQL SERVER 2008 database engine, maintaining its main
	/// characteristics and acting as a factory to create objects adapted to it.
	/// </summary>
	public class DataEngine : Direct.Concrete.SqlServerEngine, IDataEngine
	{
		const string SQLSERVER_2008_VERSION = "10";
		const bool SQLSERVER_2008_SUPPORT_NATIVE_CTE = true;

		/// <summary>
		/// Initializes a new engine.
		/// </summary>
		public DataEngine()
			: base()
		{
			ServerVersion = SQLSERVER_2008_VERSION;
		}

		/// <summary>
		/// Invoked to obtain a string with identification of this string for representation
		/// purposes.
		/// </summary>
		protected override string ToStringType()
		{
			return "SqlServerEngine2008";
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
	}
}
// ======================================================== 
