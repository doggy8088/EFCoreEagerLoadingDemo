using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversityApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversityApi.Controllers
{
    [Route("courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        public CoursesController(ContosouniversityContext _db)
        {
            db = _db;
        }

        public ContosouniversityContext db { get; }

        [HttpGet("")]
        public ActionResult<IEnumerable<Course>> Get()
        {
            return this.db.Course.ToList();
        }

        [HttpGet("{id}", Name = "GetCourseById")]
        public ActionResult<Course> GetCourse(int id)
        {
            var item = db.Course.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // /courses/1/instructors
        [HttpGet("{courseId}/instructors")]
        public ActionResult<IEnumerable<Person>> GetInstructorsByCourse(int courseId)
        {
            var item = db.Course
                .Include(p => p.CourseInstructor)
                .FirstOrDefault(p => p.CourseId == courseId);

            if (item == null)
            {
                return NotFound();
            }

            return (from p in db.Person
                    where item.CourseInstructor.Any(i => i.InstructorId == p.Id)
                    select p).ToList();
        }

        [HttpPost("")]
        public ActionResult<Course> PostCourse(Course item)
        {
            db.Course.Add(item);
            db.SaveChanges();

            //return item;
            return CreatedAtRoute("GetCourseById", new { id = item.CourseId }, item);
        }

        [HttpPut("{id}")]
        public ActionResult<Course> PutCourse(int id, Course item)
        {
            if (db.Course.Any(p => p.CourseId == id) == false)
            {
                return NotFound();
            }

            db.Course.Update(item);
            db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Course> DeleteCourse(int id)
        {
            var item = db.Course.Find(id);

            if (item == null)
            {
                return NotFound();
            }

            db.Course.Remove(item);
            db.SaveChanges();

            return item;
        }
    }
}