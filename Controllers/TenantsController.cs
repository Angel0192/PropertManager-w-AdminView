using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Models; 

namespace PropertyManager.Controllers
{
    public class TenantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TenantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tenants = await _context.Tenants.Include(t => t.Property).ToListAsync();
            return View(tenants);
        }

        public IActionResult Create()
        {
            ViewBag.PropertyList = new SelectList(_context.Properties, "PropertyID", "PropertyName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantID,FirstName,LastName,Email,PhoneNum,PropertyID")] Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.PropertyList = new SelectList(_context.Properties, "PropertyID", "PropertyName", tenant.PropertyID);
            return View(tenant);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant == null) return NotFound();

            ViewBag.PropertyList = new SelectList(_context.Properties, "PropertyID", "PropertyName", tenant.PropertyID);
            return View(tenant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TenantID,FirstName,LastName,Email,PhoneNum,PropertyID")] Tenant tenant)
        {
            if (id != tenant.TenantID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantExists(tenant.TenantID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.PropertyList = new SelectList(_context.Properties, "PropertyID", "PropertyName", tenant.PropertyID);
            return View(tenant);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tenant = await _context.Tenants
                .Include(t => t.Property)
                .FirstOrDefaultAsync(m => m.TenantID == id);

            if (tenant == null) return NotFound();

            return View(tenant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant != null)
            {
                _context.Tenants.Remove(tenant);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TenantExists(int id)
        {
            return _context.Tenants.Any(e => e.TenantID == id);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tenant = await _context.Tenants
                .Include(t => t.Property) // You need this to see the building name!
                .FirstOrDefaultAsync(m => m.TenantID == id);

            if (tenant == null) return NotFound();

            return View(tenant);
        }
    }
}