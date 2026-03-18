using AutoMapper;
using TestAudisoft.Application.Abstractions.Persistence;
using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Application.Professor.Dtos;
using TestAudisoft.Entities;
using TestAudisoft.Enums;

namespace TestAudisoft.Application.Professor.UseCases.ProfessorBusiness
{
    public class ProfessorBusiness : IProfessorBusiness
    {
        private readonly IProfessorRepository _professorRepository;
        private readonly ICommonRepository _commonRepository;
        private readonly IGradesRepository _gradesRepository;
        private readonly IMapper _mapper;

        public ProfessorBusiness(IMapper mapper, IProfessorRepository professorRepository, ICommonRepository commonRepository, IGradesRepository gradesRepository)
        {
            _mapper = mapper;
            _professorRepository = professorRepository;
            _commonRepository = commonRepository;
            _gradesRepository = gradesRepository;
        }

        async Task<PagedResult<ProfessorDto>> IProfessorBusiness.GetAll(StudentFilter filter)
        {
            PagedResult<ProfessorEntity> result = await _professorRepository.GetAll(filter);

            return new PagedResult<ProfessorDto>
            {
                Items = _mapper.Map<IEnumerable<ProfessorDto>>(result.Items),
                TotalRecords = result.TotalRecords,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        async Task<ProfessorDto?> IProfessorBusiness.GetById(int id)
        {
            ProfessorEntity? professor = await _professorRepository.GetById(id);

            if (professor is null)
                return null;

            return _mapper.Map<ProfessorDto>(professor);
        }

        async Task<DbActions> IProfessorBusiness.CreateProfessor(ProfessorCreateDto professor)
        {
            CommonUtilities.ValidatePerson(professor);

            if (await _commonRepository.CheckIfEmailExists(professor.Email))
                throw new BusinessException(ErrorType.CorreoExistente, "El correo ya se encuentra registrado.");

            return await _professorRepository.CreateProfessor(_mapper.Map<ProfessorEntity>(professor));
        }

        async Task<DbActions> IProfessorBusiness.UpdateProfessor(ProfessorUpdateDto professor)
        {
            CommonUtilities.ValidatePersonUpdate(professor, ErrorType.ProfesorNoEncontrado, "El id del profesor es inválido.");

            if (await _professorRepository.GetById(professor.Id) is null)
                throw new BusinessException(ErrorType.ProfesorNoEncontrado, "El profesor no existe.");

            if (await _commonRepository.CheckIfEmailExists(professor.Email, professor.Id, false))
                throw new BusinessException(ErrorType.CorreoExistente, "El correo ya se encuentra registrado por otro usuario.");

            return await _professorRepository.UpdateProfessor(_mapper.Map<ProfessorEntity>(professor));
        }

        async Task<ProfessorWithGradesDto?> IProfessorBusiness.GetProfessorWithGrades(int professor_id)
            => _mapper.Map<ProfessorWithGradesDto>(await _professorRepository.GetByIdWithGrades(professor_id));

        async Task<DbActions> IProfessorBusiness.DeleteProfessor(int id)
        {
            IEnumerable<GradesEntity> grades = await _gradesRepository.GetByProfessorId(id);
            if (grades.Any())
                throw new BusinessException(ErrorType.ProfesorConNotas, "No se puede eliminar el profesor porque tiene notas asociadas.");

            CommonUtilities.ValidateEntityId(id, ErrorType.ProfesorNoEncontrado, "El id del profesor es inválido.");

            ProfessorEntity? professor_db = await _professorRepository.GetById(id);
            if (professor_db is null)
                throw new BusinessException(ErrorType.ProfesorNoEncontrado, "El profesor no existe.");

            return await _professorRepository.DeleteProfessor(id);
        }
    }
}
