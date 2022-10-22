using System.Runtime.CompilerServices;
using Isu.Extra.Exceptions;

namespace Isu.Extra;

public interface IDayBuilder
{
    INumberOfLessontBuilder WithDay(int day);
}

public interface INumberOfLessontBuilder
{
    INameOfSubjectBuilder WithNumber(int number);
}

public interface INameOfSubjectBuilder
{
    INameOfTeacher WithSubject(string subject);
}

public interface INameOfTeacher
{
    INumberOfAuditory WithNameOfTeacher(string teacher);
}

public interface INumberOfAuditory
{
    ILessonBuilder WithNumberOfAuditory(int auditory);
}

public interface ILessonBuilder
{
    Lesson Build();
}

public class Lesson
{
    private int _day;
    private int _numberOfLesson;
    private string _nameOfSubject = null!;
    private string _nameOfTeacher = null!;
    private int _numberOfAuditory;

    public static IDayBuilder Builder => new LessonBuilder();

    public bool Intersect(Lesson secondLesson)
    {
        return this.GetDay() == secondLesson.GetDay() &&
               this.GetNumberOfLesson == secondLesson.GetNumberOfLesson;
    }

    public int GetDay() => _day;
    public int GetNumberOfLesson() => _numberOfLesson;
    public string GetNameOfSubject() => _nameOfSubject;
    public string GetNameOfTeacher() => _nameOfTeacher;
    public int GetNumberOfAuditory() => _numberOfAuditory;
    private class LessonBuilder : IDayBuilder, INumberOfLessontBuilder, INameOfSubjectBuilder, INameOfTeacher, INumberOfAuditory, ILessonBuilder
    {
        private Lesson _lesson = new Lesson();

        public INumberOfLessontBuilder WithDay(int day)
        {
            if (day is < 0 or > 6)
            {
                throw new WrongDayException(day);
            }

            _lesson._day = day;
            return this;
        }

        public INameOfSubjectBuilder WithNumber(int number)
        {
            if (number < 1 || number > 8)
            {
                throw new WrongNumberOfLessonException(number);
            }

            _lesson._numberOfLesson = number;
            return this;
        }

        public INameOfTeacher WithSubject(string subject)
        {
            _lesson._nameOfSubject = subject;
            return this;
        }

        public INumberOfAuditory WithNameOfTeacher(string teacher)
        {
            _lesson._nameOfTeacher = teacher;
            return this;
        }

        public ILessonBuilder WithNumberOfAuditory(int auditory)
        {
            if (auditory > 9999 || auditory < 1000)
            {
                throw new WrongNumberOfAuditoryException(auditory);
            }

            _lesson._numberOfAuditory = auditory;
            return this;
        }

        public Lesson Build()
        {
            return _lesson;
        }
    }
}