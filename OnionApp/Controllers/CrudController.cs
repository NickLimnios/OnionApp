using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Onion.RazorUI.ViewModels;
using Onion.API.Factory;

namespace OnionApp.Controllers
{
    public class CrudController<TEntView, TEnt> : Controller
        where TEntView : IViewModel
        where TEnt : class
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            string relativePath = $"{this.RouteData.Values["controller"]}/{this.RouteData.Values["action"]}";
            var requestUrl = ApiClientFactory.Instance.CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, relativePath));

            IEnumerable<TEnt> entities = ApiClientFactory.Instance.GetAllAsync<TEnt>(requestUrl).Result;
            IEnumerable<TEntView> entitiesView = Mapper.Map<IEnumerable<TEnt>, IEnumerable<TEntView>>(entities);

            return View(entitiesView);
        }

        public IActionResult Create()
        {
            return View("Edit");
        }

        public IActionResult Update(int id)
        {
            string relativePath = $"{this.RouteData.Values["controller"]}/get";
            var requestUrl = ApiClientFactory.Instance.CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, relativePath),$"id={id}");

            TEnt entity = ApiClientFactory.Instance.GetAsync<TEnt>(requestUrl).Result;
            TEntView entityView = Mapper.Map<TEnt, TEntView>(entity);

            return View("Edit", entityView);
        }

        [HttpPost]
        public IActionResult Edit(TEntView entityView)
        {
            TEnt entity = Mapper.Map<TEntView, TEnt>(entityView);

            string relativePath = $"{this.RouteData.Values["controller"]}/";
            if (entityView.id <= 0)
                relativePath += "create"; //TODO : Parametric?
            else
                relativePath += "update"; //TODO : Parametric?

            var requestUrl = ApiClientFactory.Instance.CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, relativePath));
            TEnt response = ApiClientFactory.Instance.PostAsync<TEnt>(requestUrl, entity).Result;

            return RedirectToAction("GetAll");
        }

        public IActionResult Delete(int id)
        {
            string relativePath;
            System.Uri requestUrl;

            //get entity by id
            relativePath = $"{this.RouteData.Values["controller"]}/get";
            requestUrl = ApiClientFactory.Instance.CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, relativePath),$"id={id}");
            TEnt entity = ApiClientFactory.Instance.GetAsync<TEnt>(requestUrl).Result;
            
            //delete entity
            relativePath = $"{this.RouteData.Values["controller"]}/{this.RouteData.Values["action"]}";
            requestUrl = ApiClientFactory.Instance.CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture, relativePath));
            TEnt response = ApiClientFactory.Instance.PostAsync<TEnt>(requestUrl, entity).Result;

            return RedirectToAction("GetAll");
        }
    }
}