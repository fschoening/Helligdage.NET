using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helligdage.NET.Core
{
	public class Util
	{
		/// <summary>
		/// Returns the DateTime for easter-sunday for the specified year using the Lilius-Clavius algorithm.
		/// 
		/// Algorithm: http://www.henk-reints.nl/easter/.
		/// </summary>
		public static DateTime FindEasterSunday(int year)
		{
			var a = year % 19 + 1;
			var b = year / 100 + 1;
			var c = (3 * b) / 4 - 12;
			var d = (8 * b + 5) / 25 - 5;
			var e = (year * 5) / 4 - 10 - c;
			var f = ((11 * a + 20 + d - c) % 30 + 30) % 30;
			f = (f == 24 || (f == 25 && a > 11)) ? f + 1 : f;
			var g = 44 - f;
			g = (g < 21) ? g + 30 : g;

			// result: the date of March being Easter Sunday, even if this date > 31 (if so, it wraps into April)
			var result = g + 7 - (e + g) % 7;

			var easterSunday = new DateTime(year, 3, 1);
			easterSunday = easterSunday.AddDays(result - 1);
			return easterSunday;
		}
	}
}
