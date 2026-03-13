using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Application.DTOs.BookDTOS;
using FluentValidation;

namespace Books.Application.Validators
{
    public class BookValidator:AbstractValidator<BookCreateDto>
    {

        public BookValidator()
        {
            
            
            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Назва книги обов'язкова")
                .MaximumLength(200).WithMessage("Назва не може бути довшою за 200 символів");

            
            RuleFor(book => book.Year)
                .InclusiveBetween(1500, DateTime.Now.Year)
                .WithMessage($"Рік видання має бути між 1500 та {DateTime.Now.Year}")
                .NotEmpty().WithMessage("Вкажіть год книги");

            RuleFor(book => book.AuthorIds)
                .NotEmpty().WithMessage("Авторы обязательны");


            
            RuleFor(book => book.GenreId)
                .NotEmpty().WithMessage("Жанр книги обов'язкова");

            
        }
    }

}
