using AutoMapper;
using BookManagement.Application.Features.Books.Commands.AddBook;
using EventBus.Messages.Events;

namespace BookManagement.API.Mapping
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<AddBookCommand, BookAddedEvent>().ReverseMap();
        }
    }
}
