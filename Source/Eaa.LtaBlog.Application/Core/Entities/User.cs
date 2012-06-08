using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Security.Principal;
using Newtonsoft.Json;

namespace Eaa.LtaBlog.Application.Core.Entities
{
	public class User: IIdentity
	{
		const string SALT = "cx908#lta.is.diego's.phrase#123TTT";

		public User()
		{
			Roles = new List<string>();
		}

		public string Id
		{
			get;
			set;
		}

		public string Firstname { get; set; }

		public string Lastname { get; set; }

		public string Email { get; set; }
		
		public List<string> Roles { get; private set; }

		public bool IsAdmin { get { return Roles.Contains("admin", StringComparer.InvariantCultureIgnoreCase); } }

		

		protected string HashedPassword { get; private set; }

		private string passwordSalt;
		private string PasswordSalt
		{
			get
			{
				return passwordSalt ?? (passwordSalt = Guid.NewGuid().ToString("N"));
			}
			set { passwordSalt = value; }
		}

		public User SetPassword(string pwd)
		{
			HashedPassword = GetHashedPassword(pwd);
			return this;
		}

		private string GetHashedPassword(string pwd)
		{
			using (var sha = SHA256.Create())
			{
				var computedHash = sha.ComputeHash(Encoding.Unicode.GetBytes(PasswordSalt + pwd + SALT));
				return Convert.ToBase64String(computedHash);
			}
		}

		public bool ValidatePassword(string password)
		{
			return HashedPassword == GetHashedPassword(password);
		}

		public override string ToString()
		{
			return Firstname + " " + Lastname;
		}

		#region IIdentity Members

		[JsonIgnore]
		public string AuthenticationType
		{
			get { return "LTA"; }
		}

		[JsonIgnore]
		public bool IsAuthenticated
		{
			get { return true; }
		}

		[JsonIgnore]
		public string Name
		{
			get { return Email; }
		}

		#endregion
	}
}