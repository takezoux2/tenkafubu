using System;

namespace Tenkafubu.Concurrent
{
	public class NotAllowedOperationException : Exception
	{
		public NotAllowedOperationException (string message) : base(message)
		{
			
		}
	}
}

