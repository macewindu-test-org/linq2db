// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using System.Collections.Generic;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.Fluent.Informix
{
	public class Testunique
	{
		public int Id1 { get; set; } // INTEGER
		public int Id2 { get; set; } // INTEGER
		public int Id3 { get; set; } // INTEGER
		public int Id4 { get; set; } // INTEGER

		#region Associations
		/// <summary>
		/// FK_testfkunique_testunique backreference
		/// </summary>
		public IEnumerable<Testfkunique> Testfkuniques { get; set; } = null!;

		/// <summary>
		/// FK_testfkunique_testunique_1 backreference
		/// </summary>
		public IEnumerable<Testfkunique> Testfkuniques1 { get; set; } = null!;
		#endregion
	}
}
