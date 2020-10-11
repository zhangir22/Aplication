using Microsoft.Ajax.Utilities;
using Questionary.Context;
using Questionary.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionary.Controllers
{
    [Authorize]
    public class WorksheetController : Controller
    {
        // GET: Worksheet
        UserContext _userDb = new UserContext();
        TestContext _testDb = new TestContext();
        private Dictionary<string, string> Answers { get; set; } = new Dictionary<string, string>();
        private readonly string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\Questionary\Questionary\App_Data\LocalDB.mdf;Integrated Security=True;Connect Timeout=30";
        public WorksheetController()
        {
            Answers.Add("CapitalKZ", "Nur-Sultan");
            Answers.Add("CountCityKZ", "88");
            Answers.Add("LanguageKZ", "Kazakh");
            Answers.Add("DateFounded", "10.12.1991 0:00:00");
        }
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,DateComplete,CapitalKZ,CountCityKZ,LanguageKZ,DateFounded,ClientsId")] TestForUser test)
        {
            if (ModelState.IsValid)
            {
                test.DateComplete = DateTime.Now;

                string getId = $"SELECT u.Id FROM Clients u WHERE u.Name = '{User.Identity.Name}'";

                using (SqlConnection connection = new SqlConnection(connStr))
                {
                    connection.Open();

                    SqlCommand set = new SqlCommand(getId, connection);
                    test.ClientsId = (int)set.ExecuteScalar();
                    string command = $"SELECT COUNT(c.Id) FROM Clients c WHERE c.Id = {test.ClientsId}";
                    SqlCommand sqlCommand = new SqlCommand(command, connection);
                    var count = sqlCommand.ExecuteScalar();
                    if ((int)count > 1)
                    {
                        return View("ErrorView");
                    }
                    else
                    {
                        using (TestContext db = new TestContext())
                        {
                            db.Tests.Add(test);
                            db.SaveChanges();
                            return RedirectToAction("CongratulationsView", "Worksheet");

                        }
                    }
                }

            }
            return View();
        }
        //public ActionResult CongratulationsView() 
        //{
        //    return View();
        //}
            
   
        public ActionResult CongratulationsView()
        {
           ResultUser resultUser = new ResultUser();
            ArrayList result = new ArrayList();

            var item = _userDb.Users.Where(u => u.Name == User.Identity.Name).FirstOrDefault();
            var _itemTest = _testDb.Tests.Where(t => t.ClientsId == item.Id);
            int rightAnswer = 0;
            int lieAnswer = 0;
            int countQuastions = Answers.Count;
            List<string> answerUser = new List<string>();
            foreach (var temp in _itemTest)
            {
                resultUser.DateComplete = temp.DateComplete;
                answerUser.Add(temp.CapitalKZ);
                answerUser.Add(Convert.ToString(temp.CountCityKZ));
                answerUser.Add(temp.LanguageKZ);
                answerUser.Add(Convert.ToString(temp.DateFounded));
                break;
            }
            int count = -1;
            foreach (var list in Answers.Values)
            {
                count++;
                 if (list.Contains(answerUser[count]))
                 {
                        rightAnswer++;
                 }
                
            }
            lieAnswer = countQuastions - rightAnswer;
            resultUser.Name = User.Identity.Name;
            resultUser.RightAnswer = rightAnswer;
            resultUser.LieAnswer = lieAnswer;
            int tempProsent = 100 / countQuastions;
            int prosent = rightAnswer * tempProsent;
            resultUser.Porsent = prosent;

            return View(resultUser);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Input");
        }
        public ActionResult ErrorView() 
        {
            return View();
        }
    }
}
