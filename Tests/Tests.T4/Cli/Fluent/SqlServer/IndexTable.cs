// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------


#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.Fluent.SqlServer
{
	public class IndexTable
	{
		public int PkField1    { get; set; } // int
		public int PkField2    { get; set; } // int
		public int UniqueField { get; set; } // int
		public int IndexField  { get; set; } // int

		#region Associations
		/// <summary>
		/// FK_Patient2_IndexTable backreference
		/// </summary>
		public IndexTable2? IndexTable2 { get; set; }
		#endregion
	}
}
