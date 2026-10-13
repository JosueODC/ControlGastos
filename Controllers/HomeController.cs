
using System.Diagnostics;
using ControlGastos.Data;
using ControlGastos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ControlGastos.Controllers;

public class HomeController : Controller
{
    private readonly GastosContext _context;

    public HomeController(GastosContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var movimientos = _context.Movimientos
        .OrderByDescending(m => m.Fecha)
        .ToList();
        decimal ingresos = movimientos
        .Where(m=>m.Tipo=="Ingreso")
        .Sum(m=>m.Cantidad);

        decimal gastos = movimientos
        .Where(m=>m.Tipo =="Gasto")
        .Sum(m=>m.Cantidad);
        ViewBag.Ingresos = ingresos;
        ViewBag.Gastos = gastos;
        ViewBag.Saldo = ingresos - gastos;

    return View(movimientos);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(Movimiento movimiento)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Movimientos.Add(movimiento);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public IActionResult Editar(int id)
    {
        var movimiento =_context.Movimientos.Find(id);
        if(movimiento==null)
        {
            return NotFound();
        }
        return View(movimiento);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(int id,Movimiento movimiento)
    {
        if(id != movimiento.id)
        {
            return NotFound();
        }
        if(!ModelState.IsValid)
        {
            return View(movimiento);
        }
        _context.Movimientos.Update(movimiento);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id
                ?? HttpContext.TraceIdentifier
        });
    }
}
