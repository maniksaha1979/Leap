using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Leap.BookStore.Service;
using Leap.BookStore.dto;

namespace Leap.BookStore.WebAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("api/Task")]
    [Authorize]
    public class TaskController : ApiController
    {
        private readonly ITaskService Service;

        public TaskController(ITaskService serv)
        {
            Service = serv;
        }

        [HttpGet, Route("Two/{bool}/{page:int}/{bookperpage:int}")]
        public async Task<IHttpActionResult> GetBooksAsync(bool isAsc, int page, int bookperpage)
        {
            try
            {
                IEnumerable<BookDto> data = await Service.GetBooks(isAsc, page, bookperpage);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet, Route("Four/{title}")]
        public async Task<IHttpActionResult> GetBooksByTitleSearch(string title)
        {
            try
            {
                IEnumerable<SearchBookDDto> data = await Service.SearchBook(title);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost, Route("CreateOrder")]
        public async Task<IHttpActionResult> CreateProject([FromBody]OrderDto dto)
        {
            try
            {
                bool result = await Service.SaveOrder (dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }



    }
}
