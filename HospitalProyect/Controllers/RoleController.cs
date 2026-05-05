using HospitalProyect.Models;
using HospitalProyect.Repositories;
using HospitalProyect.Models;
using HospitalProyect.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalProyect.Controllers
{
	[Authorize(Roles = "admin")]
	public class RoleController : Controller
	{
		private readonly RoleRepository _roleRepository;

		public RoleController(RoleRepository roleRepository)
		{
			_roleRepository = roleRepository;
		}

		// GET: /Role/
		public IActionResult Index()
		{
			var roles = _roleRepository.GetAllRoles();
			return View(roles);
		}

		// GET: /Role/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: /Role/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(RoleModel role)
		{
			if (ModelState.IsValid)
			{
				_roleRepository.AddRole(role);
				return RedirectToAction(nameof(Index));
			}
			return View(role);
		}

		// GET: /Role/Edit/5
		public IActionResult Edit(int id)
		{
			var role = _roleRepository.GetRoleById(id);
			if (role == null) return NotFound();
			return View(role);
		}

		// POST: /Role/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(RoleModel role)
		{
			if (ModelState.IsValid)
			{
				_roleRepository.UpdateRole(role);
				return RedirectToAction(nameof(Index));
			}
			return View(role);
		}
	}
}