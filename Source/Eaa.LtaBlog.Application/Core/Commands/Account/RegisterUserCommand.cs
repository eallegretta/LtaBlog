using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eaa.LtaBlog.Application.Core.Queries;
using Eaa.LtaBlog.Application.Core.Entities;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using AutoMapper;

namespace Eaa.LtaBlog.Application.Core.Commands.Account
{
	public class RegisterUserCommand: Command
	{
		[Required]
		[Email]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		public string Firstname { get; set; }

		[Required]
		public string Lastname { get; set; }

		protected override bool Validate()
		{
			bool isValid = base.Validate();

			if (isValid)
			{
				var alreadyExistingUser = Session.GetUserByEmail(Email);

				if (alreadyExistingUser != null)
				{
					AddValidationResult("The email you are trying to register already exists", "Email");

					return false;
				}
			}

			return isValid;
		}

		public override void Execute()
		{
			if (!IsValid)
				return;

			var user = Mapper.Map<User>(this);
			user.Roles.Add("user");

			Session.Store(user);
		}
	}
}