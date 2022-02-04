using Microsoft.AspNetCore.Mvc;

using BabyParty.Models;
using BabyParty.Services;

namespace BabyParty.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AdministrateurController : ControllerBase
	{
		[HttpGet("{nom}/{passe}")]
		public ActionResult<Administrateur> Get(string nom, string passe)
		{
			Administrateur? administrateur = AdministrateurService.Get(nom, passe);
			if (administrateur == null) return NotFound();
			return administrateur;
		}
	}
}
