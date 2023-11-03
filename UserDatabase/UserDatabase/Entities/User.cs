using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDatabase.Entities {
	public class User {
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? City { get; set; }
		public int Admin { get; set; }
		public ICollection<Trip>? Trip { get; set; }
		public override string ToString() {
			return $"Id:{Id} | Name: {Name}";
		}
	}
}
