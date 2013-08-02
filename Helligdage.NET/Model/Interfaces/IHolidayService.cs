using System;
using System.Collections.Generic;
using Helligdage.NET.Model.Domain;
using Helligdage.NET.Model.Enums;

namespace Helligdage.NET.Model.Interfaces
{
	public interface IHolidayService
	{
		/// <summary>
		/// Gets all holidays for a given year. Optionally only returns holidays of a specified type.
		/// </summary>
		/// <param name="year">The year for which to get the holidays.</param>
		/// <param name="typesToCheckFor">The types for which to check for (Flags attribute - use e.g. "HolidayType.Juleaftensdag | HolidayType.FoersteJuledag").</param>
		/// <returns>A (possibly empty) list of holidays for the specified year.</returns>
		List<Holiday> GetHolidaysForYear(int year, HolidayType typesToCheckFor = HolidayType.ALL);

		/// <summary>
		/// Gets all holidays for a given year. This is a list, since several holidays may fall on the same date (e.g. "Pinse" and "Grundlovsdag").
		/// Optionally only includes holidays of a specified type.
		/// </summary>
		/// <param name="date">The date for which to return all holidays, that occur on this day (may be more than 1).</param>
		/// <param name="typesToCheckFor">The types for which to check for (Flags attribute - use e.g. "HolidayType.Juleaftensdag | HolidayType.FoersteJuledag").</param>
		/// <returns>A (possibly empty) list of holidays for the specified date.</returns>
		List<Holiday> GetHolidaysForDate(DateTime date, HolidayType typesToCheckFor = HolidayType.ALL);

		/// <summary>
		/// Checks whether or not a given date is a Danish holiday. Optionally only checks for the specified types.
		/// </summary>
		/// <param name="d">The date to check.</param>
		/// <param name="typesToCheckFor">The types for which to check for (Flags attribute - use e.g. "HolidayType.Juleaftensdag | HolidayType.FoersteJuledag").</param>
		/// <returns>A boolean indicating whether or not the given date contains 1 or more Danish holidays.</returns>
		bool IsHoliday(DateTime d, HolidayType typesToCheckFor = HolidayType.ALL);
	}
}
