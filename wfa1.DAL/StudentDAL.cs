using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wfa1.DAL.Entities;

namespace wfa1.DAL
{
    public class StudentDAL
    {
        private StudentModels db;
        public StudentDAL()
        {
            db = new StudentModels();
        }

        public List<Student> GetAllStudents()
        {
            return db.Students.Include("Faculty").Include("Major").ToList();
        }
        public List<Student> GetAllStudentsWithNoMajor()
        {
            return db.Students.Include("Faculty").Include("Major").Where(s => s.MajorID == null).ToList();
        }
        public List<Faculty> GetAllFaculties()
        {
            return db.Faculties.ToList();
        }

        public List<Major> GetAllMajors()
        {
            return db.Majors.ToList();
        }
        public Student GetStudentById(string studentID)
        {
            return db.Students.SingleOrDefault(s => s.StudentID == studentID);
        }

        public void AddStudent(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
        }

        public void UpdateStudent(Student student)
        {
            var existingStudent = db.Students.SingleOrDefault(s => s.StudentID == student.StudentID);
            if (existingStudent != null)
            {
                existingStudent.FullName = student.FullName;
                existingStudent.AverageScore = student.AverageScore;
                existingStudent.FacultyID = student.FacultyID;
                existingStudent.MajorID = student.MajorID;
                existingStudent.Avatar = student.Avatar;
                db.SaveChanges();
            }
        }
        public void DeleteStudent(string studentID)
        {
            var student = db.Students.SingleOrDefault(s => s.StudentID == studentID);
            if (student != null)
            {
                db.Students.Remove(student);
                db.SaveChanges();
            }
        }
    }
}
