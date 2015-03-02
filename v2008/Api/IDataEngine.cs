// ======================================================== IDataEngine.cs
namespace Kerosene.ORM.SqlServer.v2008
{
	using Kerosene.Tools;
	using System;
	using System.Collections.Generic;
	using System.Data.SqlClient;

	// ==================================================== 
	/// <summary>
	/// Represents an underlying SQL SERVER 2008 database engine, maintaining its main
	/// characteristics and acting as a factory to create objects adapted to it.
	/// </summary>
	public interface IDataEngine : Direct.IDataEngine
	{
		/// <summary>
		/// Returns a new instance that is a copy of the original one.
		/// </summary>
		/// <returns>A new instance.</returns>
		new IDataEngine Clone();

		/// <summary>
		/// Returns a new instance that is a copy of the original one.
		/// </summary>
		/// <param name="settings">A dictionary containing the names of the properties whose
		/// values are to be changed with respect to the original instance, or null to not
		/// modify any of those.</param>
		/// <returns>A new instance.</returns>
		new IDataEngine Clone(IDictionary<string, object> settings);

		/// <summary>
		/// Factory method to create a new parser adapted to this instance.
		/// </summary>
		/// <returns>A new parser.</returns>
		new IParser CreateParser();

		/// <summary>
		/// Gets the provider factory associated with this engine.
		/// </summary>
		new SqlClientFactory ProviderFactory { get; }
	}
}
// ======================================================== 
