using Isu.Entities;
using Isu.Extra;
using Isu.Extra.Models;

namespace Isu.Extra.Services;

public interface IIsuExtra
{
    public IReadOnlyList<Division> GetListOfDivision(ElectiveModule course);

    public IReadOnlyList<ExtraStudent> GetListOfStudentsInDivision(Division division);

    public IReadOnlyList<ExtraStudent> GetListOfStudentsIsNotInCourse(ExtraGroup group);

    public TimeTable SignUpForCourse(ExtraStudent student, ElectiveModule course);

    public TimeTable DeleteReservation(ExtraStudent student, Division division);
}