using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using VinStageStore.Context;

namespace VinStageStore.Controllers
{
	public class UserController : Controller
	{
		VinStageShopEntities db = new VinStageShopEntities();
		// GET: User
		public ActionResult Index()
		{
			var userIdObject = Session["idUser"];
			User user = null;

			if (userIdObject != null && int.TryParse(userIdObject.ToString(), out int userId))
			{
				user = db.Users.FirstOrDefault(n => n.Id == userId);
			}

			return View(user);
		}

		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "Id", "RoleName", user.RoleId);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,FullName,Address,Phone")] User user)
		{
			if (ModelState.IsValid)
			{
				// Load the existing user entity from the database
				User existingUser = db.Users.Find(user.Id);

				if (existingUser == null)
				{
					return HttpNotFound();
				}

				// Update only the specific fields
				existingUser.FullName = user.FullName;
				existingUser.Address = user.Address;
				existingUser.Phone = user.Phone;

				// Mark the entity as modified and save changes
				db.Entry(existingUser).State = EntityState.Modified;
				db.SaveChanges();

				return RedirectToAction("Index");
			}

			return View(user);
		}
	}
}