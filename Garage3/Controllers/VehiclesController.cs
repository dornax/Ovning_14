﻿using System;
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
    public class VehiclesController : Controller
    {
        private readonly Garage3Context _db;

        public VehiclesController(Garage3Context context)
        {
            _db = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var garage3Context = _db.Vehicles.Include(v => v.Member).Include(v => v.ParkingSpace).Include(v => v.Type);
            return View(await garage3Context.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _db.Vehicles
                .Include(v => v.Member)
                .Include(v => v.ParkingSpace)
                .Include(v => v.Type)
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
            ViewData["MemberId"] = new SelectList(_db.Members, "Id", "Id");
            ViewData["ParkingSpaceId"] = new SelectList(_db.ParkingSpaces, "Id", "Id");
            ViewData["VehicleTypeId"] = new SelectList(_db.VehicleTypes, "Id", "Id");
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
                _db.Add(vehicle);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_db.Members, "Id", "Id", vehicle.MemberId);
            ViewData["ParkingSpaceId"] = new SelectList(_db.ParkingSpaces, "Id", "Id", vehicle.ParkingSpaceId);
            ViewData["VehicleTypeId"] = new SelectList(_db.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _db.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_db.Members, "Id", "Id", vehicle.MemberId);
            ViewData["ParkingSpaceId"] = new SelectList(_db.ParkingSpaces, "Id", "Id", vehicle.ParkingSpaceId);
            ViewData["VehicleTypeId"] = new SelectList(_db.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
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
                    _db.Update(vehicle);
                    await _db.SaveChangesAsync();
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
            ViewData["MemberId"] = new SelectList(_db.Members, "Id", "Id", vehicle.MemberId);
            ViewData["ParkingSpaceId"] = new SelectList(_db.ParkingSpaces, "Id", "Id", vehicle.ParkingSpaceId);
            ViewData["VehicleTypeId"] = new SelectList(_db.VehicleTypes, "Id", "Id", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Vehicles == null)
            {
                return NotFound();
            }

            var vehicle = await _db.Vehicles
                .Include(v => v.Member)
                .Include(v => v.ParkingSpace)
                .Include(v => v.Type)
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
            if (_db.Vehicles == null)
            {
                return Problem("Entity set 'Garage3Context.Vehicles'  is null.");
            }
            var vehicle = await _db.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _db.Vehicles.Remove(vehicle);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
          return (_db.Vehicles?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        //##################################################################################################


        // GET: Vehicles/MemberCreate
        public IActionResult X_MemberCreate()
        {
            return View();
        }

        // POST: Vehicles/MemberCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> X_MemberCreate([Bind("Id, PersonNo, FirstName, LastName")] Member member)
        {
            if (ModelState.IsValid)
            {
                _db.Add(member);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(X_MembersOverview));
            }
            return View(member);
        }



        //##################################################################################################




        // GET: MembersOverview
        public async Task<IActionResult> X_MembersOverview()
        {
            var model = _db.Members.Select(m => new MemberShowViewModel 
                                    { 
                                        Id = m.Id,
                                        PersonNo = m.PersonNo,
                                        FirstName = m.FirstName,
                                        LastName = m.LastName,
                                        NoOfVehicles = m.Vehicles.Select(v => new { v.Id,}).Count()
                                    });
            return View(await model.ToListAsync());
        }



        //##################################################################################################




        // GET: MemberDetails
        public async Task<IActionResult> X_MemberDetails(int? id)
        {
            var member = await _db.Members.Select(m => new MemberShowViewModel
            {
                Id = m.Id,
                PersonNo = m.PersonNo,
                FirstName = m.FirstName,
                LastName = m.LastName,
                NoOfVehicles = m.Vehicles.Select(v => new { v.Id, }).Count(),
                Vehicles = m.Vehicles.Select(v => new MemberOwnedVehiclesViewModel 
                {
                    Id = v.Id,
                    RegistrationNo = v.RegistrationNo,
                    Make = v.Make,
                    Model = v.Model,
                    Year = v.Year,
                    Color = v.Color,
                    NumberOfWheels = v.NumberOfWheels,
                })
            }).FirstOrDefaultAsync(m => m.Id == id);

            return View(member);
        }



        //##################################################################################################




        // GET: MemberEdit
        public async Task<IActionResult> X_MemberEdit(int? id)
        {
            var member = await _db.Members.Select(m => new Member
            {
                Id = m.Id,
                PersonNo = m.PersonNo,
                FirstName = m.FirstName,
                LastName = m.LastName,
            }).FirstOrDefaultAsync(m => m.Id == id);

            return View(member);
        }



        // POST: Vehicles/MemberEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> X_MemberEdit(int id, [Bind("Id, PersonNo, FirstName, LastName")] Member member)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(member);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(member.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(X_MembersOverview));
            }
            return View(member);
        }
    }
}
