using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Application.Grades.Dtos;
using TestAudisoft.Enums;

namespace TestAudisoft.Application.Common
{
    public static class CommonUtilities
    {
        public static void ValidatePerson(PersonBaseDto person)
        {
            if (string.IsNullOrWhiteSpace(person.FirstName))
                throw new BusinessException(ErrorType.NombreRequerido, "El nombre es requerido.");

            if (string.IsNullOrWhiteSpace(person.LastName))
                throw new BusinessException(ErrorType.ApellidoRequerido, "El apellido es requerido.");

            if (string.IsNullOrWhiteSpace(person.Email))
                throw new BusinessException(ErrorType.CorreoRequerido, "El correo es requerido.");
        }

        public static void ValidatePersonUpdate(PersonUpdateDto person, ErrorType not_found_error_type, string invalid_id_message)
        {
            if (person.Id <= 0)
                throw new BusinessException(not_found_error_type, invalid_id_message);

            ValidatePerson(person);
        }

        public static void ValidateEntityId(int id, ErrorType error_type, string message)
        {
            if (id <= 0)
                throw new BusinessException(error_type, message);
        }

        public static void ValidateGradeBase(GradeCreateDto grades)
        {
            if (string.IsNullOrWhiteSpace(grades.Name))
                throw new BusinessException(ErrorType.NombreNotaRequerido, "El nombre de la nota es requerido.");

            if (grades.GradeValue < 0 || grades.GradeValue > 5)
                throw new BusinessException(ErrorType.CalificacionInvalida, "La nota debe estar entre 0 y 5.");

            if (grades.StudentId <= 0)
                throw new BusinessException(ErrorType.EstudianteNoEncontrado, "El id del estudiante es inválido.");

            if (grades.ProfessorId <= 0)
                throw new BusinessException(ErrorType.ProfesorNoEncontrado, "El id del profesor es inválido.");
        }

        public static void ValidateGradeBase(GradeUpdateDto grades)
        {
            if (string.IsNullOrWhiteSpace(grades.Name))
                throw new BusinessException(ErrorType.NombreNotaRequerido, "El nombre de la nota es requerido.");

            if (grades.GradeValue < 0 || grades.GradeValue > 5)
                throw new BusinessException(ErrorType.CalificacionInvalida, "La nota debe estar entre 0 y 5.");

            if (grades.StudentId <= 0)
                throw new BusinessException(ErrorType.EstudianteNoEncontrado, "El id del estudiante es inválido.");

            if (grades.ProfessorId <= 0)
                throw new BusinessException(ErrorType.ProfesorNoEncontrado, "El id del profesor es inválido.");
        }
    }
}
