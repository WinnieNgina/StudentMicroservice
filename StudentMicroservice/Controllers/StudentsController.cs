using Microsoft.AspNetCore.Mvc;
using StudentMicroservice.Dtos;
using StudentMicroservice.Services;

namespace StudentMicroservice.Controllers;

[Route("api/students")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ResponseStudentDto>), 200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllStudents()
    {
        try
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Server error");
        }
    }
    [HttpPost]
    [ProducesResponseType(typeof(ResponseStudentDto), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(500)]

    public async Task<IActionResult> RegisterStudent([FromBody] CreateStudentDto createStudentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var registeredStudent = await _studentService.AddStudentAsync(createStudentDto);
            return Ok(registeredStudent);
        }
        catch (Exception ex) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Server error", ExceptionMessage = ex.Message, StackTrace = ex.StackTrace });
        }
    }

}
