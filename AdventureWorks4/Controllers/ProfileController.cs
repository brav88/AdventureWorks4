﻿using AdventureWorks4.FirebaseAuth;
using Firebase.Storage;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace AdventureWorks4.Controllers
{
	public class ProfileController : Controller
	{
		public IActionResult Index()
		{
			//Leemos de la sesion los datos del usuario y se lo pasamos a la vista
			ViewBag.User = JsonConvert.DeserializeObject<Models.User>(HttpContext.Session.GetString("userSession"));
			
			return View();
		}

		public IActionResult Details(int id)
		{
			return View();
		}

		public async Task<IActionResult> uploadPhoto(IFormFile photo)
		{
			//Leemos de la sesion los datos del usuario
			Models.User? user = JsonConvert.DeserializeObject<Models.User>(HttpContext.Session.GetString("userSession"));

			//Creamos la ruta donde se va a guardar la foto en el servidor local
			string photoPath = Directory.GetCurrentDirectory() + "\\wwwroot\\" + Path.Combine("Images\\", photo.FileName);

			//Copiamos la foto en la ruta del servidor local
			using (var stream = new FileStream(photoPath, FileMode.Create))
			{
				photo.CopyTo(stream);
			}

			//Abrimos la foto de servidor local para mandarla a Firebase Storage
			var downloadUrl = string.Empty;
			using (var streamToFb = System.IO.File.OpenRead(photoPath))
			{
				//Mandamos la foto a Firebase storage y este nos reponde la URL
				downloadUrl = await new FirebaseStorage($"{FirebaseAuthHelper.firebaseAppId}.appspot.com")
								 .Child("ProfilePhotos")
								 .Child(photo.FileName)
								 .PutAsync(streamToFb);
			}

			//Actualizamos en Firestore Database el campo PhotoPath con la URL que nos devolvio Firebase Storage
			FirestoreDb db = FirestoreDb.Create(FirebaseAuthHelper.firebaseAppId);
			DocumentReference docRef = db.Collection("Users").Document(user?.DocumentId);
			Dictionary<string, object> dataToUpdate = new Dictionary<string, object>
			{
				{ "PhotoPath", downloadUrl },				
			};
			WriteResult result = await docRef.UpdateAsync(dataToUpdate);

			//Pasamos la URl de la foto a la vista
			user.PhotoPath = downloadUrl;
			ViewBag.User = user;

			return View("Index");
		}		
	}
}
