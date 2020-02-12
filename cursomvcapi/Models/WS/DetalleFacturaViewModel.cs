using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cursomvcapi.Models.WS
{
	public class DetalleFacturaViewModel : SecurityViewModel
	{
		public int? Id { get; set; }
		public string NumFactura { get; set; }
		public string CodArticulo { get; set; }
		public int? Cantidad { get; set; }
		public decimal PrecioUnitario { get; set; }
		public decimal Total { get; set; }
	}
}