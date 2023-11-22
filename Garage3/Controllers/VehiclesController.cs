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
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

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
        public IActionResult MemberCreate()
        {
            return View();
        }

        // POST: Vehicles/MemberCreate
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MemberCreate([Bind("Id, PersonNo, FirstName, LastName")] Member member)
        {
            if (ModelState.IsValid)
            {
                _db.Add(member);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(MembersOverview));
            }
            return View(member);
        }



        //##################################################################################################




        // GET: MembersOverview
        public async Task<IActionResult> MembersOverview()
        {
            var members = await _db.Members.Select(m => new MemberShowViewModel 
                                    { 
                                        Id = m.Id,
                                        PersonNo = m.PersonNo,
                                        FirstName = m.FirstName,
                                        LastName = m.LastName,
                                        NoOfVehicles = m.Vehicles.Select(v => new { v.Id,}).Count()
                                    })
                .ToListAsync();

            var model = new SearchFilterSortViewModel
            {
                SearchFilterSortMembers = members
            };
            return View(model);
        }


        public async Task<IActionResult> Sorting(string sortOrder  )
        {
           

            var members = _db.Members.AsNoTracking()
               
                .Select(m => new MemberShowViewModel
                {
                    Id = m.Id,
                    PersonNo = m.PersonNo,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    NoOfVehicles = m.Vehicles.Count,
                    Vehicles = m.Vehicles
                });
            
            

            switch (sortOrder)
            {
                case "name_desc":
                    members = members.OrderByDescending(s => s.FirstName);
                    break;

                default:
                    members = members.OrderBy(s => s.FirstName);
                    break;
            }
          

            var searchFilterSortViewModel = new SearchFilterSortViewModel
            {
                SearchFilterSortMembers = await members.ToListAsync(),
               NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : ""


        };

            return View("MembersOverview", searchFilterSortViewModel);
        }
        





    }
}
