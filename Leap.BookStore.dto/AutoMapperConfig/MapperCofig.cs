using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap.BookStore.DAL;
using AutoMapper;
namespace Leap.BookStore.dto
{
    public class DtoDALMapperConfig : Profile
    {

        public DtoDALMapperConfig()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order_Items, OrderDetailsDto>().ReverseMap();
            CreateMap<IEnumerable<Order_Items>, IEnumerable<OrderDetailsDto>>().ReverseMap();

            CreateMap<Book, SearchBookDDto>()
                 .ForMember(d => d.BookID, o => o.MapFrom(s => s.book_ID))
                 .ForMember(d => d.ISBN, o => o.MapFrom(s => s.ISBN))
                 .ForMember(d => d.BookTitle, o => o.MapFrom(s => s.book_title))
                 .ForMember(d => d.CategoryCode, o => o.MapFrom(s => s.Book_Category.CategoryCode))
                 .ForMember(d => d.CodeDescription, o => o.MapFrom(s => s.Book_Category.book_category_Description)).ReverseMap();

            


        }
    }
}
