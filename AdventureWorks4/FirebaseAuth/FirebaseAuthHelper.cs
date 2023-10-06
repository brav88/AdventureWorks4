using Firebase.Auth.Providers;
using Firebase.Auth;
using Microsoft.AspNetCore.DataProtection;

namespace AdventureWorks4.FirebaseAuth
{
	public static class FirebaseAuthHelper
	{
		public const string firebaseAppId = "<your app id>";
		public const string firebaseApiKey = "<your api key>";

		public static FirebaseAuthClient setFirebaseAuthClient() 
		{
			var response = new FirebaseAuthClient(new FirebaseAuthConfig
			{
				ApiKey = firebaseApiKey,
				AuthDomain = $"{firebaseAppId}.firebaseapp.com",
				Providers = new FirebaseAuthProvider[]
					{
						new EmailProvider()
					}
			});

			return response;
		}
	}
}
