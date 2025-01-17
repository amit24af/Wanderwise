using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WDWS.Data;
using wdws.Models;

namespace WDWS.Controllers
{
    public class PasosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PasosController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        // GET: Pasos
        [Authorize(Roles = "Administrator")] 
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pasosi.ToListAsync());
        }

        // GET: Pasos/Details/5
        [Authorize(Roles = "Administrator, Klijent")] 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasos = await _context.Pasosi
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pasos == null)
            {
                return NotFound();
            }

            return View(pasos);
        }

        // GET: Pasos/Create
        [Authorize(Roles = "Klijent")] 
        public IActionResult Create()
        {
            ViewBag.Drzave = new SelectList(GetDrzave());
            return View();
        }

        // POST: Pasos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,clientID,drzavaKojaIzdaje,nacionalnost,datumIsteka,napomene,serijskiBroj")] Pasos pasos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pasos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Drzave = new SelectList(GetDrzave());
            return View(pasos);
        }

        // GET: Pasos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasos = await _context.Pasosi.FindAsync(id);
            if (pasos == null)
            {
                return NotFound();
            }
            return View(pasos);
        }

        // POST: Pasos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,clientID,drzavaKojaIzdaje,nacionalnost,datumIsteka,napomene,serijskiBroj")] Pasos pasos)
        {
            if (id != pasos.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pasos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasosExists(pasos.ID))
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
            return View(pasos);
        }

        // GET: Pasos/Delete/5
        [Authorize(Roles = "Administrator")] 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasos = await _context.Pasosi
                .FirstOrDefaultAsync(m => m.ID == id);
            if (pasos == null)
            {
                return NotFound();
            }

            return View(pasos);
        }

        // POST: Pasos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pasos = await _context.Pasosi.FindAsync(id);
            if (pasos != null)
            {
                _context.Pasosi.Remove(pasos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PasosExists(int id)
        {
            return _context.Pasosi.Any(e => e.ID == id);
        }
        private List<string> GetDrzave(){
            return new List<string> 
            {
                "Afganistan",
                "Albanija",
                "Alžir",
                "Argentina",
                "Australija",
                "Austrija",
                "Belgija",
                "Bosna i Hercegovina",
                "Brazil",
                "Bugarska",
                "Kanada",
                "Kina",
                "Kolumbija",
                "Hrvatska",
                "Češka",
                "Danska",
                "Egipat",
                "Finska",
                "Francuska",
                "Njemačka",
                "Grčka",
                "Mađarska",
                "Island",
                "Indija",
                "Indonezija",
                "Iran",
                "Irska",
                "Italija",
                "Japan",
                "Jordan",
                "Kazahstan",
                "Kenija",
                "Južna Koreja",
                "Meksiko",
                "Nizozemska",
                "Novi Zeland",
                "Norveška",
                "Pakistan",
                "Peru",
                "Poljska",
                "Portugal",
                "Rumunija",
                "Rusija",
                "Saudijska Arabija",
                "Srbija",
                "Slovačka",
                "Slovenija",
                "Južnoafrička Republika",
                "Španija",
                "Švedska",
                "Švicarska",
                "Turska",
                "Ukrajina",
                "Ujedinjeno Kraljevstvo",
                "Ujedinjeni Arapski Emirati",
                "Sjedinjene Američke Države",
                "Vijetnam",
                "Zambija",
                "Zimbabve"
            };
        }
    }
}
