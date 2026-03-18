using AutoMapper;
using TestAudisoft.Application.Abstractions.Persistence;
using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Application.Grades.Dtos;
using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Application.Grades.UseCases.GradesBusiness
{
    public class GradesBusiness : IGradesBusiness
    {
        private readonly IGradesRepository _gradesRepository;
        private readonly ICommonRepository _commonRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IMapper _mapper;

        public GradesBusiness(IMapper mapper,IGradesRepository gradesRepository,IStudentRepository studentRepository,IProfessorRepository professorRepository, 
            ICommonRepository commonRepository)
        {
            _mapper = mapper;
            _gradesRepository = gradesRepository;
            _studentRepository = studentRepository;
            _professorRepository = professorRepository;
            _commonRepository = commonRepository;
        }

        async Task<PagedResult<GradeDto>> IGradesBusiness.GetAll(GradesFilter filter)
        {
            PagedResult<GradesEntity> result = await _gradesRepository.GetAll(filter);

            return new PagedResult<GradeDto>
            {
                Items = _mapper.Map<IEnumerable<GradeDto>>(result.Items),
                TotalRecords = result.TotalRecords,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        async Task<GradeDto?> IGradesBusiness.GetById(int id)
        {
            GradesEntity? grades = await _gradesRepository.GetById(id);

            if (grades is null)
                return null;

            return _mapper.Map<GradeDto>(grades);
        }

        async Task<DbActions> IGradesBusiness.CreateGrades(GradeCreateDto grades)
        {
            CommonUtilities.ValidateGradeBase(grades);

            if (await _commonRepository.CheckIfGradeNameExists(grades.Name))
                throw new BusinessException(ErrorType.NombreNotaExistente, "Ya existe una nota con ese nombre.");

            if (await _studentRepository.GetById(grades.StudentId) is null)
                throw new BusinessException(ErrorType.EstudianteNoEncontrado, "El estudiante asociado no existe.");

            if (await _professorRepository.GetById(grades.ProfessorId) is null)
                throw new BusinessException(ErrorType.ProfesorNoEncontrado, "El profesor asociado no existe.");

            return await _gradesRepository.CreateGrades(_mapper.Map<GradesEntity>(grades));
        }

        async Task<DbActions> IGradesBusiness.UpdateGrades(GradeUpdateDto grades)
        {
            CommonUtilities.ValidateEntityId(grades.Id, ErrorType.NotaNoEncontrada, "El id de la nota es inválido.");
            CommonUtilities.ValidateGradeBase(grades);

            if (await _gradesRepository.GetById(grades.Id) is null)
                throw new BusinessException(ErrorType.NotaNoEncontrada, "La nota no existe.");

            if (await _commonRepository.CheckIfGradeNameExists(grades.Name, grades.Id))
                throw new BusinessException(ErrorType.NombreNotaExistente, "Ya existe otra nota con ese nombre.");

            if (await _professorRepository.GetById(grades.ProfessorId) is null)
                throw new BusinessException(ErrorType.ProfesorNoEncontrado, "El profesor asociado no existe.");

            return await _gradesRepository.UpdateGrades(_mapper.Map<GradesEntity>(grades));
        }

        async Task<IEnumerable<GradeDto>> IGradesBusiness.GetByStudentId(int student_id)
        {
            if (await _studentRepository.GetById(student_id) is null)
                throw new BusinessException(ErrorType.EstudianteNoEncontrado, "El estudiante no existe.");

            return _mapper.Map<IEnumerable<GradeDto>>(await _gradesRepository.GetByStudentId(student_id));
        }

        async Task<IEnumerable<GradeDto>> IGradesBusiness.GetByProfessorId(int professor_id)
        {
            if (await _professorRepository.GetById(professor_id) is null)
                throw new BusinessException(ErrorType.ProfesorNoEncontrado, "El profesor no existe.");


            return _mapper.Map<IEnumerable<GradeDto>>(await _gradesRepository.GetByProfessorId(professor_id));
        }

        async Task<DbActions> IGradesBusiness.DeleteGrades(int id)
        {
            CommonUtilities.ValidateEntityId(id, ErrorType.NotaNoEncontrada, "El id de la nota es inválido.");

            GradesEntity? grades_db = await _gradesRepository.GetById(id);
            if (grades_db is null)
                throw new BusinessException(ErrorType.NotaNoEncontrada, "La nota no existe.");

            return await _gradesRepository.DeleteGrades(id);
        }
    }
}
