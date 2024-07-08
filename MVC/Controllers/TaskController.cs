using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult Index()
        {
            IEnumerable<mvcTaskModel> tasklist;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("task").Result;
            tasklist = response.Content.ReadAsAsync<IEnumerable<mvcTaskModel>>().Result;
            return View(tasklist);

        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new mvcTaskModel());

            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("task/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcTaskModel>().Result);
            }

        }
        [HttpPost]
        public ActionResult AddOrEdit(mvcTaskModel task)
        {
            if (task.ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("task", task).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("task/" + task.ID, task).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("task/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult UpdateCompletedStatus(int id, int completed, mvcTaskModel task)
        {
            try
            {
                
                if (task.ID == null)
                {
                    return Json(new { success = false, message = "Task not found" });
                }

                task.Completed = completed == 1;
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("task", task).Result;

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }

}