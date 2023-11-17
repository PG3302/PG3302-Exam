using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.Diagnostics;
using TravelDatabase.DataAccess.SqLite;
using TravelDatabase.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using TravelDatabase.Repositories;

namespace TravelDatabase
{
    public class Program {
		static void Main() {
			InitDatabase.InitFromCsv();
		}
	}
}
