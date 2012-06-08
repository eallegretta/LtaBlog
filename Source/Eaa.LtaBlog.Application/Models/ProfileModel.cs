using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using System.Web.Mvc;

namespace Eaa.LtaBlog.Application.Models
{
	public class ProfileModel
	{
		[Required]
		[HiddenInput(DisplayValue = false)]
		public string Id { get; set; }

		[Required]
		[Email]
		public string Email { get; set; }
		
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Validate Password")]
		[Compare("Password")]
		public string ValidatePassword { get; set; }

		[Required]
		public string Firstname { get; set; }
		
		[Required]
		public string Lastname { get; set; }
	}
}