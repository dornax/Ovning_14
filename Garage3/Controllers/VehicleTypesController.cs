using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Data;
using Garage3.Models.Entities;
using Garage3.Models.ViewModels;

namespace Garage3.Controllers
{
    public class VehicleTypesController : Controller
    {
        private readonly Garage3Context _context;

        public VehicleTypesController(Garage3Context context)
        {
            _context = context;
        }
        private bool VehicleTypeExists(int id)
        {
          return (_context.VehicleTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //Hämtar alla fordonstyper från databasen och skapar en lista av ViewModel-objekt
        public async Task<IActionResult> ListVehicleTypes()
        {
            var vehicleTypes = await _context.VehicleTypes.ToListAsync();
            var viewModelList = new List<VehicleTypeViewModel>();

            // Loopar igenom varje fordonstyp och skapar ett VehicleTypeViewModel-objekt för varje typ.
            // Därefter läggs varje skapat objekt till i listan för att användas i vyn.
            foreach (var vt in vehicleTypes)
            {
                var viewModel = new VehicleTypeViewModel
                {
                    Id = vt.Id,
                    Type = vt.Type,
                    Quantity = GetQuantityForVehicleType(vt.Id)
                };
                viewModelList.Add(viewModel);
            }
            //Skickar listan av ViewModel-objekt till vyn
            return View(viewModelList);
        }


        public int GetQuantityForVehicleType(int vehicleTypeId)
        {
            //hämtar fordonstypen från databasen inklusive dess fordon
            var vehicleType = _context.VehicleTypes
                .Include(vt => vt.Vehicles) // Inkluderar fordonen för att kunna räkna dem
                .FirstOrDefault(vt => vt.Id == vehicleTypeId);

            if (vehicleType == null) // Om fordonstypen inte finns, returnera 0
            {
                return 0;
            }

            ////Räknar antalet fordon i samlingen för den här fordonstypen
            return vehicleType.Vehicles.Count;
        }

        public IActionResult CreateVehicleType()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVehicleType([Bind("Type")] VehicleType vehicleType)
        {
            if (_context.VehicleTypes.Any(vt => vt.Type == vehicleType.Type))
            {
                ModelState.AddModelError("Type", "Type already exists.");
                return View(vehicleType);
            }
            if (ModelState.IsValid)
            {
                var newVehicleType = new VehicleType { Type = vehicleType.Type };

                _context.Add(newVehicleType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ListVehicleTypes));
            }

            return View(vehicleType);
        }


        public async Task<IActionResult> EditVehicleType(int? id)
        {
            if (id == null || _context.VehicleTypes == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.VehicleTypes.FindAsync(id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            return View(vehicleType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVehicleType(int id, [Bind("Id,Type")] VehicleType vehicleType)
        {
            if (id != vehicleType.Id)
            {
                return NotFound();
            }

            if (_context.VehicleTypes.Any(vt => vt.Id != id && vt.Type == vehicleType.Type))
            {
                ModelState.AddModelError("Type", "Type already exists.");
                return View(vehicleType);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicleType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleTypeExists(vehicleType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListVehicleTypes));
            }

            return View(vehicleType);
        }

        public async Task<IActionResult> DeleteVehicleType(int? id)
        {
            if (id == null || _context.VehicleTypes == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.VehicleTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicleType == null)
            {
                return NotFound();
            }

            return View(vehicleType);
        }

        [HttpPost, ActionName("DeleteVehicleType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deleted(int id)
        {
            if (_context.VehicleTypes == null)
            {
                return Problem("Entity set 'Garage3Context.VehicleTypes'  is null.");
            }
            var vehicleType = await _context.VehicleTypes.FindAsync(id);
            if (vehicleType != null)
            {
                _context.VehicleTypes.Remove(vehicleType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ListVehicleTypes));
        }
    }
}
