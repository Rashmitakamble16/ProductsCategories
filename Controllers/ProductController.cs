using ASP.NET_MVC_Project.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ASP.NET_MVC_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;   //?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var products = _context.Products.Include(p => p.Category);
            var totalProducts = products.Count();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
            var productsOnPage = products.OrderBy(p => p.ProductId)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(productsOnPage);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel createProductView)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(createProductView);
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", createProductView.CategoryId);
            return View(createProductView);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var product = _context.Products
                .Include(p => p.Category) // Ensure you include the Category if you want to display category details
                .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                _context.Update(product).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
