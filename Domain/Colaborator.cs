﻿namespace Domain;

using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;

public class Colaborator : IColaborator
{
	public long Id { get; set; }
    public string _strName { get; set; }
    public string _strEmail { get; set; }

	protected Colaborator() {}

    public Colaborator(string _strName, string _strEmail) {

		if( isValidParameters(_strName, _strEmail) ) {
			this._strName = _strName;
			this._strEmail = _strEmail;
		}
		else
		{
			throw new ArgumentException("Invalid arguments.");
		}
	}

	private bool isValidParameters(string strName, string strEmail) {

		if( strName==null ||
			strName.Length > 50 ||
			string.IsNullOrWhiteSpace(strName) ||
			ContainsAny(strName, ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]))
			return false;

		if( !IsValidEmailAddres( strEmail ) )
			return false;
		
		return true;
	}

	private bool ContainsAny(string stringToCheck, params string[] parameters)
	{
		return parameters.Any(parameter => stringToCheck.Contains(parameter));
	}

	// from https://mailtrap.io/blog/validate-email-address-c/
	private bool IsValidEmailAddres(string email)
	{
		var valid = true;

		try
		{
			var emailAddress = new MailAddress(email);
		}
		catch
		{
			valid = false;
		}

		return valid;
	}

	public string getName() {
		return _strName;
	}
}
