using Kerosene.Tools;
using System;

namespace Kerosene.ORM.SqlServer.v2008
{
	// ==================================================== 
	/// <summary>
	/// Represents an object able to parse an arbitrary object, including any arbitrary command
	/// logic expressed as a dynamic lambda expression, extracting and capturing the arguments
	/// found in it, returning a string that can be understood by the underlying database engine.
	/// </summary>
	public interface IParser : Core.IParser
	{
		/// <summary>
		/// The engine this parser is associated with, that maintains the main characteristics
		/// of the underlying database engine.
		/// </summary>
		new IDataEngine Engine { get; }
	}
}
