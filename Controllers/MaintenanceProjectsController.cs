using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Models;
using System.Linq;
using System.Threading.Tasks;

public class MaintenanceProjectsController : Controller
{
    private readonly ApplicationDbContext _context;

    public MaintenanceProjectsController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index() =>
        View(await _context.MaintenanceProjects.Include(m => m.Property).ToListAsync());

    public IActionResult Create()
    {
        ViewBag.PropertyID = new SelectList(_context.Properties, "PropertyID", "PropertyName");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProjectID,PropertyID,ProjectTitle,BidAmount,Status,AssignedVendor")] MaintenanceProjects project)
    {
        if (ModelState.IsValid)
        {
            _context.Add(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.PropertyID = new SelectList(_context.Properties, "PropertyID", "PropertyName");
        return View(project);
    }

    // GET: MaintenanceProjects/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var project = await _context.MaintenanceProjects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        // Pre-select the current property in the dropdown
        ViewBag.PropertyID = new SelectList(_context.Properties, "PropertyID", "PropertyName", project.PropertyID);
        return View(project);
    }

    // POST: MaintenanceProjects/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProjectID,PropertyID,ProjectTitle,BidAmount,Status,AssignedVendor")] MaintenanceProjects project)
    {
        if (id != project.ProjectID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(project);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaintenanceProjectExists(project.ProjectID))
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

        // Repopulate dropdown if validation fails
        ViewBag.PropertyID = new SelectList(_context.Properties, "PropertyID", "PropertyName", project.PropertyID);
        return View(project);
    }

    // GET: MaintenanceProjects/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var project = await _context.MaintenanceProjects
            .Include(m => m.Property) // Includes the related property data
            .FirstOrDefaultAsync(m => m.ProjectID == id);

        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    // GET: MaintenanceProjects/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var project = await _context.MaintenanceProjects
            .Include(m => m.Property)
            .FirstOrDefaultAsync(m => m.ProjectID == id);

        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    // POST: MaintenanceProjects/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var project = await _context.MaintenanceProjects.FindAsync(id);
        if (project != null)
        {
            _context.MaintenanceProjects.Remove(project);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool MaintenanceProjectExists(int id)
    {
        return _context.MaintenanceProjects.Any(e => e.ProjectID == id);
    }

}