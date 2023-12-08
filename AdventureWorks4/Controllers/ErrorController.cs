using AdventureWorks4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorks4.Controllers
{
	public class ErrorController : Controller
	{
		// GET: ErrorController
		public ActionResult Index(int id)
		{
			ErrorHandler? err = null;

			if (id == 99)
			{
				err = new ErrorHandler()
				{
					Title = "You do NOT have access to this resource",
					ErrorMessage = "Please log in again",
					ActionMessage = "Go to login",
					Path = "/Login"
				};
			}
			else
			{
				err = new ErrorHandler()
				{
					Title = "You must login to access this resource",
					ErrorMessage = "Session is inactive",
					ActionMessage = "Go to login",
					Path = "/Login"
				};
			}
			
			ViewBag.Error = err;

			return View("ErrorHandler");			
		}

		// GET: ErrorController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: ErrorController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: ErrorController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: ErrorController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: ErrorController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: ErrorController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: ErrorController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
