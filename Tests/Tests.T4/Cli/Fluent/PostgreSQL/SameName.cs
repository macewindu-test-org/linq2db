// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using System.Collections.Generic;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.Fluent.PostgreSQL
{
	public class SameName
	{
		public int Id { get; set; } // integer

		#region Associations
		/// <summary>
		/// same_name backreference
		/// </summary>
		public IEnumerable<SameName1> SameNames1 { get; set; } = null!;

		/// <summary>
		/// same_name backreference
		/// </summary>
		public IEnumerable<SameName2> SameNames2 { get; set; } = null!;
		#endregion
	}
}
