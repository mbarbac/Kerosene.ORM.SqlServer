namespace Kerosene.ORM.SqlServer.v2012
{
	using Kerosene.Tools;
	using System;

	// ==================================================== 
	/// <summary>
	/// Represents an object able to parse an arbitrary object, or arbitrary logic expressed as
	/// a dynamic lambda expression, extracting and capturing the relevant arguments, and to
	/// return a string that contains the result of that parsing in a syntax understood by the
	/// underlying database service.
	/// </summary>
	public interface IParser : v2008.IParser
	{
		/// <summary>
		/// The engine this parser is associated with, that maintains the main characteristics
		/// of the underlying database engine.
		/// </summary>
		new IDataEngine Engine { get; }
	}
}
