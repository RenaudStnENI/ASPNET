using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP.Database;
using TP.Models;

namespace AspNet_TP01_Module05.Controllers
{
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            return View(FakeDBCat.Instance.ListeChats);
        }

        // GET: Chat/Details/5
        public ActionResult Details(int id)
        {
            Chat chat = FakeDBCat.Instance.ListeChats.FirstOrDefault(c => c.Id == id);

            return chat != null ? View(chat) : (ActionResult) RedirectToAction("Index");
        }


        // GET: Chat/Delete/5
        public ActionResult Delete(int id)
        {
            Chat chat = FakeDBCat.Instance.ListeChats.First(c => c.Id == id);

            return chat != null ? View(chat) : (ActionResult) RedirectToAction("Index");
        }

        // POST: Chat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Chat chat = FakeDBCat.Instance.ListeChats.FirstOrDefault(c => c.Id == id);

                if (chat != null)
                {
                    FakeDBCat.Instance.ListeChats.Remove(chat);
                }
            }
            catch
            {
                return View();
            }

            return RedirectToAction("Index");
        }
    }
}