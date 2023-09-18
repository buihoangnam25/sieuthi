using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VinStageStore.Context;
using VinStageStore.Models;

namespace VinStageStore.Controllers
{
	public class HomeController : Controller
	{
		VinStageShopEntities objModel = new VinStageShopEntities();
		public ActionResult Index()
		{
			HomeModel homeModel = new HomeModel();
			homeModel.ListCategory = objModel.Categories.ToList();
			homeModel.ListProduct = objModel.Products.ToList();	
			homeModel.ListSupplier = objModel.Suppliers.ToList();
			return View(homeModel);
		}

		public ActionResult Register() 
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(User _user)
		{
			//kiểm tra và lưu vào db
			if (ModelState.IsValid)
			{
				var check = objModel.Users.FirstOrDefault(s => s.Email == _user.Email);
				if(check == null)
				{
					_user.Password = GetMD5(_user.Password);
					objModel.Configuration.ValidateOnSaveEnabled = false;
					objModel.Users.Add(_user);
					objModel.SaveChanges();
					return RedirectToAction("Index");
				}
				else
				{
					ViewBag.error = "Email đã tồn tại";
					return View();
				}
			}


			return View("Index");
		}

		public static string GetMD5(string str)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] fromData = Encoding.UTF8.GetBytes(str);
			byte[] targetData = md5.ComputeHash(fromData);

			string byte2String = null;
			for (int i = 0; i < targetData.Length; i++)
			{
				byte2String += targetData[i].ToString("x2");
			}
			return byte2String; ;
		}

		public ActionResult Login() 
		{
			return View(); 
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(string username, string password)
		{
			if (ModelState.IsValid)
			{

				var f_password = GetMD5(password);
				var data = objModel.Users.Where(s => s.UserName.Equals(username) && s.Password.Equals(f_password)).ToList();
				
				if (data.Count() > 0)
				{
					//add session
					Session["FullName"] = data.FirstOrDefault().FullName;
					Session["Email"] = data.FirstOrDefault().Email;
					Session["idUser"] = data.FirstOrDefault().Id;

					// Check xem người dùng có phải là admin không..
					bool hasRoleAdmin = data.Any(n => n.RoleId == 1);
					Session["RoleAdmin"] = hasRoleAdmin ? data.Where(n => n.RoleId == 1) : null;
					return RedirectToAction("Index");
				}
				else
				{
					ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác.");
				}
			}
			return View();
		}

		//Logout
		public ActionResult Logout()
		{
			Session.Clear();//remove session
			return RedirectToAction("Login");
		}

	}
}