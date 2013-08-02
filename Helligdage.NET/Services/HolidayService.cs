using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helligdage.NET.Core;
using Helligdage.NET.Model.Domain;
using Helligdage.NET.Model.Enums;
using Helligdage.NET.Model.Interfaces;

namespace Helligdage.NET.Services
{
    public class HolidayService : IHolidayService
    {
	    private readonly Dictionary<int, List<Holiday>> _holidaysForYears = new Dictionary<int, List<Holiday>>();

		/// <summary>
		/// Gets all holidays for a given year. Optionally only returns holidays of a specified type.
		/// </summary>
		/// <param name="year">The year for which to get the holidays.</param>
		/// <param name="typesToCheckFor">The types for which to check for (Flags attribute - use e.g. "HolidayType.Juleaftensdag | HolidayType.FoersteJuledag").</param>
		/// <returns>A (possibly empty) list of holidays for the specified year.</returns>
		public List<Holiday> GetHolidaysForYear(int year, HolidayType typesToCheckFor = HolidayType.ALL)
		{
			if (!_holidaysForYears.ContainsKey(year))
				_holidaysForYears.Add(year, InitYear(year));

			var holidaysForYear = _holidaysForYears[year];

			return holidaysForYear.Where(x => (x.HolidayType & typesToCheckFor) == x.HolidayType).ToList();
		}

		/// <summary>
		/// Gets all holidays for a given year. This is a list, since several holidays may fall on the same date (e.g. "Pinse" and "Grundlovsdag").
		/// Optionally only includes holidays of a specified type.
		/// </summary>
		/// <param name="date">The date for which to return all holidays, that occur on this day (may be more than 1).</param>
		/// <param name="typesToCheckFor">The types for which to check for (Flags attribute - use e.g. "HolidayType.Juleaftensdag | HolidayType.FoersteJuledag").</param>
		/// <returns>A (possibly empty) list of holidays for the specified date.</returns>
		public List<Holiday> GetHolidaysForDate(DateTime date, HolidayType typesToCheckFor = HolidayType.ALL)
		{
			date = date.Date;
			var holidaysForYear = GetHolidaysForYear(date.Year, typesToCheckFor);
			var holidaysForDate = holidaysForYear.Where(x => x.Date == date).ToList();

			return holidaysForDate;
		}

		/// <summary>
		/// Checks whether or not a given date is a Danish holiday. Optionally only checks for the specified types.
		/// </summary>
		/// <param name="d">The date to check.</param>
		/// <param name="typesToCheckFor">The types for which to check for (Flags attribute - use e.g. "HolidayType.Juleaftensdag | HolidayType.FoersteJuledag").</param>
		/// <returns>A boolean indicating whether or not the given date contains 1 or more Danish holidays.</returns>
		public bool IsHoliday(DateTime d, HolidayType typesToCheckFor = HolidayType.ALL)
		{
			var holidaysForDate = GetHolidaysForDate(d, typesToCheckFor);
			var dayIsHoliday = holidaysForDate.Count > 0;
			return dayIsHoliday;
		}

		private List<Holiday> InitYear(int year)
		{
			var holidays = new List<Holiday>();
			var easterSunday = Util.FindEasterSunday(year);

			holidays.Add(new Holiday(new DateTime(year, 1, 1),  HolidayType.Nytaarsdag));
			holidays.Add(new Holiday(new DateTime(year, 6, 5), HolidayType.Grundlovsdag));
			holidays.Add(new Holiday(new DateTime(year, 12, 24), HolidayType.Juleaftensdag));
			holidays.Add(new Holiday(new DateTime(year, 12, 25), HolidayType.FoersteJuledag));
			holidays.Add(new Holiday(new DateTime(year, 12, 26), HolidayType.AndenJuledag));
			holidays.Add(new Holiday(new DateTime(year, 12, 31), HolidayType.Nytaarsaften));
			holidays.Add(new Holiday(easterSunday.AddDays(-3), HolidayType.SkaerTorsdag));
			holidays.Add(new Holiday(easterSunday.AddDays(-2), HolidayType.LangFredag));
			holidays.Add(new Holiday(easterSunday, HolidayType.PaaskeSoendag));
			holidays.Add(new Holiday(easterSunday.AddDays(1), HolidayType.AndenPaaskedag));
			holidays.Add(new Holiday(easterSunday.AddDays(26), HolidayType.StoreBededag));
			holidays.Add(new Holiday(easterSunday.AddDays(39), HolidayType.KristiHimmelfartsdag));
			holidays.Add(new Holiday(easterSunday.AddDays(49), HolidayType.PinseSoendag));
			holidays.Add(new Holiday(easterSunday.AddDays(50), HolidayType.AndenPinsedag));

			return holidays;
		}
    }
}
