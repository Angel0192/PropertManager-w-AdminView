public class WorkLogsController : Controller
{
    private readonly ApplicationDbContext _context;
    public WorkLogsController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index() => 
        View(await _context.WorkLogs.Include(w => w.Project).ToListAsync());

    public IActionResult Create()
    {
        ViewBag.ProjectID = new SelectList(_context.MaintenanceProjects, "ProjectID", "ProjectTitle");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("LogID,ProjectID,ClockInTime,ClockOutTime,GPSLocation,MaterialsUsed")] WorkLogs workLog)
    {
        if (ModelState.IsValid)
        {
            _context.Add(workLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(workLog);
    }
}