using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap.BookStore.dto;

namespace Leap.BookStore.Service
{
    public interface ITaskService
    {
        Task<IEnumerable<BookDto>> GetBooks(bool isAsc, int pageNumber, int bookPerPage);
        Task<bool> SaveOrder(OrderDto dto);
        Task<IEnumerable<SearchBookDDto>> SearchBook(string title);
    }
}
