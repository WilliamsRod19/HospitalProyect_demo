using HospitalProyect.Models;
using HospitalProyect.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalProyect.Controllers
{
	public class StaffController : Controller
	{
		private readonly StaffRepository _staffRepository;
		private readonly StaffCategoryRepository _staffCategoryRepository;
		private readonly SpecialtyRepository _specialtyRepository;

		// Inyección de dependencias: El sistema nos da el repositorio automáticamente
		public StaffController(StaffRepository staffRepository, StaffCategoryRepository staffCategoryRepository, SpecialtyRepository specialtyRepository)
		{
			_staffRepository = staffRepository;
			_staffCategoryRepository = staffCategoryRepository;
			_specialtyRepository = specialtyRepository;
		}

		// GET: Listar todo el personal
		public IActionResult Index()
		{
			var staffList = _staffRepository.GetAll();
			return View(staffList);
		}

		// GET: Formulario para crear
		public IActionResult Create()
		{
			var specialties = _specialtyRepository.GetAll();
			var staffCategories = _staffCategoryRepository.GetAll();

			ViewBag.Specialties = new SelectList(specialties, nameof(SpecialtyModel.Id), nameof(SpecialtyModel.Name));
			ViewBag.StaffCategories = new SelectList(staffCategories, nameof(StaffCategoryModel.Id), nameof(StaffCategoryModel.Name));


			return View();
		}

		// POST: Guardar el personal
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(StaffModel staff)
		{
			if (!ModelState.IsValid)
			{
				_staffRepository.Add(staff);
				return RedirectToAction(nameof(Index));
			}
			return View(staff);
		}

		// GET: Editar
		public IActionResult Edit(int id)
		{
			var staff = _staffRepository.GetById(id);
			if (staff == null) return NotFound();

			var specialties = _specialtyRepository.GetAll();
			var staffCategories = _staffCategoryRepository.GetAll();

			ViewBag.Specialties = new SelectList(specialties, nameof(SpecialtyModel.Id), nameof(SpecialtyModel.Name), staff.SpecialtyId);
			ViewBag.StaffCategories = new SelectList(staffCategories, nameof(StaffCategoryModel.Id), nameof(StaffCategoryModel.Name), staff.StaffCategoryId);

			return View(staff);
		}

		// POST: Guardar cambios de edición
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(StaffModel staff)
		{
			if (!ModelState.IsValid)
			{
				_staffRepository.Update(staff);
				return RedirectToAction(nameof(Index));
			}
			return View(staff);
		}

		public IActionResult Delete(int id)
		{
			var staff = _staffRepository.GetById(id);
			if (staff == null) return NotFound();
			return View(staff);
		}

		// POST: Eliminar
		[HttpPost]
		public IActionResult Delete(StaffModel staffModel)
		{
			_staffRepository.Delete(staffModel.Id);
			return RedirectToAction(nameof(Index));
		}
	}
}
