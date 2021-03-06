﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace SerilogLesson.Models
{
    public interface IBookRepository
    {
        Task<IReadOnlyCollection<Book>> GetAll();
        ValueTask<Book> GetById(string id);
        Task<Book> Add(Book book);
        Task<Book> Update(Book book);
        Task Delete(string id);
    }
}