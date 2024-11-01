using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BodegaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public BodegaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Bodega bodega = new Bodega();

            if (id==null)
            {
                bodega.Status = true;
                return View(bodega);
            }

            bodega = await _unidadTrabajo.Bodega.Get(id.GetValueOrDefault());
            if (bodega==null)
            {
                return NotFound();
            }
            return View(bodega);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                if (bodega.Id == 0)
                {
                    await _unidadTrabajo.Bodega.Add(bodega);
                    TempData[DS.Exito] = "La Bodega se ha creado exitosamente";
                }
                else
                {
                    _unidadTrabajo.Bodega.Update(bodega);
                    TempData[DS.Exito] = "La bodega se ha actualizado exitosamente";
                }
                await _unidadTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Ha ocurrido un error al guardar la Bodega";
            return View(bodega);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var all = await _unidadTrabajo.Bodega.GetAll();
            return Json(new { data = all });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var bodegaDB = await _unidadTrabajo.Bodega.Get(id);
            if (bodegaDB == null)
            {
                return Json(new { success = false, message = "Error al borrar la Bodega" });
            }
            _unidadTrabajo.Bodega.Remove(bodegaDB);
            await _unidadTrabajo.Save();
            return Json(new { success = true, message = "Bodega eliminada con éxito" });
        }

        [ActionName("NameValidation")]
        public async Task<IActionResult> NameValidation(string name, int id = 0)
        {
            bool value = false;
            var list = await _unidadTrabajo.Bodega.GetAll();
            if (id == 0)
            {
                value = list.Any(b => b.Name.ToLower().Trim() == name.ToLower().Trim());
            }
            else
            {
                value = list.Any(b => b.Name.ToLower().Trim() == name.ToLower().Trim() && b.Id != id);
            }
            if (value)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });
        }

        #endregion
    }
}
