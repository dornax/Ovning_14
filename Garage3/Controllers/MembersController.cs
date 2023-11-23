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
    public class MembersController : Controller
    {
        private readonly Garage3Context _db;

        public MembersController(Garage3Context context)
        {
            _db = context;
        }

        public IActionResult Park(int? id)

        {
            if (id == null || _db.Members == null)
            {
                return NotFound();
            }

            bool garageFreeSpace = _db.ParkingSpaces.Any(p => p.InUse == false);

            if (!garageFreeSpace)
            {
                TempData["garageIsFull"] = "No places left in garage.";
                return RedirectToAction("Index", "Vehicles");
            }

            TempData["memberId"] = id;

            return RedirectToAction("Park", "Vehicles");
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            var members= await _db.Members.Select(m => new MemberShowViewModel
            {
                Id = m.Id,
                PersonNo = m.PersonNo,
                FirstName = m.FirstName,
                LastName = m.LastName,
                NoOfVehicles = m.Vehicles.Select(v => new { v.Id, }).Count()
            })
             .ToListAsync();
            var model = new SearchFilterSortViewModel
            {
                SearchFilterSortMembers = members
            };
            return View(model);
        }
        public async Task<IActionResult> Sorting(string sortOrder)
        {


            var members = _db.Members.AsNoTracking()

                .Select(m => new MemberShowViewModel
                {
                    Id = m.Id,
                    PersonNo = m.PersonNo,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    NoOfVehicles = m.Vehicles.Count,
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
            return View("Index", searchFilterSortViewModel);
        }


        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
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
                    Type = v.VehicleType.Type,
                    Make = v.Make,
                    Model = v.Model,
                    Year = v.Year,
                    Color = v.Color,
                    NumberOfWheels = v.NumberOfWheels,
                })
            }).FirstOrDefaultAsync(m => m.Id == id);


            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonNo,FirstName,LastName")] Member member)
        {
            if (ModelState.IsValid)
            {
                _db.Add(member);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Members == null)
            {
                return NotFound();
            }

            var member = await _db.Members.Select(m => new Member
            {
                Id = m.Id,
                PersonNo = m.PersonNo,
                FirstName = m.FirstName,
                LastName = m.LastName,
            }).FirstOrDefaultAsync(m => m.Id == id);

            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PersonNo,FirstName,LastName")] Member member)
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
                    if (!MemberExists(member.Id))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Members == null)
            {
                return NotFound();
            }


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
                    Type = v.VehicleType.Type,
                    Make = v.Make,
                    Model = v.Model,
                    Year = v.Year,
                    Color = v.Color,
                    NumberOfWheels = v.NumberOfWheels,
                })
            }).FirstOrDefaultAsync(m => m.Id == id);


            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Members == null)
            {
                return Problem("Entity set 'Garage3Context.Members'  is null.");
            }
            var member = await _db.Members.FindAsync(id);
            if (member != null)
            {
                _db.Members.Remove(member);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
          return (_db.Members?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
