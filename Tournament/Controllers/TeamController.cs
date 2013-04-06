﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tournament.Entities;
using Tournament.ViewModels;

namespace Tournament.Controllers
{
    public class TeamController : BaseController
    {
        public ActionResult Index()
        {
            var teams = RavenSession.Query<Team>().Customize(x => x.WaitForNonStaleResults(TimeSpan.FromSeconds(5))).ToList();
            var vm = Mapper.Map<IEnumerable<TeamViewModel>>(teams);
            return View(vm);
        }

        public ActionResult Detail(int id)
        {
            var model = RavenSession.Load<Team>(id);
            if (model == null)
            {
                return HttpNotFound(string.Format("Team {0} does not exist", id));
            }

            return View(Mapper.Map<TeamViewModel>(model));
        }

        //[Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var team = Mapper.Map<Team>(viewModel);
                // set the id to be the object/ to allow for incrementing id
                RavenSession.Store(team, "teams/");
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        //[Authorize]
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var model = RavenSession.Load<Team>(id);
            if (model == null)
            {
                return HttpNotFound(string.Format("Team {0} does not exist", id));
            }
            return View(Mapper.Map<TeamViewModel>(model));
        }

        //[Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeamViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var team = Mapper.Map<Team>(viewModel);
                RavenSession.Store(team);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        //[Authorize]
        [HttpGet]
        public ActionResult Delete(string id)
        {
            var model = RavenSession.Load<Team>(id);
            if (model == null)
            {
                return HttpNotFound(string.Format("Team {0} does not exist", id));
            }
            return View(Mapper.Map<TeamViewModel>(model));
        }

        //[Authorize]
        [HttpPost]
        [ActionName("delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(string id)
        {
            var model = RavenSession.Load<Team>(id);
            if (model == null)
            {
                return HttpNotFound(string.Format("Team {0} does not exist", id));
            }
            RavenSession.Delete(model);
            return RedirectToAction("Index");
        }
    }
}