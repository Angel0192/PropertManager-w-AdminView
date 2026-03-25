using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Models;
using System.Linq;
using System.Threading.Tasks;

public class InvoicesController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public InvoicesController(ApplicationDbContext context) => _context = context;

    // GET: Invoices
    public async Task<IActionResult> Index() =>
        View(await _context.Invoices.Include(i => i.Project).Include(i => i.RentSchedule).ToListAsync());

    // GET: Invoices/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var invoice = await _context.Invoices
            .Include(i => i.Project)
            .Include(i => i.RentSchedule)
            .FirstOrDefaultAsync(m => m.InvoiceID == id);

        if (invoice == null) return NotFound();

        return View(invoice);
    }

    // GET: Invoices/Create
    public IActionResult Create()
    {
        ViewBag.ProjectID = new SelectList(_context.MaintenanceProjects, "ProjectID", "ProjectTitle");
        ViewBag.ScheduleID = new SelectList(_context.RentSchedules, "ScheduleID", "Status");
        return View();
    }

    // POST: Invoices/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("InvoiceID,ProjectID,ScheduleID,InvoiceDate,TotalAmount,IsExported")] Invoices invoice)
    {
        if (ModelState.IsValid)
        {
            _context.Add(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.ProjectID = new SelectList(_context.MaintenanceProjects, "ProjectID", "ProjectTitle", invoice.ProjectID);
        ViewBag.ScheduleID = new SelectList(_context.RentSchedules, "ScheduleID", "Status", invoice.ScheduleID);

        return View(invoice);
    }

    // GET: Invoices/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var invoice = await _context.Invoices.FindAsync(id);
        if (invoice == null) return NotFound();

        ViewBag.ProjectID = new SelectList(_context.MaintenanceProjects, "ProjectID", "ProjectTitle", invoice.ProjectID);
        ViewBag.ScheduleID = new SelectList(_context.RentSchedules, "ScheduleID", "Status", invoice.ScheduleID);
        return View(invoice);
    }

    // POST: Invoices/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("InvoiceID,ProjectID,ScheduleID,InvoiceDate,TotalAmount,IsExported")] Invoices invoice)
    {
        if (id != invoice.InvoiceID) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(invoice);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(invoice.InvoiceID ?? 0)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewBag.ProjectID = new SelectList(_context.MaintenanceProjects, "ProjectID", "ProjectTitle", invoice.ProjectID);
        ViewBag.ScheduleID = new SelectList(_context.RentSchedules, "ScheduleID", "Status", invoice.ScheduleID);
        return View(invoice);
    }

    // GET: Invoices/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var invoice = await _context.Invoices
            .Include(i => i.Project)
            .Include(i => i.RentSchedule)
            .FirstOrDefaultAsync(m => m.InvoiceID == id);

        if (invoice == null) return NotFound();

        return View(invoice);
    }

    // POST: Invoices/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var invoice = await _context.Invoices.FindAsync(id);
        if (invoice != null)
        {
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool InvoiceExists(int id)
    {
        return _context.Invoices.Any(e => e.InvoiceID == id);
    }
}