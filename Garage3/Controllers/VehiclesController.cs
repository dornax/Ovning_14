using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3.Data;
using Garage3.Models.Entities;
using Microsoft.Identity.Client;
using Garage3.Models.ViewModels;

namespace Garage3.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Garage3Context _context;

        public VehiclesController(Garage3Context context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var garage3Context = _context.Vehicles.Include(v => v.Member).Include(v => v.ParkingSpace).Include(v => v.VehicleType);
            return View(await garage3Context.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Member)
                .Include(v => v.ParkingSpace)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id");
            ViewData["ParkingSpaceId"] = new SelectList(_context.ParkingSpaces, "Id", "Id");
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleType,RegistrationNo,Make,Model,Year,Color,NumberOfWheels,TimeOfArrival,MemberId,VehicleTypeId,ParkingSpaceId")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", vehicle.MemberId);
            ViewData["ParkingSpaceId"] = new SelectList(_context.ParkingSpaces, "Id", "Id", vehicle.ParkingSpaceId);
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", vehicle.MemberId);
            ViewData["ParkingSpaceId"] = new SelectList(_context.ParkingSpaces, "Id", "Id", vehicle.ParkingSpaceId);
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleType,RegistrationNo,Make,Model,Year,Color,NumberOfWheels,TimeOfArrival,MemberId,VehicleTypeId,ParkingSpaceId")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "Id", vehicle.MemberId);
            ViewData["ParkingSpaceId"] = new SelectList(_context.ParkingSpaces, "Id", "Id", vehicle.ParkingSpaceId);
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Member)
                .Include(v => v.ParkingSpace)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicles == null)
            {
                return Problem("Entity set 'Garage3Context.Vehicles'  is null.");
            }
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return (_context.Vehicles?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}