using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap.BookStore.DAL.Repositories;
using Leap.BookStore.DAL;
using Leap.BookStore.dto;
using AutoMapper;
using System.Data.Entity;

namespace Leap.BookStore.Service
{
    public class TaskService:ITaskService
    {
        public readonly IUnitOfWork UnitOfWork;
        private readonly IMapper BOMapper;
        private Repository<Order> OrdersRepo;
        private Repository<Order_Items> OrderItems;


        public TaskService(IUnitOfWork uow, IMapper mapper)
        {
            UnitOfWork = uow;
            BOMapper = mapper;
            OrdersRepo = new Repository<Order>(UnitOfWork.GetDataContext());
            OrderItems = new Repository<Order_Items>(UnitOfWork.GetDataContext());
        }
         
        public async Task<IEnumerable<BookDto>> GetBooks(bool isAsc, int pageNumber, int bookPerPage)
        {
            var source = UnitOfWork.GetDataContext().Set<Book>().AsQueryable();
            int count = source.Count();
            int TotalPages = (int)Math.Ceiling(count / (double)bookPerPage);

            var Tempitems = source.Skip((pageNumber - 1) * bookPerPage).Take(bookPerPage);
            var items = isAsc ? await Tempitems.OrderBy(x => x.book_title).ToListAsync() : await Tempitems.OrderByDescending(x => x.book_title).ToListAsync();
            var result = BOMapper.Map<IEnumerable<Book>, IEnumerable<BookDto>>(items);
            return result;

        }

        public async Task<bool> SaveOrder(OrderDto dto)
        {
            dto.order_Value = dto.DetailsItem.Sum(i => i.Item_Agreed_Price * i.Item_Number);
            Order orderdata = BOMapper.Map<OrderDto, Order>(dto);
            OrdersRepo.Add(orderdata);
            dto.DetailsItem.ForEach(i => {
                Order_Items orderItemw = BOMapper.Map<OrderDetailsDto, Order_Items>(i);
                OrderItems.Add(orderItemw);

            });
            int retValue = await UnitOfWork.Complete();
            return retValue > 0;
        }

        public async Task<IEnumerable<SearchBookDDto>> SearchBook(string title)
        {
            var books =await  UnitOfWork.GetDataContext().Set<Book>().Include(b => b.Book_Category)
                             .Where(b => b.book_title.ToLower().Equals(title.ToLower()) || 
                                         b.book_title.ToLower().Contains(title.ToLower()) || 
                                         b.book_title.ToLower().StartsWith(title.ToLower()) || 
                                         b.book_title.ToLower().EndsWith(title.ToLower())).ToListAsync();
            var result = BOMapper.Map<IEnumerable<Book>, IEnumerable<SearchBookDDto>>(books);

            return result;

        }
    }
    
}
