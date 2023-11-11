using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using System.Collections.Generic;

namespace Mvc.Infrastructure.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await GetCategories());
        }


        public async Task<List<CategoryViewModel>> GetCategories()
        {
            string apiUrl = "https://localhost:7191/api/CategoryV1";

            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            //using (HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(apiUrl);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //    HttpResponseMessage response = await client.GetAsync(apiUrl);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        var data = await response.Content.ReadAsStringAsync();
            //        //categories = data.ToList();

            //    }
            //}

            return categories;
        }
    }
}
