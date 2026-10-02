using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement
{
    public class StudentSearcher
    {
        /// <summary>
        /// Выполняет поиск и фильтрацию студентов
        /// </summary>
        /// <param name="sourceList">Исходный полный список студентов</param>
        /// <param name="nameQuery">Строка поиска по ФИО</param>
        /// <param name="groupQuery">Строка фильтра по группе</param>
        public List<Student> Filter(List<Student> sourceList, string nameQuery, string groupQuery)
        {
 
            if (sourceList == null) return new List<Student>();


            string cleanName = nameQuery?.Trim().ToLower() ?? "";
            string cleanGroup = groupQuery?.Trim().ToLower() ?? "";


            return sourceList.Where(student =>
            {

                bool matchesName = string.IsNullOrEmpty(cleanName) ||
                                   (student.FullName != null && student.FullName.ToLower().Contains(cleanName));

      
                bool matchesGroup = string.IsNullOrEmpty(cleanGroup) ||
                                    (student.Group != null && student.Group.ToLower().Contains(cleanGroup));

                return matchesName && matchesGroup;
            }).ToList();
        }
    }
}
