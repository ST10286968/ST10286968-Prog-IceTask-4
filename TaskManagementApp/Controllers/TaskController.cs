using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManagementApp.Controllers
{
    public class TaskController : Controller
    {
        // In-memory list to store tasks
        private static List<TaskModel> tasks = new List<TaskModel>();

        // GET: Display list of tasks
        public IActionResult Index()
        {
            return View(tasks);
        }

        // GET: Create new task
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create new task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                task.Id = tasks.Count + 1; // Auto-increment Id
                tasks.Add(task);
                return RedirectToAction("Index");
            }
            return View(task); // Return form with validation errors
        }

        // GET: Edit task by ID
        public IActionResult Edit(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound(); // Handle non-existing task gracefully
            }
            return View(task);
        }

        // POST: Edit task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                var existingTask = tasks.FirstOrDefault(t => t.Id == task.Id);
                if (existingTask == null)
                {
                    return NotFound();
                }
                existingTask.Title = task.Title;
                existingTask.Description = task.Description;
                existingTask.Deadline = task.Deadline;
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Confirm task deletion
        public IActionResult Delete(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        // POST: Delete task
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            tasks.Remove(task);
            return RedirectToAction("Index");
        }
    }
}
