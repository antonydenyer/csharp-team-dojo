using System.Web.Mvc;
using TeamDojo.Web.Models;

namespace TeamDojo.Web.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var mapPath = Server.MapPath("pronet.xml");
			var network = Network.Load(mapPath);

			return View(network);
		}

		public ActionResult About()
		{
			return View();
		}
	}
}
