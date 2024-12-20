﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Funapplication.Data;
using Funapplication.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authorization;

namespace Funapplication.Controllers
{
    public class RiddlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RiddlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Riddles
        public async Task<IActionResult> Index()
        {
              return _context.Riddle != null ? 
                          View(await _context.Riddle.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Riddle'  is null.");
        }

        // GET: Riddles/ShowSearchForm
       
        //add a filter based on the category (level ): EMH
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();

        }

        // POST: Riddles/ShowSearchForm
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {

            return View("Index", await _context.Riddle.Where(j => j.RiddleQuestion.Contains(SearchPhrase)).ToListAsync());

        }

        // GET: Riddles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Riddle == null)
            {
                return NotFound();
            }

            var riddle = await _context.Riddle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (riddle == null)
            {
                return NotFound();
            }

            return View(riddle);
        }



        // GET: Riddles/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryOptions"] = new List<SelectListItem>
    {
        new SelectListItem { Value = "Easy", Text = "Easy" },
        new SelectListItem { Value = "Medium", Text = "Medium" },
        new SelectListItem { Value = "Hard", Text = "Hard" }
    };
            return View();
        }

        // POST: Riddles/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RiddleQuestion,RiddleAnswer,Category")] Riddle riddle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(riddle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryOptions"] = new List<SelectListItem>
    {
        new SelectListItem { Value = "Easy", Text = "Easy" },
        new SelectListItem { Value = "Medium", Text = "Medium" },
        new SelectListItem { Value = "Hard", Text = "Hard" }
    };
            return View(riddle);
        }


        // GET: Riddles/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Riddle == null)
            {
                return NotFound();
            }

            var riddle = await _context.Riddle.FindAsync(id);
            if (riddle == null)
            {
                return NotFound();
            }
            return View(riddle);
        }

        // POST: Riddles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RiddleQuestion,RiddleAnswer,Category")] Riddle riddle)
        {
            if (id != riddle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(riddle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiddleExists(riddle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(riddle);
        }

        // GET: Riddles/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Riddle == null)
            {
                return NotFound();
            }

            var riddle = await _context.Riddle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (riddle == null)
            {
                return NotFound();
            }

            return View(riddle);
        }

        // POST: Riddles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Riddle == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Riddle'  is null.");
            }
            var riddle = await _context.Riddle.FindAsync(id);
            if (riddle != null)
            {
                _context.Riddle.Remove(riddle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RiddleExists(int id)
        {
          return (_context.Riddle?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
