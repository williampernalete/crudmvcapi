using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cursomvcapi.Models.WS;
using cursomvcapi.Models;


namespace cursomvcapi.Controllers
{
    public class FacturaController : BaseController
	{
		[HttpGet]
		public Reply ListarFacturas([FromBody] SecurityViewModel model)
		{
			Reply oR = new Reply();
			oR.result = 0;

			if (!Verify(model.token))
			{
				oR.mensaje = "no esta autorizado";
				oR.result = 0;
				return oR;
			}
			try
			{
				using (cursomvcapiEntities db = new cursomvcapiEntities())
				{

					List<ListFacturaViewModel> lstFact = (from d in db.facturas
														  select new ListFacturaViewModel
														  {
															  Id = d.id,
															  NumFactura = d.numfactura,
															  Monto = (decimal)d.monto,
															  Fecha = (DateTime)d.fecha
														  }).ToList();

					oR.data = lstFact;
					oR.result = 1;
				}
			}
			catch(Exception ex)
			{
				oR.mensaje= "No se pueden listar las faturass";
				
			}
			return oR;
		}
    }
}
