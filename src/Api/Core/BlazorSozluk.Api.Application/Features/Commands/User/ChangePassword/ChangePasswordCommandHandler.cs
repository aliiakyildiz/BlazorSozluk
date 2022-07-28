using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.User.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.UserId.HasValue)
            {
                throw new ArgumentException(nameof(request.UserId));
            }
            var dbUser = await _userRepository.GetByIdAsync(request.UserId.Value);

            if (dbUser == null)
                throw new DatabaseValidationException("User Not Found!");

            var echPass = PasswordEncryptor.Encrypt(request.OldPassword);
            if (dbUser.Password != echPass)
                throw new DatabaseValidationException("Old password wrong!");

            dbUser.Password = PasswordEncryptor.Encrypt(request.NewPassword);

            await _userRepository.UpdateAsync(dbUser);
            return true;
        }
    }
}
