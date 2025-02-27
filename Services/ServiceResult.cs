using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.Services
{

	
    public class ServiceResult<T>
    {
		public T? Data { get; set; }
		public List<string>? ErrorMessage { get; set; }
		[JsonIgnore] // serialize etme demek
		public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
		[JsonIgnore]
		public bool IsFail => !IsSuccess;
		[JsonIgnore]
		public HttpStatusCode Status { get; set; }

		// static factory method
		public static ServiceResult<T> Success(T data,HttpStatusCode status = HttpStatusCode.OK)
		{
			return new ServiceResult<T>()
			{
				Data = data,
				Status = status
			};
		}

		public static ServiceResult<T> Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
		{
			return new ServiceResult<T>()
			{
				ErrorMessage = errorMessage,
				Status = status
			};
		}
		public static ServiceResult<T> Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
		{
			return new ServiceResult<T>()
			{
				ErrorMessage = new List<string>() { errorMessage },
				Status = status
			};
		}
	}

	public class ServiceResult
	{
		
		public List<string>? ErrorMessage { get; set; }

		[JsonIgnore]
		public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;

		[JsonIgnore] 
		public bool IsFail => !IsSuccess;

		[JsonIgnore]
		public HttpStatusCode Status { get; set; }

		// static factory method
		public static ServiceResult Success(HttpStatusCode status = HttpStatusCode.OK)
		{
			return new ServiceResult()
			{
				Status = status
			};
		}

		public static ServiceResult Fail(List<string> errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
		{
			return new ServiceResult()
			{
				ErrorMessage = errorMessage,
				Status = status
			};
		}
		public static ServiceResult Fail(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
		{
			return new ServiceResult()
			{
				ErrorMessage = new List<string>() { errorMessage },
				Status = status
			};
		}
	}
}
