using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserDatabase.Entities {
	public class Trip {
		public int Id { get; set; }
		public User? User {	get; set; }
		public int UserId { get; set; }
		public int DepartureId { get; set; }
		public int ArrivalId { get; set; }
		public override string ToString() {
			return $"Id: {Id} | User: {User}";
		}
	}
}
