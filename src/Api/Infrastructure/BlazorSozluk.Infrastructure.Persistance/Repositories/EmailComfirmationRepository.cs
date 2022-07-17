using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infrastructure.Persistance.Repositories
{
    public class EmailComfirmationRepository : GenericRepository<EmailConfirmation>, IEmailComfirmationRepository
    {
        public EmailComfirmationRepository(BlazorSozlukContext dbContext) : base(dbContext)
        {
        }
    }
}
