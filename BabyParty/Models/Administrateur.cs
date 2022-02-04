namespace BabyParty.Models
{
	public class Administrateur
	{
		// Schéma de données = correspondance de la table "administrateur" en bdd

		public int Id { get; set; }
		public string? Nom { get; set; }
		public string? Passe { get; set; }
	}
}