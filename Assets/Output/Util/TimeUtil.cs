using System;

namespace Tenkafubu.Util
{
	public static class TimeUtil
	{
		
		public static DateTime UnixEpocTime = new DateTime(1970,1,1,0,0,0,0,DateTimeKind.Utc);
		
		/// <summary>
		/// To the unix time.
		/// Time unit is seconds.
		/// </summary>
		/// <returns>
		/// The unix time.
		/// </returns>
		/// <param name='time'>
		/// Time.
		/// </param>
		public static long ToUnixTime(DateTime time){
			var diff = (time.ToUniversalTime() - UnixEpocTime);
			return (long)diff.TotalSeconds;
		}
		
		public static DateTime FromUnixTime(long unixTime){
			return UnixEpocTime.ToLocalTime().AddSeconds(unixTime);
		}

		public static long CurrentUnixTime_Sec() {
			return ToUnixTime(DateTime.Now);
		}
	}
}

