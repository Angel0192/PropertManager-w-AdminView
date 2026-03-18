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
        return View(project);
    }
}