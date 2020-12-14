using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

       
        public IActionResult AddJob()
        {
            List<Employer> employers = context.Employers.ToList();
            List<Skill> skills = context.Skills.ToList();
            Console.WriteLine("skills count " + skills.Count);
            AddJobViewModel addJobViewModel = new AddJobViewModel(employers, skills);
            return View(addJobViewModel);
        }

        [HttpPost]
        [Route("/Home/AddJob")]
        public IActionResult AddJob(AddJobViewModel addJobViewModel, String[] selectedSkills)
        {
            if (ModelState.IsValid)
            {
                Job job = new Job
                {
                    Name = addJobViewModel.JobName,
                    EmployerId = addJobViewModel.EmployerId
                };
                context.Jobs.Add(job);
                context.SaveChanges();
                foreach (String selSkill in selectedSkills)
                {
                    JobSkill jobSkill = new JobSkill(job.Id, int.Parse(selSkill));
                    context.JobSkills.Add(jobSkill);
                }
                context.SaveChanges();
                return Redirect("/Home");
            }
            List<Employer> employers = context.Employers.ToList();
            addJobViewModel.Employers = new List<SelectListItem>();
            foreach (var employer in employers)
            {
                addJobViewModel.Employers.Add(new SelectListItem
                {
                    Value = employer.Id.ToString(),
                    Text = employer.Name
                });
            }
            addJobViewModel.Skills = context.Skills.ToList();
            return View(addJobViewModel);
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
