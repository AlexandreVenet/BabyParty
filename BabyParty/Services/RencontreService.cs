using Npgsql;
using System.Text;

using BabyParty.Models;

namespace BabyParty.Services
{
	public static class RencontreService
	{
		private static List<Rencontre> _rencontres;
		private static int _nextId;

		static RencontreService()
		{
			_rencontres = new List<Rencontre>()
			{
				new Rencontre {Id = 0, DateRencontre = "2022-02-01", Equipe1="Toto", Equipe2="Doudou", Score1=0, Score2= 3},
				new Rencontre {Id = 1, DateRencontre = "2022-02-02", Equipe1="Youpi", Equipe2="Lala", Score1=12, Score2= 4},
			};
			_nextId = _rencontres.Count;

			//System.Diagnostics.Debug.WriteLine("toto"); // ------------------------ TEST
		}

		public static List<Rencontre> GetAll()
		{
			// Connexion à bdd
			var connString = DataForConnecting(Config.ConfigServeur.m_data);

			// Requête et traitement
			using (var conn = new NpgsqlConnection(connString))
			{
				conn.Open();

				using (var cmd = new NpgsqlCommand("SELECT * FROM rencontre;", conn)) // A FAIRE : requête préparée
				{
					using (var reader = cmd.ExecuteReader())
					{
						_rencontres = new List<Rencontre>();
						
						while (reader.Read())
						{
							//Console.WriteLine($"Date de match : {reader.GetValue(1)}");
							//Console.WriteLine($"Equipes : {reader.GetValue(2)} VS {reader.GetValue(3)}");
							//Console.WriteLine($"Scores : {reader.GetValue(4)} / {reader.GetValue(5)}");

							// Horrible à modifier ! Traitements à compléter !
							int id = reader.GetInt32(0);
							string time = reader.GetDateTime(1).ToString();
							string equipe1 = reader.GetString(2);
							string equipe2 = reader.GetString(3);
							int score1 = 0;
							try
							{
								score1 = reader.GetInt32(4);
							}
							catch
							{
								score1 = 0;
							}
							int score2 = 0;
							try
							{
								score2 = reader.GetInt32(5);
							}
							catch
							{
								score2 = 0;
							}

							Rencontre r = BuildRencontre(id, time, equipe1, equipe2, score1, score2);
							_rencontres.Add(r);
							_nextId = _rencontres.Count;
						}
					}
				}
			}

			return _rencontres;
		}

		public static Rencontre? Get(int id)
		{
			return _rencontres.FirstOrDefault(p => p.Id == id);
		}

		public static void Add(Rencontre rencontre)
		{
			rencontre.Id = _nextId++;
			_rencontres.Add(rencontre);
		}

		public static void Update(Rencontre rencontre)
		{
			int index = _rencontres.FindIndex(p => p.Id == rencontre.Id);
			if (index == -1) return;
			_rencontres[index] = rencontre;
		}

		public static void Delete(int id)
		{
			Rencontre? rencontre = Get(id);
			if (rencontre == null) return;
			_rencontres.Remove(rencontre);
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

		private static Rencontre BuildRencontre(int id, string date, string equipe1, string equipe2, int score1, int score2)
		{
			return new Rencontre
			{
				Id = id,
				DateRencontre = date,
				Equipe1 = equipe1,
				Equipe2 = equipe2,
				Score1 = score1,
				Score2 = score2
			};
		}
	}
}
