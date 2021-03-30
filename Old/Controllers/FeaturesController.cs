using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMasters.Data;
using ProjectMasters.Models;

namespace ProjectMasters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly ProjectMastersContext _context;

        public FeaturesController(ProjectMastersContext context)
        {
            _context = context;
        }

        // GET: api/Features/project
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<Feature>>> GetFeatureOfProject(int projectId)
        {
            return await _context.Project
                .Include(x=>x.Features)
                    .ThenInclude(x=>x.Tasks)
                        .ThenInclude(x=>x.RequiredSkills)
                            .ThenInclude(x=>x.SkillScheme)
                .Include(x => x.Features)
                    .ThenInclude(x => x.Tasks)
                        .ThenInclude(x => x.Assignees)
                .Where(x => x.Id == projectId).SelectMany(x => x.Features).ToListAsync();
        }
    }
}
