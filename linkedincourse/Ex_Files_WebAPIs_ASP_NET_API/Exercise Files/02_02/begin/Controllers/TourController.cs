using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExploreCalifornia.DataAccess;
using ExploreCalifornia.DataAccess.Models;
using ExploreCalifornia.DTOs;

namespace ExploreCalifornia.Controllers
{
    public class TourController : ApiController
    {
        AppDataContext _context = new AppDataContext();

        [HttpGet]
        public List<Tour> GetAllTours ([FromUri]bool freeOnly = false)
        {
            var query = _context.Tours.AsQueryable();

            if (freeOnly) query = query.Where(i => i.Price == 0.0m);

            return query.ToList();
        }

        public Tour Get(int id)
        {
            var item = _context.Tours
                .FirstOrDefault(i => i.TourId == id);

            return item;
        }

        
        public Tour Get(string name)
        {
            var item = _context.Tours
                .FirstOrDefault(i => i.Name.Contains(name));

            return item;
        }

        [HttpPost]
        public List<Tour> SearchTours([FromBody]TourSearchRequestDto request)
        {
            if (request.MinPrice > request.MaxPrice)
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("MinPrice must be less than MaxPrice")
                });

            var query = _context.Tours
                .Where(i => i.Price <= request.MaxPrice 
                              && i.Price >= request.MinPrice);

            return query.ToList();
        }
        
        [HttpPut]
        public IHttpActionResult Put(int id, Tour dto)
        {
            return Ok($"Put {id} {dto.Name}");
        }

        [HttpPatch]
        public IHttpActionResult Patch()
        {
            return Ok("Patch");
        }

        [HttpDelete]
        public IHttpActionResult Delete()
        {
            return Ok("Delete");
        }
    }
}