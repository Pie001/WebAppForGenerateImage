using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using WebAppForGenerateImage.Helpers;

namespace WebAppForGenerateImage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 画像作成API
        /// {id}から渡されたurlのスクリーンショートの画像ファイルを返す
        /// </summary>
        /// <param name="id">{controller}/{action}/{id}のid部分(この場合はパラメータに当てはまる)</param>
        /// <returns>idのサイトurlが正しい場合、スクリーンショートの画像ファイルを返す</returns>
        [HttpGet]
        public ActionResult GenerateImage(string id)
        {
            FileContentResult result;

            if (!id.IsNullOrEmpty())
            {
                string address = "http://" + id;
                using (var memStream = new System.IO.MemoryStream())
                {
                    Bitmap bitmap = WebsiteThumbnailImageHelper.GetWebSiteThumbnail(address, 1080, 1080, 400, 400);

                    bitmap.Save(memStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    result = this.File(memStream.GetBuffer(), "image/jpeg");
                }
            }
            else
            {
                return null;
            }
            return result;
        }

    }
}