// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------


#pragma warning disable 1573, 1591
#nullable enable

namespace Cli.NewCliFeatures.FluentSQLite
{
	public class InheritanceChild
	{
		public long    InheritanceChildId  { get; set; } // integer
		public long    InheritanceParentId { get; set; } // integer
		public long?   TypeDiscriminator   { get; set; } // integer
		public string? Name                { get; set; } // nvarchar(50)
	}
}
