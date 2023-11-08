using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage2.Data;
using Garage2.Models;
using Garage2.Models.ViewModels;
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

        
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["RegistrationNumberSortParm"] = String.IsNullOrEmpty(sortOrder) ? "RegistrationNumber_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var ParkedVehicle = from s in _context.ParkedVehicle
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                ParkedVehicle = ParkedVehicle.Where(s => s.RegistrationNumber.Contains(searchString));
                                       //|| s.VehicleType.Contains(searchString));
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
            return View("Index", await ParkedVehicle.AsNoTracking().ToListAsync());
        }

        
      
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

                //if (await _context.ParkedVehicle.AnyAsync(v => v.RegistrationNumber == parkedVehicle.RegistrationNumber))
                if (await DoesRegNoExistsAsync(parkedVehicle.RegistrationNumber))
                {
                    ModelState.AddModelError("RegistrationNumber", "Vehicle is alredy in the garage.");
                }
                else
                {
                    parkedVehicle.TimeOfArrival = DateTime.Now;
                    _context.Add(parkedVehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(parkedVehicle);
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsRegNoAvailable(string registrationNumber)
        {
            if (await DoesRegNoExistsAsync(registrationNumber))
            {
                return Json(false);
            }

            return Json(true);
        }

        private async Task<bool> DoesRegNoExistsAsync(string registrationNumber)
        {
            return await _context.ParkedVehicle.AnyAsync(v => v.RegistrationNumber == registrationNumber);
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

            var parkedVehicle = await _context.ParkedVehicle.Select(v => new EditViewModel
            {
                ParkedVehicleId = v.ParkedVehicleId,
                RegistrationNumber = v.RegistrationNumber,
                VehicleType = v.VehicleType,
                ExistingRegNo = v.RegistrationNumber,
                Make = v.Make,
                Model = v.Model,
                Year = v.Year,
                Color = v.Color,
                NumberOfWheels = v.NumberOfWheels
            }).FirstOrDefaultAsync(v => v.ParkedVehicleId == id);

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
        //public async Task<IActionResult> Edit(int id, [Bind("ParkedVehicleId,VehicleType,RegistrationNumber,Make,Model,Year,Color,NumberOfWheels")] ParkedVehicle parkedVehicle)
        public async Task<IActionResult> Edit(int id, EditViewModel parkedVehicle)
        {
            if (id != parkedVehicle.ParkedVehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (parkedVehicle.RegistrationNumber == parkedVehicle.ExistingRegNo)
                    {
                        var v = new ParkedVehicle
                        {
                            ParkedVehicleId = parkedVehicle.ParkedVehicleId,
                            VehicleType = parkedVehicle.VehicleType,
                            RegistrationNumber = parkedVehicle.RegistrationNumber,
                            Make = parkedVehicle.Make,
                            Model = parkedVehicle.Model,
                            Year = parkedVehicle.Year,
                            Color = parkedVehicle.Color,
                            NumberOfWheels = parkedVehicle.NumberOfWheels
                        };
                        _context.Update(v);
                        _context.Entry(v).Property(v => v.TimeOfArrival).IsModified = false;
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        if (await DoesRegNoExistsAsync(parkedVehicle.RegistrationNumber))
                        {

                            ModelState.AddModelError("RegistrationNumber", "Vehicle is alredy in the garage.");
                        }
                        else
                        {
                            var v = new ParkedVehicle
                            {
                                ParkedVehicleId = parkedVehicle.ParkedVehicleId,
                                VehicleType = parkedVehicle.VehicleType,
                                RegistrationNumber = parkedVehicle.RegistrationNumber,
                                Make = parkedVehicle.Make,
                                Model = parkedVehicle.Model,
                                Year = parkedVehicle.Year,
                                Color = parkedVehicle.Color,
                                NumberOfWheels = parkedVehicle.NumberOfWheels
                            };
                            _context.Update(v);
                            _context.Entry(v).Property(v => v.TimeOfArrival).IsModified = false;
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }

                    }
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

        public async Task<IActionResult> GenerateReceipt(int id)
        {
            // Hitta det parkerade fordonet med det specificerade ID:t
            var parkedVehicle = await _context.ParkedVehicle.FirstOrDefaultAsync(m => m.ParkedVehicleId == id);

            // Om inget fordon hittas med det ID:t, returnera NotFound-resultat
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            // Skapa en ny ViewModel för kvittot och fyll i information från fordonet
            var receiptViewModel = new ReceiptViewModel
            {
                ParkedVehicleId = parkedVehicle.ParkedVehicleId,
                VehicleType = parkedVehicle.VehicleType,
                RegistrationNumber = parkedVehicle.RegistrationNumber,
                TimeOfArrival = parkedVehicle.TimeOfArrival,
            };

            // Anropa metoden som sätter avresetid till nuvarande tid
            receiptViewModel.SetDepartureTime();


            // Beräkna tid och pris baserat på pris- och tid
            receiptViewModel.CalculateTimeAndPrice();

            // Ta bort fordonet från databasen efter att kvittot är generat
            _context.ParkedVehicle.Remove(parkedVehicle);
            await _context.SaveChangesAsync();

            // Returnera kvittovyn med den skapade ViewModel
            return View("Receipt", receiptViewModel);
        }
    }
}
