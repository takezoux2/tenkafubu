using System;

namespace Tenkafubu.Concurrent
{
	public interface Result<T>
	{
		
		T Result{get;}
		Exception Error{get;}
		
		bool Success{get;}
		bool HasError{get;}
	}
	
	
}

