using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyApp_Server.Models;
using BabyApp_Server.Filter;
namespace BabyApp_Server.Controllers
{
    public class BabyServerController : Controller
    {
        //
        // GET: /BabyServer/

        public ActionResult Login()
        {
            if (Request.HttpMethod == "POST" && Request["Login"] != null)
            {
                var user_name = Request["UserName"];
                var user_pwd = Request["UserPwd"];
                if (user_name == "harrychung" && user_pwd == "asdf3145")
                {
                    Session.Abandon();
                    Session.Clear();
                    Request.Cookies.Clear();
                    Response.Cookies[HttpModule.HttpModule.cookieName][HttpModule.HttpModule.User_Name] = user_name;
                    Response.Cookies[HttpModule.HttpModule.cookieName].Expires.AddHours(1);
                    //Response.AddHeader("Location", "/Index");
                    Response.Redirect("/Add");
                }
                else {
                    var msg = "<script type=\"text/javascript\">alert(\"登入失敗\"); </script>";

                    Response.Write(msg);
                }
            }
            return View();
        }

        [IndexRequireAttribute]
        public ActionResult Index()
        {

            return View();
        }

        [AddRequireAttribute]
        public ActionResult Add(IEnumerable<HttpPostedFileBase> files)
        {

            if (Request.HttpMethod == "POST" && Request["Submit"]!=null)
            {
                int id = -1;

                using (var db = new BabyAppDBDataContext())
                {
                    BabyAppTB babytb = new BabyAppTB();
                    DateTime today = Convert.ToDateTime(Request["Today"]).AddYears(1911);
                    List<BabyAppTB> search = (from c in db.BabyAppTB 
                                              where c.RecodeDateTime == today 
                                              select c).ToList();
                    if (search.Count >= 1)
                    {
                        var error_msg = "<script>alert('" + Request["Today"] + " 資料庫已有紀錄');</script>";
                        Response.Write(error_msg);
                    }
                    else
                    {
                        babytb.Context = Request["context"];
                        babytb.sendCheck = false;
                        babytb.RecodeDateTime = today;

                        db.BabyAppTB.InsertOnSubmit(babytb);
                        db.SubmitChanges();

                        id = (from c in db.BabyAppTB
                                  where c.RecodeDateTime == today
                                  select c.id).SingleOrDefault();

                        //最後儲存 照片
                        if (files != null)
                        {
                            foreach (var file in files)
                            {
                                new FileObjectController().SaveImg(file, id);
                            }
                        }
                        var success_msg = "<script>alert('" + Request["Today"] + " 儲存成功');</script>";
                        Response.Write(success_msg);
                    }
                }
            }
            return View();
        }

        //回傳 查詢到的 資料 組合成 Json 字串
        public ActionResult search()
        {
            if (Request.HttpMethod == "POST" || Request.HttpMethod == "GET")
            {
                DateTime search_date = Convert.ToDateTime(Request["Date"]);
                using (var db = new BabyAppDBDataContext())
                {
                    BabyAppTB babytb = (from c in db.BabyAppTB where c.RecodeDateTime.Equals(search_date) select c).SingleOrDefault();

                    if (babytb != null)
                    {
                        String recode_date = Convert.ToDateTime(babytb.RecodeDateTime).ToString("yyyy/MM/dd");
                        String json_str = "{";
                        json_str += "\"Date\":\"" + recode_date + "\",";
                        json_str += "\"Content\":\"" + babytb.Context + "\"";
                        json_str += "}";
                        Response.Write(json_str);

                        babytb.sendTime = DateTime.Now;
                        db.SubmitChanges();
                    }
                    else {
                        Response.Write("-1");
                    }
                }
            }

            return new EmptyResult();
        }
        //app 端 回傳 CheckCode  判斷是否成功傳至app端
        public ActionResult check()
        {
            if (Request.HttpMethod == "POST" || Request.HttpMethod == "GET")
            {
                DateTime search_date = Convert.ToDateTime(Request["Date"]);
                String check_code = Request["CheckCode"];
                try
                {
                    if (check_code == "ok")
                    {
                        using (var db = new BabyAppDBDataContext())
                        {
                            BabyAppTB babytb = (from c in db.BabyAppTB where c.RecodeDateTime.Equals(search_date) select c).SingleOrDefault();
                            babytb.sendCheck = true;
                            db.SubmitChanges();
                        }
                        Response.Write("1");
                    }
                    else {
                        Response.Write("0");
                    }
                }
                catch (Exception e)
                {
                    Response.Write("0");
                }
            }

            return new EmptyResult();
        }

        public ActionResult showImg()
        {
            if (Request.HttpMethod == "POST" || Request.HttpMethod == "GET")
            {
                using (var db = new BabyAppDBDataContext())
                {
                    DateTime search_date = Convert.ToDateTime(Request["Date"]);

                    string mime = "image/jpeg";
                    string path = (from c in db.BabyAppTB where c.RecodeDateTime.Equals(search_date) select c.ImgPath).SingleOrDefault();
                    mime = (from c in db.BabyAppTB where c.RecodeDateTime.Equals(search_date) select c.ImgMime).SingleOrDefault();
                    FileStream fs;
                    if (path == null || mime == null)
                    {
                        fs = new FileStream(Server.MapPath("~/FileUpload/NoIMG.jpg"), FileMode.Open);
                        mime = "image/jpeg";
                    }
                    else
                    {
                        fs = new FileStream(path, FileMode.Open);
                    }
                    byte[] bydata = new byte[fs.Length];
                    fs.Read(bydata, 0, bydata.Length);
                    fs.Close();
                    return File(bydata, mime);
                }
            }
            return new EmptyResult();
        }

    }
}
