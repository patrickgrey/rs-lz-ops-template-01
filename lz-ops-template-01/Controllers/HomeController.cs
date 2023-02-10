using lz_ops_template_01.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace lz_ops_template_01.Controllers
{
  public class HomeController : Controller
  {
    //Hosted web API REST Service base url
    string Baseurl = "https://www.thecocktaildb.com/api/";
    public async Task<ActionResult> Index()
    {
      Response CocktailData = new Response();
      Drink firstDrink = new Drink();
      using (var client = new HttpClient())
      {
        //Passing service base url
        client.BaseAddress = new Uri(Baseurl);
        client.DefaultRequestHeaders.Clear();
        //Define request data format
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //Sending request to find web api REST service resource GetAllCocktailViewModels using HttpClient
        HttpResponseMessage Res = await client.GetAsync("json/v1/1/lookup.php?i=11007");
        //Checking the response is successful or not which is sent using HttpClient
        if (Res.IsSuccessStatusCode)
        {
          //Storing the response details recieved from web api
          var CocktailResponse = Res.Content.ReadAsStringAsync().Result;
          //Deserializing the response recieved from web api and storing into the CocktailViewModel list
          CocktailData = JsonConvert.DeserializeObject<Response>(CocktailResponse);
          firstDrink = CocktailData.Drinks[0];
          //System.Diagnostics.Debug.WriteLine("HIYAAAAAA");
          //System.Diagnostics.Debug.WriteLine(firstDrink);
          ViewBag.StrDrink = firstDrink.StrDrink;
          ViewBag.StrGlass = firstDrink.StrGlass;
          ViewBag.StrInstructions = firstDrink.StrInstructions;
        }
        else
        {
          ViewBag.StrDrink = "API call failed";
          ViewBag.StrGlass = "API call failed";
          ViewBag.StrInstructions = "API call failed";
        }
        //returning the employee list to view
        return View();
      }
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your application description page.";

      return View();
    }

    public ActionResult Contact()
    {
      ViewBag.Message = "Your contact page.";

      return View();
    }
  }
}