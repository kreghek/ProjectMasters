using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ProjectMasters.Data;
using ProjectMasters.Models;

namespace ProjectMasters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectMastersContext _context;

        public ProjectsController(ProjectMastersContext context)
        {
            _context = context;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<Project>> GenerateNewProject()
        {
            var random = new Random();

            var skills = _context.SkillSchemes.ToArray();
            var requiredSkills = skills.OrderBy(x => random.Next()).Take(random.Next(1, 3));

            var project = new Project { 
                Name = "Project " + Guid.NewGuid().ToString(),
                Features = Enumerable.Range(1, 10).Select(x=>new Feature { 
                    Name = "Feature " + Guid.NewGuid().ToString(),
                    Tasks = Enumerable.Range(1, 10).Select(t => new FeatureTask { 
                        Name = "Task " + Guid.NewGuid().ToString(),
                        RealValue = random.Next(5, 20) * 0.1f,
                        Estimate = 8,
                        RequiredSkills = skills.OrderBy(x => random.Next()).Take(random.Next(1, 3)).ToList()
                    }).ToList()
                }).ToList()
            };

            _context.Project.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProject()
        {
            return await _context.Project.ToListAsync();
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(long id)
        {
            var project = await _context.Project.FindAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(long id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Projects
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            _context.Project.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { id = project.Id }, project);
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Project>> DeleteProject(long id)
        {
            var project = await _context.Project.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Project.Remove(project);
            await _context.SaveChangesAsync();

            return project;
        }

        private bool ProjectExists(long id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
