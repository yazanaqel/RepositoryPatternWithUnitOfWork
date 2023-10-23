using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUnitOfWork.Core.Models;
using RepositoryPatternWithUnitOfWork.Core.Repository;

namespace Repository_Pattern_With_Unit_Of_Work.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork;
	public AuthorController(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}


	[HttpGet("GetByIdAsync/{id}")]
	public async Task<IActionResult> GetByIdAsync(int id) => Ok(await _unitOfWork.Authors.GetByIdAsync(id));



	[HttpGet("GetAllAsync")]
	public Task<IActionResult> GetAllAsync(int page = 1, int pageSize = 10)
	{

		//for other use
		//var totalCount = _unitOfWork.Authors.GetAllAsync().Result.Count;
		//var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);

		var authorsPerPage = _unitOfWork.Authors.GetAllAsync().Result
			.Skip((page - 1) * pageSize).Take(pageSize).ToList();

		return Task.FromResult<IActionResult>(Ok(authorsPerPage));
	}


	[HttpGet("GetByAuthorNameAsync/{name}")]
	public async Task<IActionResult> GetByAuthorNameAsync(string name) =>
		Ok(await _unitOfWork.Authors.FindAsync(x => x.Name.ToLower() == name.ToLower()));


	[HttpPost("AddOneAuthor")]
	public async Task<IActionResult> AddOneAuthor(Author author)
	{
		var newAuthor = await _unitOfWork.Authors.AddAsync(author);
		await _unitOfWork.Complete();

		return Ok(newAuthor);
	}
}
