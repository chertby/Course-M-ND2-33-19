using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Htp.ITnews.Web.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {

        }

        public IActionResult OnGetTest()
        {
            var result = "test";
            return new JsonResult(result);
        }

        //public IActionResult OnPostTest(string value)
        //{
        //    return new JsonResult(value);
        //}

        public IActionResult OnPostTest()
        {
            var result = "test";
            return new JsonResult(result);
        }

        public IActionResult OnPostUpdateValue(string name, string value)
        {
            var success = false;
            var msg = "server error";

            //Update data to database 
            //using (MyDatabaseEntities dc = new MyDatabaseEntities())
            //{
            //    var user = dc.SiteUsers.Find(id);
            //    if (user != null)
            //    {
            //        dc.Entry(user).Property(propertyName).CurrentValue = value;
            //        dc.SaveChanges();
            //        status = true;
            //    }
            //    else
            //    {
            //        message = "Error!";
            //    }
            //}

            //success = true;
            value = "*" + value + "*";

            var response = new { value = value, success = success, msg = msg };
            //JObject o = JObject.FromObject(response);
            //return Content(o.ToString());

            return new JsonResult(response);

        }
    }
}
