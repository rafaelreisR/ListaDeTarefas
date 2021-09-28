using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TodoMvc.Infrastructure;
using TodoMvc.Models;

namespace TodoMvc.Controllers
{
    public class ToDoController : Controller
    {

        private readonly ToDoRestClient restClient;

        public ToDoController()
        {
            this.restClient = new ToDoRestClient();
        }

        public ActionResult Index()
        {
            var model = this.restClient.GetAll();
            return View(model);
        }

        // GET: AgendamentoController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = this.restClient.GetById(id);
            return View(model);
        }

        // GET: AgendamentoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AgendamentoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToDoItemModel model)
        {
            try
            {
                this.restClient.Save(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AgendamentoController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = this.restClient.GetById(id);
            return View(model);
        }

        // POST: AgendamentoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, ToDoItemModel model)
        {
            try
            {
                this.restClient.Update(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AgendamentoController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = this.restClient.GetById(id);
            return View(model);
        }

        // POST: AgendamentoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, ToDoItemModel model)
        {
            try
            {
                this.restClient.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}