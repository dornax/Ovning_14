using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2.Data;
using Garage2.Models;
using System.Drawing;
using Garage2.Migrations;

namespace Garage2.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Garage2Context _context;

        public ParkedVehiclesController(Garage2Context context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Search(string RegNo)//TimeOfArrival
        //{
        //    var model = _context.ParkedVehicle.Where(e => e.RegistrationNumber.StartsWith(RegNo))
        //                                 .Select(r => new OverviewViewModel
        //                                 {
        //                                     RegistrationNumber = r.RegistrationNumber,
        //                                     //TimeOfArrival
        //                                 });

        //    return View(nameof(Index), await model.ToListAsync());
        //}
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["RegistrationNumberSortParm"] = String.IsNullOrEmpty(sortOrder) ? "RegistrationNumber_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var ParkedVehicle = from s in _context.ParkedVehicle
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                ParkedVehicle = ParkedVehicle.Where(s => s.RegistrationNumber.Contains(searchString)
                                       || s.VehicleType.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "RegistrationNumber_desc":
                    ParkedVehicle = ParkedVehicle.OrderByDescending(s => s.RegistrationNumber);
                    break;
                case "Date":
                    ParkedVehicle = ParkedVehicle.OrderBy(s => s.TimeOfArrival);
                    break;
                case "date_desc":
                    ParkedVehicle = ParkedVehicle.OrderByDescending(s => s.TimeOfArrival);
                    break;
                default:
                    ParkedVehicle = ParkedVehicle.OrderBy(s => s.RegistrationNumber);
                    break;
            }
            return View("Index1", await ParkedVehicle.AsNoTracking().ToListAsync());
        }

        // GET: ParkedVehicles
        //public async Task<IActionResult> Index()
        //{
        //    var model = await _context.ParkedVehicle.Select(v => new OverviewViewModel
        //    {
        //        ParkedVehicleId = v.ParkedVehicleId,
        //        VehicleType = v.VehicleType,
        //        RegistrationNumber = v.RegistrationNumber,
        //        Make = v.Make,
        //        Model = v.Model,
        //        Color = v.Color,







        //    })

        //       // .Select()
        //       .ToListAsync();

        //    //return i list of overviewmodel to the view

        //    return View(model);

        //}
      
        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ParkedVehicle == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.ParkedVehicleId == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Park()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Park([Bind("ParkedVehicleId,VehicleType,RegistrationNumber,Make,Model,Year,Color,NumberOfWheels")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Park));
            }
            return View(parkedVehicle);
        }


        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParkedVehicleId,VehicleType,RegistrationNumber,Make,Model,Year,Color,NumberOfWheels")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ParkedVehicle == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParkedVehicleId,VehicleType,RegistrationNumber,Make,Model,Year,Color,NumberOfWheels")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.ParkedVehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.ParkedVehicleId))
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
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ParkedVehicle == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.ParkedVehicleId == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ParkedVehicle == null)
            {
                return Problem("Entity set 'Garage2Context.ParkedVehicle'  is null.");
            }
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle != null)
            {
                _context.ParkedVehicle.Remove(parkedVehicle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkedVehicleExists(int id)
        {
          return (_context.ParkedVehicle?.Any(e => e.ParkedVehicleId == id)).GetValueOrDefault();
        }
    }
}
