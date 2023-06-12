namespace Quoter.Shared.Models
{
	public class ActionResult
	{
		public bool IsSuccess { get; set; }

		private object _value { get; set; }

		public T GetValue<T>()
		{
			if(_value is T)
			{
				return (T)_value;
			}
			else
			{
				throw new InvalidOperationException($"Value is not typeof {typeof(T)}");
			}
		}

		public ActionResult(bool isSuccess, object value)
		{
			IsSuccess = isSuccess;
			_value = value;
		}

		public static ActionResult Success(object value)
		{
			return new ActionResult(true, value);
		}

		public static ActionResult Fail()
		{
			return new ActionResult(false, null);
		}
	}
}
