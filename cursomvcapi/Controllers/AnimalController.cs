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
    public class AnimalController : BaseController
    {
		[HttpPost]
		public Reply ListarAnimales([FromBody] SecurityViewModel model)
		{
			Reply oR = new Reply();
			oR.result = 0;

			//Verificamos el token
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
					List<ListAnimalViewModel> lst = (from d in db.animal
													  where d.idState == 1
													  select new ListAnimalViewModel
													  {
														  Nombre = d.name,
														  Patas = d.patas
													  }).ToList();
					oR.data = lst;
					oR.result = 1;
				}
			}
			catch(Exception ex)
			{
				oR.mensaje = "Error en el servidor ";
			}
			return oR;
		}

		[HttpPost]
		public Reply Add([FromBody] AnimalViewModel model)
		{
			Reply oR = new Reply();
			oR.result = 0;

			//Verificamos el token
			if (!Verify(model.token))
			{
				oR.mensaje = "no esta autorizado";
				oR.result = 0;
				return oR;

			}
			//Validaciones
			if (!Validate(model){
				oR.mensaje = error;
				return oR;
			}

			try
			{
				using (cursomvcapiEntities db = new cursomvcapiEntities())
				{
					animal oAnimal = new animal();
					oAnimal.idState = 1;
					oAnimal.name = model.Nombre;
					oAnimal.patas = model.Patas;

					db.animal.Add(oAnimal);
					db.SaveChanges();

					// retorno la lista de animales con el nuevo agregadoo
					List<ListAnimalViewModel> lst = (from d in db.animal
													 where d.idState == 1
													 select new ListAnimalViewModel
													 {
														 Nombre = d.name,
														 Patas = d.patas
													 }).ToList();
					oR.data = lst;
					oR.result = 1;

				}
			}
			catch(Exception ex)
			{
				oR.mensaje = "HA ocurrido un error";

			}
			return oR;
		}

		#region HELPERSS
		private bool Validate(AnimalViewModel model)
		{
			if(model.Nombre == "")
			{
				error = "El nombre es obligatorio";
				return false;
					 
			}
			return true;
		}

		#endregion
	}
}
