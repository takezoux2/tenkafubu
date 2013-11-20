using System;
using System.Collections;


namespace Tenkafubu.Util
{
	public class EmptyEnumerator : IEnumerator
	{
		public EmptyEnumerator ()
		{
		}
		
		public bool MoveNext ()
		{
			return false;
		}

		public void Reset ()
		{
		}

		public object Current {
			get {
				return null;
			}
		}
	}
}

