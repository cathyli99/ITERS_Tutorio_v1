using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITERS_Tutorio_v1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITERS_Tutorio_v1.Controllers
{
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly ITERSTutoriov10Context _db;
        private readonly IStringLocalizer<CoursesController> _localizer;

        public CoursesController(ITERSTutoriov10Context db, IStringLocalizer<CoursesController> loc)
        {
            _db = db;
            _localizer = loc;
        }
        // GET: api/courses
        [HttpGet]
        public IEnumerable<ApiCourse> Get()
        {
            var course_tb = (from i in _db.TbCourses where i.IsActive && i.IsDeleted == null orderby i.JoinDate descending select i).ToList();
            var course_list= new List<ApiCourse>();

            foreach (var c in course_tb)
            {
                course_list.Add(new ApiCourse
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    SubTitle = c.SubTitle,
                    CourseCode = c.CourseCode,
                    CourseBrief = c.CourseBrief,
                    CoverImage = c.CoverImage,
                    CourseDuration = c.CourseDuration,
                    CourseDetails = c.CourseDetails,
                    CourseTags = c.CourseTags,
                    TargetAudiences = c.TargetAudiences,
                    KnowledgePoints = c.KnowledgePoints,
                    Scores = c.Scores,
                    IsRecommended = c.IsRecommended,
                    JoinDate = c.JoinDate,
                    ModifiedDate = c.ModifiedDate,
                    Instructors = "api/courses_instructors/" + c.CourseId,
                    Students = "api/courses_students" + c.CourseId
                });
            }
            return course_list;
        }

        // GET api/courses/5
        [HttpGet("{id}")]
        public ApiCourse Get(int id)
        {
            var tbCourse = (from i in _db.TbCourses where i.CourseId == id select i).FirstOrDefault();
            var apiCourse = new ApiCourse();

            if (tbCourse != null)
            {
                apiCourse.CourseId = tbCourse.CourseId;
                apiCourse.CourseName = tbCourse.CourseName;
                apiCourse.SubTitle = tbCourse.SubTitle;
                apiCourse.CourseCode = tbCourse.CourseCode;
                apiCourse.CourseBrief = tbCourse.CourseBrief;
                apiCourse.CoverImage = tbCourse.CoverImage;
                apiCourse.CourseDuration = tbCourse.CourseDuration;
                apiCourse.CourseTags = tbCourse.CourseTags;
                apiCourse.TargetAudiences = tbCourse.TargetAudiences;
                apiCourse.CourseDetails = tbCourse.CourseDetails;
                apiCourse.KnowledgePoints = tbCourse.KnowledgePoints;
                apiCourse.Scores = tbCourse.Scores;
                apiCourse.IsRecommended = tbCourse.IsRecommended;
                apiCourse.JoinDate = tbCourse.JoinDate;
                apiCourse.ModifiedDate = tbCourse.ModifiedDate;
                apiCourse.Instructors = "/api/coursesinstructors/" + tbCourse.CourseId;
                apiCourse.Students = "/api/coursesubscriptions/" + tbCourse.CourseId;
            }
            return apiCourse;
        }

        // POST api/courses
        [HttpPost]
        public void Post([FromBody]ApiCourse c)
        {
            var tbCourse = new TbCourses()
            {
                CourseName = c.CourseName,
                SubTitle = c.SubTitle,
                CourseCode = c.CourseCode,
                CourseBrief = c.CourseBrief,
                CoverImage = c.CoverImage,
                CourseDuration = c.CourseDuration,
                CourseTags = c.CourseTags,
                TargetAudiences = c.TargetAudiences,
                CourseDetails = c.CourseDetails,
                KnowledgePoints = c.KnowledgePoints,
                Scores = c.Scores,
                IsRecommended = c.IsRecommended,
                JoinDate = c.JoinDate == null ? DateTime.Now : (DateTime)c.JoinDate,
                IsActive = c.IsActive
            };

            _db.TbCourses.Add(tbCourse);
            _db.SaveChanges();

            var tbCourseAssignment = new TbCourseAssignments
            {
                InstructorId = Guid.Parse("C0A46162 - C5E2 - 43CC - AB29 - B24E650BA7C1"),
                CourseId = tbCourse.CourseId
            };
            _db.TbCourseAssignments.Add(tbCourseAssignment);
            _db.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        //public void Put(int id, [FromBody]ApiCourse course)
        public ApiCourse Put(int id, [FromBody]ApiCourse course)
        {
            var apicourse = new ApiCourse();

            var tbCourse = (from i in _db.TbCourses
                            where i.CourseId == id
                            select i).FirstOrDefault();

            if (tbCourse != null)
            {
                tbCourse.CourseId = id;
                tbCourse.CourseName = course.CourseName;
                tbCourse.SubTitle = course.SubTitle;
                tbCourse.CourseCode = course.CourseCode;
                tbCourse.CourseBrief = course.CourseBrief;
                tbCourse.CoverImage = course.CoverImage;
                tbCourse.CourseDuration = course.CourseDuration;
                tbCourse.CourseTags = course.CourseTags;
                tbCourse.TargetAudiences = course.TargetAudiences;
                tbCourse.CourseDetails = course.CourseDetails;
                tbCourse.KnowledgePoints = course.KnowledgePoints;
                tbCourse.Scores = course.Scores;
                tbCourse.IsRecommended = course.IsRecommended;
                tbCourse.IsActive = course.IsActive;
                tbCourse.Modifier = Guid.Parse("C0A46162-C5E2-43CC-AB29-B24E650BA7C1");
                tbCourse.ModifiedDate = DateTime.Now;

                _db.TbCourses.Update(tbCourse);
                _db.SaveChanges();
            }
            else
            {
                apicourse.Message = _localizer["CourseNotExists"];
            }
            return apicourse;
        }

        // DELETE api/courses/5
        [HttpDelete("{id}")]
        //public void Delete(int id)
        public ApiCourse Delete(int id)
        {
            var apicourse = new ApiCourse();

            var tbCourse = (from i in _db.TbCourses
                            where i.CourseId == id
                            select i).FirstOrDefault();

            if (tbCourse != null)
            {
                tbCourse.CourseId = id;
                tbCourse.IsDeleted = true;
                tbCourse.Modifier = Guid.Parse("C0A46162-C5E2-43CC-AB29-B24E650BA7C1");
                tbCourse.ModifiedDate = DateTime.Now;

                _db.TbCourses.Update(tbCourse);
                _db.SaveChanges();
            }
            else
            {
                apicourse.Message = _localizer["CourseNotExists"];
            }

            return apicourse;
        }
    }
}

