using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
   public class ConnectionStringOption
    {
		public const string Key = "ConnectionStrings";
		public string SqlServer { get; set; } = default!;
	}
}
