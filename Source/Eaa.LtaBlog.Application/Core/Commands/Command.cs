using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Raven.Client;

namespace Eaa.LtaBlog.Application.Core.Commands
{
	public abstract class Command
	{
		private List<ValidationResult> _validationResults = new List<ValidationResult>();
		
		public IDocumentSession Session { get; set; }
		public abstract void Execute();

		private bool? _isValid = null;


		protected void AddValidationResult(string errorMessage, params string[] memberNames)
		{
			AddValidationResult(new ValidationResult(errorMessage, memberNames));
		}

		protected void AddValidationResult(ValidationResult validationResult)
		{
			_validationResults.Add(validationResult);
		}

		protected void AddValidationResults(params ValidationResult[] validationResults)
		{
			_validationResults.AddRange(validationResults);
		}

		public bool IsValid
		{
			get
			{
				if (_isValid.HasValue)
					return _isValid.Value;

				_isValid = Validate();
				return _isValid.Value;
			}
		}


		protected virtual bool Validate()
		{
			var validationResults = new List<ValidationResult>();
			Validator.TryValidateObject(this, new ValidationContext(this, null, null), validationResults, true);

			AddValidationResults(validationResults.ToArray());

			return validationResults.Count == 0;
		}

		public virtual IEnumerable<ValidationResult> ValidationResults()
		{
			return _validationResults;
		}

	}

	public abstract class Command<TResult>: Command
	{
		public TResult Result { get; protected set; }
	}


}
