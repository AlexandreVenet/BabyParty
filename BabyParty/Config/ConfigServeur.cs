namespace BabyParty.Config
{
	public static class ConfigServeur
	{
		static public Dictionary<string, string> m_data = new Dictionary<string, string>(){
			{ "Host",       "localhost" },
			{ "Port",       "5432" },
			{ "Username",   "postgres" },
			{ "Password",   "simplon59" },
			{ "Database",   "BabyParty" }
		};
	}
}
