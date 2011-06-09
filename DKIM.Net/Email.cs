/*
 * DKIM.Net
 * 
 * Copyright (C) 2011 Damien McGivern, damien@mcgiv.com
 * 
 * 
 * 
 * */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;


namespace DKIM
{

	/// <summary>
	/// Stores the origional header key and value and wether or not the value is folded.
	/// </summary>
	public class EmailHeader
	{
		public string Key;
		public string Value;
	}


	public class Email
	{
		public List<EmailHeader> Headers { get; set; }
		public string Body { get; set; }
		public Encoding Encoding { get; set; }
		public const string NewLine = "\r\n";

		public List<EmailHeader> GetHeadersToSign(string[] includeList)
		{
			var result = new List<EmailHeader>();

			if (includeList != null && includeList.Length > 0)
			{
				// Build a case insensitive dictionary of the headers we want
				var headersToInclude = new Dictionary<string, bool>(StringComparer.InvariantCultureIgnoreCase);
				foreach (var h in includeList)
				{
					headersToInclude.Add(h, true);
				}

				// Find headers to be included
				foreach (var h in Headers)
				{
					if (headersToInclude.ContainsKey(h.Key))
					{
						// Add to result
						result.Add(h);
					}
				}
			}
			else
			{
				// Include all headers
				foreach (var h in Headers)
				{
					result.Add(h);
				}
			}

			return result;
		}
	}
}
