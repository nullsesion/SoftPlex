﻿namespace SoftPlex.DataAccess.Entities
{
	public class FilterEngineEntity
	{
		public Guid Id { get; set; }
		public string ProductName { get; set; }
		public string ProductVersionName { get; set; }
		public decimal Width { get; set; }
		public decimal Height { get; set; }
		public decimal Length { get; set; }
	}
}
