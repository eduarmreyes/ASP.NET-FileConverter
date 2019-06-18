using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConvertApiDotNet;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using ConvertApiDotNet.Model;

namespace FileConverter.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    [HttpPost]
    // [ValidateAntiForgeryToken]
    public ActionResult Index(string fromFormat, string toFormat, HttpPostedFileBase file)
    {
      if (fromFormat == "")
      {
        ViewBag.Message = "From format is required";
        return View();
      }

      if (toFormat == "")
      {
        ViewBag.Message = "To format is required";
        return View();
      }

      if (file == null || file.ContentLength <= 0)
      {
        ViewBag.Message = "File is required";
        return View();
      }

      if (fromFormat == toFormat)
      {
        ViewBag.Message = "The 'from' and 'to' files types are the same.";
        return View();
      }

      ConvertApi convertApi = new ConvertApi(Properties.Settings.Default.CONVERT_API_SECRET);

      try
      {
        string path = Path.Combine(
          Server.MapPath("~/Files"),
          Path.GetFileName(file.FileName));

        file.SaveAs(path);

        /*
        
        string url = "https://v2.convertapi.com/convert/doc/to/pdf?Secret=" + Properties.Settings.Default.CONVERT_API_SECRET + "&File=" + file.InputStream;
        var handler = new HttpClientHandler();

        handler.UseCookies = false;

        using (var httpClient = new HttpClient(handler))
        {
          using (var request = new HttpRequestMessage(new HttpMethod("POST"), url))
          {
            request.Headers.TryAddWithoutValidation("Referer", "https://v2.convertapi.com/");

            request.Content = new ByteArrayContent(System.IO.File.ReadAllBytes(path));

            var response = httpClient.SendAsync(request);
          }
        }
        */

        /*
        string url = "https://v2.convertapi.com/convert/doc/to/pdf?Secret=" + Properties.Settings.Default.CONVERT_API_SECRET + "&File=" + file.InputStream;
        var client = new HttpClient();
        var requestContent = new FormUrlEncodedContent(new []
        {
          new KeyValuePair
        })
        HttpResponseMessage response = await client.PostAsync(
          url);
        */
        /*
        var response = convertApi.ConvertAsync(fromFormat, toFormat,
          new ConvertApiFileParam(@path)
        );

        ConvertApiResponse result = response.Result;

        result.SaveFile(Server.MapPath("~/Files"));
        */
      }
      catch (Exception ex)
      {
        ViewBag.Message = "ERROR: " + ex.Message.ToString();
      }
      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}