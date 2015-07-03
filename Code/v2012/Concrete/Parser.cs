using Kerosene.Tools;
using System;

namespace Kerosene.ORM.SqlServer.v2012.Concrete
{
	// ==================================================== 
	/// <summary>
	/// Represents an object able to parse an arbitrary object, or arbitrary logic expressed as
	/// a dynamic lambda expression, extracting and capturing the relevant arguments, and to
	/// return a string that contains the result of that parsing in a syntax understood by the
	/// underlying database service.
	/// </summary>
	public class Parser : v2008.Concrete.Parser, IParser
	{
		/// <summary>
		/// Initializes a new instance.
		/// </summary>
		/// <param name="engine">The engine this instance will be associated with.</param>
		public Parser(IDataEngine engine) : base(engine) { }

		/// <summary>
		/// The engine this parser is associated with, that maintains the main characteristics
		/// of the underlying database engine.
		/// </summary>
		public new DataEngine Engine
		{
			get { return (DataEngine)base.Engine; }
		}
		IDataEngine IParser.Engine
		{
			get { return this.Engine; }
		}
		v2008.IDataEngine v2008.IParser.Engine
		{
			get { return this.Engine; }
		}
		Core.IDataEngine Core.IParser.Engine
		{
			get { return this.Engine; }
		}
	}
}
