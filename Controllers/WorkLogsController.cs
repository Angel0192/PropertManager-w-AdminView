using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Models;

public class WorkLogsController : Controller
{
    private readonly ApplicationDbContext _context;
    public WorkLogsController(ApplicationDbContext context) => _context = context;

    // GET: WorkLogs
    public async Task<IActionResult> Index() =>
        View(await _context.WorkLogs.Include(w => w.Project).ToListAsync());

    // GET: WorkLogs/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var workLog = await _context.WorkLogs
            .Include(w => w.Project)
            .FirstOrDefaultAsync(m => m.LogID == id);

        if (workLog == null) return NotFound();

        return View(workLog);
    }

    // GET: WorkLogs/Create
    public IActionResult Create()
    {
        ViewBag.ProjectID = new SelectList(_context.MaintenanceProjects, "ProjectID", "ProjectTitle");
        return View();
    }

    // POST: WorkLogs/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("LogID,ProjectID,ClockInTime,ClockOutTime,GPSLocation,MaterialsUsed,ProofPhotoURL,VendorSignature")] WorkLogs workLog)
    {
        if (ModelState.IsValid)
        {
            _context.Add(workLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.ProjectID = new SelectList(_context.MaintenanceProjects, "ProjectID", "ProjectTitle", workLog.ProjectID);
        return View(workLog);
    }

    // GET: WorkLogs/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var workLog = await _context.WorkLogs.FindAsync(id);
        if (workLog == null) return NotFound();

        ViewBag.ProjectID = new SelectList(_context.MaintenanceProjects, "ProjectID", "ProjectTitle", workLog.ProjectID);
        return View(workLog);
    }

    // POST: WorkLogs/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("LogID,ProjectID,ClockInTime,ClockOutTime,GPSLocation,MaterialsUsed,ProofPhotoURL,VendorSignature")] WorkLogs workLog)
    {
        if (id != workLog.LogID) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(workLog);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkLogExists(workLog.LogID)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewBag.ProjectID = new SelectList(_context.MaintenanceProjects, "ProjectID", "ProjectTitle", workLog.ProjectID);
        return View(workLog);
    }

    // GET: WorkLogs/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var workLog = await _context.WorkLogs
            .Include(w => w.Project)
            .FirstOrDefaultAsync(m => m.LogID == id);

        if (workLog == null) return NotFound();

        return View(workLog);
    }

    // POST: WorkLogs/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var workLog = await _context.WorkLogs.FindAsync(id);
        if (workLog != null)
        {
            _context.WorkLogs.Remove(workLog);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool WorkLogExists(int id)
    {
        return _context.WorkLogs.Any(e => e.LogID == id);
    }
}