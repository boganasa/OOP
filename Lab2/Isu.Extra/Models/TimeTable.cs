using System.Collections;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class TimeTable
{
   private IReadOnlyList<Lesson> _lessons;

   public TimeTable(List<Lesson> lessons)
   {
      _lessons = new List<Lesson>(lessons);
   }

   public IReadOnlyList<Lesson> GetLessons() => _lessons;

   public override bool Equals(object? obj)
   {
      if (obj is TimeTable timeTable)
      {
         foreach (Lesson lesson in _lessons)
         {
            if (!timeTable.GetLessons().Contains(lesson))
            {
               return false;
            }
         }
      }
      else
      {
         throw new IsNotTimetableException(obj);
      }

      return true;
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