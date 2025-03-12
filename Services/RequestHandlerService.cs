using DataAccess.Models;
using DataAccess.Repository.Abstract;

namespace LessonNetCore.Services
{
    public class RequestHandlerService
    {
        private IStudentRepository _studentRepository;
        public RequestHandlerService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IResult> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAll();
            return Results.Json(students);
        }
        public async Task<IResult> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetById(id);

            if (student == null)
                return Results.NotFound();

            return Results.Json(student);
        }
        public async Task<IResult> CreateStudentAsync(IFormCollection form)
        {
            var student = new Student()
            {
                Name = form["name"],
                Surname = form["surname"]
            };

            var id = await _studentRepository.Create(student);
            return Results.Ok(id);
        }
    }
}
