using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkoutApplication.Model;


namespace WorkoutApplication.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class ExercisesController : ControllerBase
    {
        private readonly DataContext _context;

        public ExercisesController(DataContext context){
            _context=context;
        }   
        [HttpGet]
        public  IActionResult GetExercises([FromQuery] Exercise.ExerciseIntensity? intensity, [FromQuery]string? title, [FromQuery]int? minReccommendedDurationInSeconds, [FromQuery]int? maxReccommendedDurationInSeconds){
            var list=_context?.ExerciseList?.ToList();
            if(list is not null){
                if(intensity.HasValue){
                //return Ok(_context.ExerciseList?.Where(x=>x.Intensity==intensity.Value));
                    list= list?.Where(x=>x.Intensity==intensity.Value).ToList();
                }
                if(title!=null){
                //return Ok(_context.ExerciseList?.Where(x=>x.Title.ToLower().Contains(title.ToLower())));
                    list= list?.Where(x=>x.Title.ToLower().Contains(title.ToLower())).ToList();
                }
                if(minReccommendedDurationInSeconds!=null){
                //return Ok(_context.ExerciseList?.Where(x=>x.ReccommendedDurationInSeconds>=minReccommendedDurationInSeconds));
                    list= list?.Where(x=>x.ReccommendedDurationInSeconds>=minReccommendedDurationInSeconds).ToList();
                }
                if(maxReccommendedDurationInSeconds!=null){
                //return Ok(_context.ExerciseList?.Where(x=>x.ReccommendedDurationInSeconds<=maxReccommendedDurationInSeconds));
                    list= list?.Where(x=>x.ReccommendedDurationInSeconds<=maxReccommendedDurationInSeconds).ToList();
                }
            }
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetDetails(int? id){
            var exercise = _context.ExerciseList?.FirstOrDefault(e=>e.Id==id);
            if(exercise == null){

                return NotFound();
            }
            return Ok(exercise);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Exercise exercise){
            var dbExercise = _context.ExerciseList?.Find(exercise.Id);
            if(dbExercise==null){
                _context.Add(exercise);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetDetails), new{id=exercise.Id}, exercise);
            } else {
                return Conflict();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int? id, [FromBody]Exercise exercise){
            if(id!=exercise.Id || !_context.ExerciseList!.Any(e=>e.Id==id)){
                return NotFound();
            }
            _context.Update(exercise);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
