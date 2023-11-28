using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelDatabase.Entities {
	public class Trip {
		public int Id { get; set; } //PK
		public User? User {	get; set; } //Navigation Property
		public int UserId { get; set; }
		public Capital? DepartureCapital { get; set; }
		public int DepartureCapitalId { get; set; } //FK
		public Capital? ArrivalCapital { get; set; }
		public int ArrivalCapitalId { get; set; } //FK
		public override string ToString()
		{
			return $"Id: {Id}, Set by user: (Name = {User.Name}, Id = {UserId}),";
		}
	}
}
