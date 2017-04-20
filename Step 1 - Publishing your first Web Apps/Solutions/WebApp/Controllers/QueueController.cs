using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class QueueController : Controller
	{
		// GET: Queue/CreateMessage
		public ActionResult CreateMessage()
		{
			return View();
		}

		// POST: Queue/CreateMessage
		[HttpPost]
		public ActionResult CreateMessage(QueueMessageModel message)
		{
			if (ModelState.IsValid)
			{
				// TODO: Insert add message to queue logic here

				return RedirectToAction("Index", "Home");
			}

			return View(message);
		}
	}
}
