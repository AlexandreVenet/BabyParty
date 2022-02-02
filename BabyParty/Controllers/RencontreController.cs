using Microsoft.AspNetCore.Mvc;

using BabyParty.Models;
using BabyParty.Services;

namespace BabyParty.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RencontreController : ControllerBase
	{
		public RencontreController()
		{
		}

		[HttpGet]
		public ActionResult<List<Rencontre>> GetAll()
		{
			return RencontreService.GetAll();
		}

		[HttpGet("{id}")]
		public ActionResult<Rencontre> Get(int id)
		{
			Rencontre? rencontre = RencontreService.Get(id);
			if (rencontre == null) return NotFound();
			return rencontre;
		}

		[HttpPost]
		public IActionResult Post(Rencontre rencontre)
		{
			RencontreService.Add(rencontre);
			return CreatedAtAction("Post", new { id = rencontre.Id }, rencontre);
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, Rencontre rencontre)
		{
			if (id != rencontre.Id) return BadRequest();

			Rencontre? existingRencontre = RencontreService.Get(id);
			if (existingRencontre == null) return NotFound();

			RencontreService.Update(rencontre);

			return NoContent();
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			Rencontre? rencontre = RencontreService.Get(id);
			if (rencontre == null) return NotFound();

			RencontreService.Delete(id);

			return NoContent();
		}
	}
}
