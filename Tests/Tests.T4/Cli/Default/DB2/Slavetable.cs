// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB.Mapping;

#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.Default.DB2
{
	[Table("SLAVETABLE")]
	public class Slavetable
	{
		[Column("ID1"                          )] public int Id1                        { get; set; } // INTEGER
		[Column("ID 2222222222222222222222  22")] public int Id222222222222222222222222 { get; set; } // INTEGER
		[Column("ID 2222222222222222"          )] public int Id2222222222222222         { get; set; } // INTEGER

		#region Associations
		/// <summary>
		/// FK_SLAVETABLE_MASTERTABLE
		/// </summary>
		[Association(CanBeNull = false, ThisKey = nameof(Id222222222222222222222222) + "," + nameof(Id1), OtherKey = nameof(DB2.Mastertable.Id1) + "," + nameof(DB2.Mastertable.Id2))]
		public Mastertable Mastertable { get; set; } = null!;
		#endregion
	}
}
