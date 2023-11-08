using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelDatabase.Entities {
	public class User {
		public int Id { get; set; } //PK
		public string? Name { get; set; }
		public Capital? City {	get; set; } //Navigation property
		public int CityId {	get; set; } //FK to Capital
		public int Admin { get; set; }
		public ICollection<Trip>? Trip { get; set; } //Navigation property
	}
}
