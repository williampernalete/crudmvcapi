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

		[HttpGet]
		public Reply Show([FromBody] DetalleFacturaViewModel model)
		{
			Reply oResp = new Reply();
			oResp.result = 0;

			if(!Verify(model.token))
			{
				oResp.mensaje = "no esta autorizado";
				return oResp;
			}
			try
			{
				using (cursomvcapiEntities db = new cursomvcapiEntities())
				{
					List<DetalleFacturaViewModel> oDetalle = (from d in db.detallefactura
													 where d.numfactura == model.NumFactura
													 select new DetalleFacturaViewModel
													 {
														 Id= d.id,
														 NumFactura = d.numfactura,
														 CodArticulo = d.codarticulo,
														 Cantidad = d.cantidad,
														 PrecioUnitario= d.preciounitario,
														 Total = d.total
													 }).ToList();
					

					oResp.data = oDetalle;
					oResp.result = 1;



				}
			}
			catch(Exception ex)
			{
				oResp.mensaje = "problemas en el show";
			}
			return oResp;
		}
    }
}
