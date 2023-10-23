using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUnitOfWork.Core.Models;
using RepositoryPatternWithUnitOfWork.Core.Repository;

namespace Repository_Pattern_With_Unit_Of_Work.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
	private readonly IUnitOfWork _unitOfWork;
	public BookController(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}


	[HttpGet("{id}")]
	public async Task<IActionResult> GetByIdAsync(int id) => Ok(await _unitOfWork.Books.GetByIdAsync(id));

	[HttpGet("GetByBookNameAsync/{title}")]
	public async Task<IActionResult> GetByAuthorNameAsync(string title) =>
	Ok(await _unitOfWork.Books.FindAsync(x => x.Title.ToLower() == title.ToLower(), new[] { nameof(Author) }));

	[HttpGet("GetAllByBookNameAsync/{title}")]
	public async Task<IActionResult> GetAllByAuthorNameAsync(string title) =>
		Ok(await _unitOfWork.Books.FindAllAsync(x => x.Title.ToLower().Contains(title.ToLower()), new[] { nameof(Author) }));

	[HttpGet("GetOrder/{title}")]
	public async Task<IActionResult> GetOrder(string title) =>
		Ok(await _unitOfWork.Books.FindAllAsync(x => x.Title.ToLower().Contains(title.ToLower()), null, null, x => x.Id, orderByDirection: "DECS"));

	[HttpGet("GetAllBooks")]
	public async Task<IActionResult> GetAllBooks() =>
	Ok(await _unitOfWork.Books.GetAllBooks());

}
