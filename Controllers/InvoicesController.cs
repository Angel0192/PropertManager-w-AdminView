public class InvoicesController : Controller
{
    private readonly ApplicationDbContext _context;
    public InvoicesController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index() => 
        View(await _context.Invoices.Include(i => i.Project).Include(i => i.RentSchedule).ToListAsync());

    public IActionResult Create()
    {
        ViewBag.ProjectID = new SelectList(_context.MaintenanceProjects, "ProjectID", "ProjectTitle");
        ViewBag.ScheduleID = new SelectList(_context.RentSchedules, "ScheduleID", "ScheduleID");
        return View();
    }

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
        return View(invoice);
    }
}