using Foodie.Service.EmailApi.Data;
using Foodie.Service.EmailApi.Model;
using Foodie.Service.ShoppingCartAPI.Model.DTO;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Foodie.Service.EmailApi.ExternalServices
{
    public class EmailService : IEmailService
    {
        private DbContextOptions<ApplicationDbContext> _dbOptions;

        public EmailService(DbContextOptions<ApplicationDbContext> dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public async Task EmailCartAndLog(CartDto cartDto)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine("<br/>Cart email Requested");
            message.AppendLine("<br/>Total "+cartDto.CartHeaderDto.CartTotal);
            message.AppendLine("<br/>");
            message.AppendLine("<ul>");
            foreach(var item in cartDto.CartDetailsDto)
            {
                message.AppendLine("<li>");
                message.AppendLine(item.Product.Name + " x " + item.Count);
                message.AppendLine("</li>");
            }
            message.AppendLine("</ul>");
            string email = "sampleEmailaddress";
            await LogAndEmail(message.ToString(),email);
        }
        private async Task<bool> LogAndEmail(string message, string email)
        {
            try
            {
                EmailLogger emailLogger = new()
                {
                    Email = email,
                    Message = message,
                    EmailSent = DateTime.Now,

                };
                await using var _db = new ApplicationDbContext(_dbOptions);
                await _db.EmailLoggers.AddAsync(emailLogger);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { 
             return false;
            }
            
        }
        public async Task RegisterUserEmailAndLog(string email)
        {
            string message = "User Registeration Successful. <br/> Email : " + email;
            await LogAndEmail(message, "foodie@gmail.com");
        }
    }
}
