using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.User.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailComfirmationRepository _emailComfirmationRepository;

        public ConfirmEmailCommandHandler(IUserRepository userRepository, IEmailComfirmationRepository emailComfirmationRepository)
        {
            _userRepository = userRepository;
            _emailComfirmationRepository = emailComfirmationRepository;
        }

        public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var confirmation = await _emailComfirmationRepository.GetByIdAsync(request.ConfirmationId);

            if (confirmation is null)
                throw new DatabaseValidationException("Confirmation not found!");

            var dbUser = await _userRepository.GetSingleAsync(i=>i.EmailAddress== confirmation.NewEmailAdress);

            if(dbUser is null)
                throw new DatabaseValidationException("User not found with this email!");

            if (dbUser.EmailConfrimed)
                throw new DatabaseValidationException("Email address is already confirmed!");

            dbUser.EmailConfrimed = true;
            await _userRepository.UpdateAsync(dbUser);

            return true;
        }
    }

}
