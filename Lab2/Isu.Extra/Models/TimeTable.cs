using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class TimeTable
{
   private IReadOnlyList<Lesson> _lessons = new List<Lesson>();

   public TimeTable(List<Lesson> lessons)
   {
      List<Lesson> testLesson = new List<Lesson>();
      foreach (var curLesson in lessons)
      {
         testLesson.Add(curLesson);
      }

      if (testLesson.Count > 1)
      {
         for (int index = 0; index < testLesson.Count; index++)
         {
            Lesson lesson1 = testLesson[index];
            testLesson.Remove(lesson1);
            foreach (Lesson lesson in testLesson)
            {
               if (lesson1.Intersect(lesson))
               {
                  throw new LessonsAreIntersectException();
               }
            }
         }
      }

      _lessons = new List<Lesson>(lessons);
   }

   public IReadOnlyList<Lesson> GetLessons() => _lessons;

   public override bool Equals(object? obj)
   {
      if (obj is TimeTable timeTable)
      {
         return _lessons.All(lesson => timeTable.GetLessons().Contains(lesson));
      }
      else
      {
         throw new IsNotTimetableException(obj);
      }
   }

   public override int GetHashCode()
   {
      return _lessons.GetHashCode();
   }

   protected bool Equals(TimeTable other)
   {
      return _lessons.Equals(other._lessons);
   }
}