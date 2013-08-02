using System;
using Helligdage.NET.Model.Enums;

namespace Helligdage.NET.Model.Domain
{
	public class Holiday
	{
		public Holiday(DateTime date, HolidayType holidayType)
		{
			this.Date = date;
			this.HolidayType = holidayType;

			this.Name = this.HolidayType.ToString();
		}

		public DateTime Date { get; set; }
		public string Name { get; set; }
		public HolidayType HolidayType { get; set; }

		public override string ToString()
		{
			return string.Format("{0} ({1:dd/MM-yyyy})", this.Name, this.Date);
		}
	}
}
