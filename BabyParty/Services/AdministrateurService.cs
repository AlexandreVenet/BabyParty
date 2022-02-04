using Npgsql;
using System.Text;

using BabyParty.Models;

namespace BabyParty.Services
{
	public class AdministrateurService
	{
		private static Administrateur _administrateur;

		//static AdministrateurService()
		//{
		//	_administrateur = new Administrateur();
		//}

		public static Administrateur? Get(string nom, string passe)
		{
			//if(_administrateur.Nom == nom && _administrateur.Passe == passe)
			//{

			//}
			//return _administrateur.FirstOrDefault(p => p.Nom == nom && p.Passe == passe);

			// Connexion à bdd
			var connString = DataForConnecting(Config.ConfigServeur.m_data);

			// Requête et traitement
			using (var conn = new NpgsqlConnection(connString))
			{
				conn.Open();

				using (var cmd = new NpgsqlCommand($"SELECT nom, passe FROM administrateur WHERE nom='{nom}' AND passe='{passe}';", conn)) // A FAIRE : requête préparée
				{
					using (var reader = cmd.ExecuteReader())
					{
						_administrateur = new Administrateur();
						_administrateur.Nom = "Simplon";
						_administrateur.Passe = "PlonSim";

						while (reader.Read())
						{
							//Console.WriteLine($"nom : {reader.GetString(0)}");
							//Console.WriteLine($"passe: {reader.GetString(1)}");

							_administrateur.Nom = reader.GetString(0);
							_administrateur.Passe = reader.GetString(1);
						}
					}
				}
			}

			return _administrateur;
		}

		// ----------------------------------------

		// Méthodes de traitement

		private static string DataForConnecting(Dictionary<string, string> data)
		{
			StringBuilder sb = new StringBuilder();

			for (int i = 0; i < data.Count; i++)
			{
				sb.Append(data.ElementAt(i).Key);
				sb.Append("=");
				sb.Append(data.ElementAt(i).Value);
				if (i < data.Count - 1) sb.Append(";");
			}

			return sb.ToString();
		}

		private static Administrateur BuildAdministrateur(string nom, string passe)
		{
			return new Administrateur
			{

				Nom = nom,
				Passe = passe
			};
		}
	}
}
