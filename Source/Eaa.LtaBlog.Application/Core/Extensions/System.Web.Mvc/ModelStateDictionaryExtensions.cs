using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	public static class ModelStateDictionaryExtensions
	{
		public static void AddValidationResults(this ModelStateDictionary modelState, IEnumerable<ValidationResult> validationResults)
		{
			foreach (var result in validationResults)
			{
				string memberName = result.MemberNames.FirstOrDefault();

				if (memberName == null)
					memberName = Guid.NewGuid().ToString();

				modelState.AddModelError(memberName, result.ErrorMessage);
			}
		}
	}
}

