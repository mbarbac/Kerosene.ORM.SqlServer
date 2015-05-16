using Kerosene.Tools;
using System;

namespace Kerosene.ORM.SqlServer.v2008.Concrete
{
	// ==================================================== 
	/// <summary>
	/// Represents an object able to parse an arbitrary object, or arbitrary logic expressed as
	/// a dynamic lambda expression, extracting and capturing the relevant arguments, and to
	/// return a string that contains the result of that parsing in a syntax understood by the
	/// underlying database service.
	/// </summary>
	public class Parser : Core.Concrete.Parser, IParser
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
		Core.IDataEngine Core.IParser.Engine
		{
			get { return this.Engine; }
		}

		/// <summary>
		/// Parses a method invocation.
		/// This method is used to implement the virtual extensions feature, including:
		/// <para>Argument level:</para>
		/// <para>- x.Not(...) => (NOT ...)</para>
		/// <para>- x.Distinct(expression) => DISTINCT expression</para>
		/// <para>Element level:</para>
		/// <para>- x.Element.As(name) => Element AS name</para>
		/// <para>- x.Element.In(arg, ...) => Element IN (arg, ...)</para>
		/// <para>- x.Element.NotIn(arg, ...) => NOT Element IN (arg, ...)</para>
		/// <para>- x.Element.Between(arg1, arg2) => Element BETWEEN arg1 AND arg2</para>
		/// <para>- x.Element.Like(arg) => Element LIKE arg</para>
		/// <para>- x.Element.NotLike(arg) => Element NOT LIKE arg</para>
		/// <para>Default case:</para>
		/// <para>- The default case where the name of the method and its arguments are parsed as-is.</para>
		/// <para>This overriden method adds the following ones:</para>
		/// <para>- x.Element.Cast(type)</para>
		/// <para>- x.Element.Left(n), x.Element.Right(n)</para>
		/// <para>- x.Element.Len(), x.Element.Lower(), x.Element.Upper()</para>
		/// <para>- x.Element.Year(), x.Element.Month(), x.Element.Day()</para>
		/// <para>- x.Element.Hour(), x.Element.Minute(), x.Element.Second(), x.Element.Millisecond()</para>
		/// <para>- x.Element.Offset()</para>
		/// <para>- x.Element.Contains(item), x.Element.Patindex(num), x.Element.Substring(start,len)</para>
		/// <para>- x.Element.Trim(), x.Element.Rtrim(), x.Element.Rtrim()</para>
		/// </summary>
		protected override string OnParseMethod(DynamicNode.Method obj, Core.IParameterCollection pars, bool nulls)
		{
			string name = obj.Name.ToUpper();
			string parent = obj.Host == null ? null : Parse(obj.Host, pars, nulls);
			string item = null;
			string extra = null;

			// Root-level methods...
			if (obj.Host == null)
			{
				switch (name)
				{
					case "CAST":
						if (obj.Arguments == null) throw new ArgumentException("CAST() argument list is null.");
						if (obj.Arguments.Length != 2) throw new ArgumentException("CAST() requires two arguments.");
						item = Parse(obj.Arguments[0], pars, nulls);
						extra = Parse(obj.Arguments[1], pars, nulls);
						return string.Format("CAST({0} AS {1})", item, extra);
				}
			}

			// Item-level methods...
			if (obj.Host != null)
			{
				switch (name)
				{
					case "LEFT":
						if (obj.Arguments == null) throw new ArgumentException("LEFT() argument list is null.");
						if (obj.Arguments.Length != 1) throw new ArgumentException("LEFT() requires one argument.");
						item = Parse(obj.Arguments[0], pars, nulls);
						return string.Format("LEFT({0}, {1})", parent, item);
					case "RIGHT":
						if (obj.Arguments == null) throw new ArgumentException("RIGHT() argument list is null.");
						if (obj.Arguments.Length != 1) throw new ArgumentException("RIGHT() requires one argument.");
						item = Parse(obj.Arguments[0], pars, nulls);
						return string.Format("RIGHT({0}, {1})", parent, item);

					case "LEN":
						if (obj.Arguments != null) throw new ArgumentException("LEN() shall be a parameterless method.");
						return string.Format("LEN({0})", parent);
					case "LOWER":
						if (obj.Arguments != null) throw new ArgumentException("LOWER() shall be a parameterless method.");
						return string.Format("LOWER({0})", parent);
					case "UPPER":
						if (obj.Arguments != null) throw new ArgumentException("UPPER() shall be a parameterless method.");
						return string.Format("UPPER({0})", parent);

					case "YEAR":
						if (obj.Arguments != null) throw new ArgumentException("YEAR() shall be a parameterless method.");
						return string.Format("DATEPART(YEAR, {0})", parent);
					case "MONTH":
						if (obj.Arguments != null) throw new ArgumentException("MONTH() shall be a parameterless method.");
						return string.Format("DATEPART(MONTH, {0})", parent);
					case "DAY":
						if (obj.Arguments != null) throw new ArgumentException("DAY() shall be a parameterless method.");
						return string.Format("DATEPART(DAY, {0})", parent);

					case "HOUR":
						if (obj.Arguments != null) throw new ArgumentException("HOUR() shall be a parameterless method.");
						return string.Format("DATEPART(HOUR, {0})", parent);
					case "MINUTE":
						if (obj.Arguments != null) throw new ArgumentException("MINUTE() shall be a parameterless method.");
						return string.Format("DATEPART(MINUTE, {0})", parent);
					case "SECOND":
						if (obj.Arguments != null) throw new ArgumentException("SECOND() shall be a parameterless method.");
						return string.Format("SECOND(DAY, {0})", parent);
					case "MILLISECOND":
						if (obj.Arguments != null) throw new ArgumentException("MILLISECOND() shall be a parameterless method.");
						return string.Format("DATEPART(MILLISECOND, {0})", parent);

					case "OFFSET":
						if (obj.Arguments != null) throw new ArgumentException("OFFSET() shall be a parameterless method.");
						return string.Format("DATEPART(TZ, {0})", parent);

					case "CONTAINS":
						if (obj.Arguments == null) throw new ArgumentException("CONTAINS() argument list is null.");
						if (obj.Arguments.Length != 1) throw new ArgumentException("CONTAINS() requires just one argument.");
						item = Parse(obj.Arguments[0], pars, nulls);
						return string.Format("CONTAINS({0}, {1})", parent, item);
					case "PATINDEX":
						if (obj.Arguments == null) throw new ArgumentException("PATINDEX() argument list is null.");
						if (obj.Arguments.Length != 1) throw new ArgumentException("PATINDEX() requires just one argument.");
						item = Parse(obj.Arguments[0], pars, nulls);
						return string.Format("PATINDEX({1}, {0})", parent, item); // Beware, indexes inverted!
					case "SUBSTRING":
						if (obj.Arguments == null) throw new ArgumentException("SUBSTRING() argument list is null.");
						if (obj.Arguments.Length != 2) throw new ArgumentException("SUBSTRING() requires two arguments.");
						item = Parse(obj.Arguments[0], pars, nulls);
						extra = Parse(obj.Arguments[1], pars, nulls);
						return string.Format("SUBSTRING({0}, {1}, {2})", parent, item, extra);

					case "LTRIM":
						if (obj.Arguments != null) throw new ArgumentException("LTRIM() is a parameterless method.");
						return string.Format("LTRIM({0})", parent);
					case "RTRIM":
						if (obj.Arguments != null) throw new ArgumentException("RTRIM() is a parameterless method.");
						return string.Format("RTRIM({0})", parent);
					case "TRIM":
						if (obj.Arguments != null) throw new ArgumentException("TRIM() is a parameterless method.");
						return string.Format("LTRIM(RTRIM({0}))", parent);
				}
			}

			// Reverting to whatever the base class intercepts...
			return base.OnParseMethod(obj, pars, nulls);
		}
	}
}
