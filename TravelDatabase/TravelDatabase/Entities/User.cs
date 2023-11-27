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
		public string? Email { get; set; }
		public int Admin { get; set; }
		public ICollection<Trip>? Trip { get; set; } //Navigation property
	}
}
