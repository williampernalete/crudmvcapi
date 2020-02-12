using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cursomvcapi.Models.WS
{
	public class ListFacturaViewModel
	{
		public int? Id { get; set; }
		public string NumFactura { get; set; }
		public decimal Monto { get; set; }
		public DateTime Fecha { get; set; }
	}
}