using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Models;

namespace PropertyManager.Controllers
{
    public class RentSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentSchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RentSchedules
        [cite_start]// Displays all schedules and joins the Tenant table to show names [cite: 115, 122]
        public async Task<IActionResult> Index()
        {
            var schedules = await _context.RentSchedules
                .Include(r => r.Tenant)
                .ToListAsync();
            return View(schedules);
        }

        // GET: RentSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var rentSchedule = await _context.RentSchedules
                .Include(r => r.Tenant)
                .FirstOrDefaultAsync(m => m.ScheduleID == id);

            if (rentSchedule == null) return NotFound();

            return View(rentSchedule);
        }

        // GET: RentSchedules/Create
        public IActionResult Create()
        {
            [cite_start]// Populates the dropdown with Tenant LastNames 
            ViewData["TenantID"] = new SelectList(_context.Tenants, "TenantID", "LastName");
            return View();
        }

        // POST: RentSchedules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleID,TenantID,DueDate,Status,BaseRent,LateFeeAccrued,ReminderCount")] RentSchedules rentSchedule)
        {
            [cite_start]if (ModelState.IsValid) // [cite: 117, 123]
            {
                _context.Add(rentSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TenantID"] = new SelectList(_context.Tenants, "TenantID", "LastName", rentSchedule.TenantID);
            return View(rentSchedule);
        }

        // GET: RentSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var rentSchedule = await _context.RentSchedules.FindAsync(id);
            if (rentSchedule == null) return NotFound();

            ViewData["TenantID"] = new SelectList(_context.Tenants, "TenantID", "LastName", rentSchedule.TenantID);
            return View(rentSchedule);
        }

        // POST: RentSchedules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleID,TenantID,DueDate,Status,BaseRent,LateFeeAccrued,ReminderCount")] RentSchedules rentSchedule)
        {
            if (id != rentSchedule.ScheduleID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentScheduleExists(rentSchedule.ScheduleID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TenantID"] = new SelectList(_context.Tenants, "TenantID", "LastName", rentSchedule.TenantID);
            return View(rentSchedule);
        }

        // GET: RentSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var rentSchedule = await _context.RentSchedules
                .Include(r => r.Tenant)
                .FirstOrDefaultAsync(m => m.ScheduleID == id);

            if (rentSchedule == null) return NotFound();

            return View(rentSchedule);
        }

        // POST: RentSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentSchedule = await _context.RentSchedules.FindAsync(id);
            if (rentSchedule != null)
            {
                _context.RentSchedules.Remove(rentSchedule);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RentScheduleExists(int id)
        {
            return _context.RentSchedules.Any(e => e.ScheduleID == id);
        }
    }
}