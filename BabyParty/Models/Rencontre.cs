namespace BabyParty.Models
{
	public class Rencontre
	{
		// Schéma de données = correspondance de la table "rencontre" en bdd

		public int Id { get; set; }
		public string? DateRencontre { get; set; }
		public string? Equipe1 { get; set; }
		public string? Equipe2 { get; set; }
		public int Score1 { get; set; }	
		public int Score2 { get; set; }
	}
}
