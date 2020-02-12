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
	public class AccessController : ApiController
	{
		[HttpGet]
		public Reply HelloWord()
		{
			Reply oR = new Reply();
			oR.result = 1;
			oR.mensaje = "Holaaaa";
;
			return oR;
		}

		[HttpPost]
		public Reply Login([FromBody] AccessViewModel model)
		{
			Reply oR = new Reply();
			oR.result = 0;
			try
			{
				using (cursomvcapiEntities db = new cursomvcapiEntities())
				{
					var lst = db.user.Where(d => d.email == model.email && d.password == model.password && d.idEstatus == 1);

					if(lst.Count() >0)
					{
						oR.result = 1;
						oR.data = Guid.NewGuid().ToString();
						oR.mensaje = "Exitoso";

						user oUser = lst.First();
						oUser.token = oR.data.ToString();

						db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
						db.SaveChanges();
					}
					else
					{
						oR.mensaje = "Datos incorrecto";
					}
				}

			}
			catch(Exception ex)
			{
				oR.result = 0;
				oR.mensaje = "Ha ocurrido un error";
				
			}
			return oR;
		}

	}
}
