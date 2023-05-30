using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examen.Models;

namespace Examen.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly ExamenContext _context;

        public EstudiantesController(ExamenContext context)
        {
            _context = context;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index(string buscar, string filtro)
        {  
            
           
            var estudiantes = from Estudiante in _context.Estudiantes select Estudiante;
            
            if (!String.IsNullOrEmpty(buscar))
            {
                estudiantes = estudiantes.Where(e => e.Nombre!.Contains(buscar));
            }
            ViewData["FiltroHombres"] =  "SoloHombres";
       
            ViewData["FiltroMujeres"] = "SoloMujeres";

            ViewData["Todos"] = "Todos";

            switch (filtro)
            {
                case"Todos":
                    estudiantes = estudiantes.OrderBy(estudiantes => estudiantes.Nombre);
                    break;
                case "SoloHombres":
                    estudiantes = estudiantes.Where(e => e.Sexo.Equals(1));
                    break;

                case "SoloMujeres":
                    estudiantes = estudiantes.Where(e => e.Sexo.Equals(2));
                    break;
                default:
                    estudiantes = estudiantes.OrderBy(estudiantes => estudiantes.Nombre);
                    break;
            }
            return View(await estudiantes.ToListAsync());
        }

        public async Task<IActionResult> SoloHombres(string genero)
        {
            string sexo = "";
           
            
            var estudiantes = from Estudiante in _context.Estudiantes select Estudiante;

            if (!String.IsNullOrEmpty(genero))
            {
                if (genero == "Masculinos")
            {
                sexo = "1";
               estudiantes = estudiantes.Where(e => e.Sexo!.Equals(sexo));
            }   }
            return View(await estudiantes.ToListAsync());
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Estudiantes == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estudiantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cedula,Nombre,PrimerApellido,SegundoApellido,Sexo,FechaDeNacimiento,CedulaMadre,CedulaPadre,Edad")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Estudiantes == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cedula,Nombre,PrimerApellido,SegundoApellido,Sexo,FechaDeNacimiento,CedulaMadre,CedulaPadre")] Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.Id))
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
            return View(estudiante);
        }

        // GET: Estudiantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Estudiantes == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Estudiantes == null)
            {
                return Problem("Entity set 'ExamenContext.Estudiantes'  is null.");
            }
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
          return (_context.Estudiantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
