using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsBackend.Models;

namespace SportsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsPersonController : ControllerBase
    {

        private static List<SportsInfoModel> sportsPersons = new List<SportsInfoModel>
        {
             new SportsInfoModel
                {
                    Id = 1,
                    SportsName = "Cricket",
                    SportsPersonName = "MSD",
                    Age = 35,
                    City = "Ranchi",
                    Followers = 14
                }
        };
        private readonly DataContext dataContext;

        public SportsPersonController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]

        public async Task<ActionResult<List<SportsInfoModel>>> Get()
        {
            return Ok(await this.dataContext.SportsInfo.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<SportsInfoModel>>> AddSportsPerson(SportsInfoModel sportsInfoModel)
        {
            this.dataContext.SportsInfo.Add(sportsInfoModel);
            await this.dataContext.SaveChangesAsync();

            return Ok(await this.dataContext.SportsInfo.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SportsInfoModel>> Get(int id)
        {
            var sportsPerson = await this.dataContext.SportsInfo.FindAsync(id);
            if(sportsPerson == null)
            {
                return BadRequest("Sports Person Not Found");
            }

            return Ok(sportsPerson);
        }

        [HttpPut]

        public async Task<ActionResult<List<SportsInfoModel>>> UpdateSportsInfo(SportsInfoModel sportsInfoModel)
        {
            var dbsportsPerson = await this.dataContext.SportsInfo.FindAsync(sportsInfoModel.Id);
            if(dbsportsPerson == null)
                return BadRequest("Sports Person Not Found");

            dbsportsPerson.SportsName = sportsInfoModel.SportsName;
            dbsportsPerson.SportsPersonName = sportsInfoModel.SportsPersonName;
            dbsportsPerson.Age = sportsInfoModel.Age;
            dbsportsPerson.City = sportsInfoModel.City;
            dbsportsPerson.Followers = sportsInfoModel.Followers;

            await this.dataContext.SaveChangesAsync();

            return Ok(await this.dataContext.SportsInfo.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SportsInfoModel>>> Delete(int id)
        {
            var dbsportsPerson = await this.dataContext.SportsInfo.FindAsync(id);
            if (dbsportsPerson == null)
            {
                return BadRequest("Sports Person Not Found");
            }

            this.dataContext.SportsInfo.Remove(dbsportsPerson);
            await this.dataContext.SaveChangesAsync();

            return Ok(await this.dataContext.SportsInfo.ToListAsync());
        }
    }
}
