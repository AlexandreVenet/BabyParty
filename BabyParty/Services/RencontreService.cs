using Npgsql;
using System.Text;

using BabyParty.Models;

namespace BabyParty.Services
{
	public static class RencontreService
	{
		private static List<Rencontre>? _rencontres;
		private static Rencontre? _rencontre;

		// Constructeur
		//static RencontreService()
		//{
			//System.Diagnostics.Debug.WriteLine("toto"); // ------------------------ TEST
		//}


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

							Rencontre r = new Rencontre()
							{
								Id = id,
								DateRencontre = time,
								Equipe1 = equipe1,
								Equipe2 = equipe2,
								Score1 = score1,
								Score2 = score2
							};

							_rencontres.Add(r);
						}
					}
				}
			}

			return _rencontres;
		}

		public static Rencontre? Get(string date)
		{
			// Connexion à bdd
			var connString = DataForConnecting(Config.ConfigServeur.m_data);

			// Requête et traitement
			using (var conn = new NpgsqlConnection(connString))
			{
				conn.Open();

				using (var cmd = new NpgsqlCommand($"SELECT date_rencontre, equipe1, equipe2, score1, score2 FROM rencontre WHERE date_rencontre = '{date}';", conn))
				{
					using (var reader = cmd.ExecuteReader())
					{
						_rencontre = new Rencontre
						{
							DateRencontre = date,
							Equipe1 = "rien",
							Equipe2 = "rien",
							Score1 = -1,
							Score2 = -1
						};

						while (reader.Read())
						{
							string time = reader.GetDateTime(0).ToString();
							string equipe1 = reader.GetString(1);
							string equipe2 = reader.GetString(2);
							int score1 = 0;
							try
							{
								score1 = reader.GetInt32(3);
							}
							catch
							{
								score1 = 0;
							}
							int score2 = 0;
							try
							{
								score2 = reader.GetInt32(4);
							}
							catch
							{
								score2 = 0;
							}

							_rencontre.DateRencontre = time;
							_rencontre.Equipe1 = equipe1;
							_rencontre.Equipe2 = equipe2;
							_rencontre.Score1 = score1;
							_rencontre.Score2 = score2;
						}
					}
				}
			}

			return _rencontre;
		}

		public static int GetId(string date)
		{
			// Connexion à bdd
			var connString = DataForConnecting(Config.ConfigServeur.m_data);

			// Requête et traitement
			int id = default; 
			using (var conn = new NpgsqlConnection(connString))
			{
				conn.Open();

				//using var cmd = new NpgsqlCommand($"SELECT * FROM rencontre WHERE date_rencontre = (@p1);", conn)
				//{
				//	Parameters =
				//	{
				//		new("p1", DateTime.Parse(date)),
				//	}

				//};
				//cmd.ExecuteNonQueryAsync(); 
				// Et ensuite ? Comment traiter les valeurs retournées ?

				using (var cmd = new NpgsqlCommand($"SELECT id FROM rencontre WHERE date_rencontre = '{date}';", conn))
				{
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read()) 
						{
							id = reader.GetInt32(0);
						}
					}
				}
			}

			return id;
		}

		public static async void Post(Rencontre rencontre)
		{
			// Connexion à bdd
			var connString = DataForConnecting(Config.ConfigServeur.m_data);

			// Requête et traitement
			using (var conn = new NpgsqlConnection(connString))
			{
				conn.Open();

				DateTime result;
				if (rencontre.DateRencontre == null) return; //rencontre.DateRencontre = DateTime.Now.ToString();
				if (!DateTime.TryParse(rencontre.DateRencontre, out result)) return; //rencontre.DateRencontre = DateTime.Now.ToString();

				await using var cmd = new NpgsqlCommand("INSERT INTO rencontre (date_rencontre, equipe1, equipe2) VALUES (@p1, @p2, @p3)", conn)
				{
					Parameters =
					{
						new("p1", DateTime.Parse(rencontre.DateRencontre)),
						new("p2", rencontre.Equipe1),
						new("p3", rencontre.Equipe2)
					}
				};
				await cmd.ExecuteNonQueryAsync();
			}
		}

		public static async void Post(string date, string equipe1, string equipe2)
		{
			// Connexion à bdd
			var connString = DataForConnecting(Config.ConfigServeur.m_data);

			// Requête et traitement
			using (var conn = new NpgsqlConnection(connString))
			{
				conn.Open();

				DateTime result;
				if (date == null) return; //rencontre.DateRencontre = DateTime.Now.ToString();
				if (!DateTime.TryParse(date, out result)) return; //rencontre.DateRencontre = DateTime.Now.ToString();

				await using var cmd = new NpgsqlCommand("INSERT INTO rencontre (date_rencontre, equipe1, equipe2) VALUES (@p1, @p2, @p3)", conn)
				{
					Parameters =
					{
						new("p1", DateTime.Parse(date)),
						new("p2", equipe1),
						new("p3", equipe2)
					}
				};
				await cmd.ExecuteNonQueryAsync();
			}
		}

		public static async void Update(string date, int score1, int score2)
		{
			Rencontre? testRencontre = Get(date);
			if(testRencontre == null) return;

			// Connexion à bdd
			var connString = DataForConnecting(Config.ConfigServeur.m_data);

			// Requête et traitement
			using (var conn = new NpgsqlConnection(connString))
			{
				conn.Open();

				DateTime result;
				if (date == null) return; 
				if (!DateTime.TryParse(date, out result)) return;

				await using var cmd = new NpgsqlCommand("UPDATE rencontre SET score1 = (@p1), score2 = (@p2) WHERE date_rencontre = (@p3);", conn)
				{
					Parameters =
					{
						new("p1", score1),
						new("p2", score2),
						new("p3", DateTime.Parse(date)),
					}
				};
				await cmd.ExecuteNonQueryAsync();
			}
		}

		public static async void Delete(int id)
		{
			// Connexion à bdd
			var connString = DataForConnecting(Config.ConfigServeur.m_data);

			// Requête et traitement
			using (var conn = new NpgsqlConnection(connString))
			{
				conn.Open();

				await using var cmd = new NpgsqlCommand($"DELETE FROM rencontre WHERE id=(@p1);", conn)
				{
					Parameters =
					{
						new ("p1", id)
					}
				};
				await cmd.ExecuteNonQueryAsync();
			}
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

	}
}
