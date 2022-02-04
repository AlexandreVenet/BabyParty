using Microsoft.AspNetCore.Mvc;

using BabyParty.Models;
using BabyParty.Services;

namespace BabyParty.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RencontreController : ControllerBase
	{
		//public RencontreController()
		//{
		//}

		[HttpGet]
		public ActionResult<List<Rencontre>> GetAll()
		{
			return RencontreService.GetAll();
		}

		[HttpGet("{date}")]
		public ActionResult<Rencontre> Get(string date)
		{
			Rencontre? rencontre = RencontreService.Get(date);
			if (rencontre == null) return NotFound();
			return rencontre;
		}

		// un get par date pour obtenir un id
		[HttpGet("id/{date}")]
		public ActionResult<int> GetId(string date)
		{
			int? id = RencontreService.GetId(date);
			if (id == null) return NotFound();
			return id;
		}

		[HttpPost]
		public IActionResult Post(Rencontre rencontre)
		{
			RencontreService.Post(rencontre);
			return CreatedAtAction("Post", new { id = rencontre.Id }, rencontre);
		}

		[HttpPost("AjouterParChamps/{date}/{equipe1}/{equipe2}")]
		public IActionResult Post(string date, string equipe1, string equipe2)
		{ 
			RencontreService.Post(date, equipe1, equipe2);
			return CreatedAtAction("Post", new {date = date, equipe1 = equipe1, equipe2 = equipe2});
		}

		//[HttpPut("{id}")]
		//public IActionResult Update(int id, Rencontre rencontre)
		//{
		//	if (id != rencontre.Id) return BadRequest();

		//	Rencontre? existingRencontre = RencontreService.Get(id);
		//	if (existingRencontre == null) return NotFound();

		//	RencontreService.Update(rencontre);

		//	return NoContent();
		//}
		
		[HttpPut("{date}/{score1}/{score2}")]
		public IActionResult Update(string date, int score1, int score2)
		{
			Rencontre? existingRencontre = RencontreService.Get(date);
			if (existingRencontre == null) return NotFound();

			RencontreService.Update(date, score1, score2);

			return NoContent();
		}

		//[HttpDelete("{id}")]
		//public IActionResult Delete(int id)
		//{
		//	Rencontre? rencontre = RencontreService.Get(id);
		//	if (rencontre == null) return NotFound();

		//	RencontreService.Delete(id);

		//	return NoContent();
		//}

		[HttpDelete("{date}")]
		public IActionResult Delete(string date)
		{
			int id = default; 

			try
			{
				id = RencontreService.GetId(date);
			}
			catch (Exception)
			{
				return NotFound();
			}

			RencontreService.Delete(id);

			return NoContent();
		}

		[HttpDelete("delete/{id}")]
		public IActionResult Delete(int id)
		{
			RencontreService.Delete(id);

			return NoContent();
		}
	}
}
