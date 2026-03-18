using AutoMapper;
using TestAudisoft.Application.Abstractions.Persistence;
using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Application.Student.Dtos;
using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Application.Student.UseCases.StudentBusiness
{
    public class StudentBusiness : IStudentBusiness
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICommonRepository _commonRepository;
        private readonly IGradesRepository _gradesRepository;
        private readonly IMapper _mapper;

        public StudentBusiness(IMapper mapper, IStudentRepository studentRepository, ICommonRepository commonRepository, IGradesRepository gradesRepository)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _commonRepository = commonRepository;
            _gradesRepository = gradesRepository;
        }

        async Task<PagedResult<StudentDto>> IStudentBusiness.GetAll(StudentFilter filter)
        {
            PagedResult<StudentEntity> result = await _studentRepository.GetAll(filter);

            return new PagedResult<StudentDto>
            {
                Items = _mapper.Map<IEnumerable<StudentDto>>(result.Items),
                TotalRecords = result.TotalRecords,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        async Task<StudentDto?> IStudentBusiness.GetById(int id)
        {
            StudentEntity? student = await _studentRepository.GetById(id);

            if (student is null)
                return null;

            return _mapper.Map<StudentDto>(student);
        }

        async Task<DbActions> IStudentBusiness.CreateStudent(StudentCreateDto student)
        {
            CommonUtilities.ValidatePerson(student);

            if (await _commonRepository.CheckIfEmailExists(student.Email))
                throw new BusinessException(ErrorType.CorreoExistente, "El correo ya se encuentra registrado.");

            return await _studentRepository.CreateStudent(_mapper.Map<StudentEntity>(student));
        }

        async Task<DbActions> IStudentBusiness.UpdateStudent(StudentUpdateDto student)
        {
            CommonUtilities.ValidatePersonUpdate(student, ErrorType.EstudianteNoEncontrado, "El id del estudiante es inválido.");

            if (await _studentRepository.GetById(student.Id) is null)
                throw new BusinessException(ErrorType.EstudianteNoEncontrado, "El estudiante no existe.");

            if (await _commonRepository.CheckIfEmailExists(student.Email, student.Id, true))
                throw new BusinessException(ErrorType.CorreoExistente, "El correo ya se encuentra registrado por otro usuario.");

            return await _studentRepository.UpdateStudent(_mapper.Map<StudentEntity>(student));
        }

        async Task<StudentWithGradesDto?> IStudentBusiness.GetStudentWithGrades(int student_id)
        {
            StudentEntity? student = await _studentRepository.GetByIdWithGrades(student_id);

            if (student is null)
                return null;

            return _mapper.Map<StudentWithGradesDto>(student);
        }

        async Task<DbActions> IStudentBusiness.DeleteStudent(int id)
        {
            IEnumerable<GradesEntity> grades = await _gradesRepository.GetByStudentId(id);
            if (grades.Any())
                throw new BusinessException(ErrorType.EstudianteConNotas, "No se puede eliminar el estudiante porque tiene notas asociadas.");

            CommonUtilities.ValidateEntityId(id, ErrorType.EstudianteNoEncontrado, "El id del estudiante es inválido.");

            StudentEntity? student_db = await _studentRepository.GetById(id);
            if (student_db is null)
                throw new BusinessException(ErrorType.EstudianteNoEncontrado, "El estudiante no existe.");

            return await _studentRepository.DeleteStudent(id);
        }
    }
}
