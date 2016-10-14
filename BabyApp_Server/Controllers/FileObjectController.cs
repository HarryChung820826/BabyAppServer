using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BabyApp_Server.Models;
namespace BabyApp_Server.Controllers
{
    public class FileObjectController : Controller
    {
        //
        // GET: /FileObject/

        public void SaveImg(HttpPostedFileBase file, int id)
        {
            
            if (file != null && file.ContentLength > 0 && id>0) 
            {
                var fileName = Path.GetFileName(file.FileName);
                string mime = MimeMapping.GetMimeMapping(fileName);
                String mime_content = mime.Split('/')[1]; //取得副檔名
                string mappath = System.Web.HttpContext.Current.Server.MapPath("~/FileUpload");
                var path = Path.Combine(mappath, fileName);
                file.SaveAs(path);

                SaveInfoToDB(id,path,mime);
            }
        }

        public void SaveInfoToDB(int id , String path , String mime)
        {
            using (var db = new BabyAppDBDataContext())
            {
                BabyAppTB baby_tb = (from c in db.BabyAppTB where c.id == id select c).SingleOrDefault();
                baby_tb.ImgPath = path;
                baby_tb.ImgMime = mime;
                db.SubmitChanges();
            }
        }

    }
}
