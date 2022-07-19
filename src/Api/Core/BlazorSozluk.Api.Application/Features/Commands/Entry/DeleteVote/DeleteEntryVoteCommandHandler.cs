using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote
{
    public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:SozlukConstants.VoteExcahngeName,
                exchangeType: SozlukConstants.DefaultExchanceType,
                queueName:SozlukConstants.DeleteEntryVoteQueueName,
                obj:new DeleteEntryVoteEvent()
                {
                    CreatedBy=request.UserId,
                    EntryId=request.EntryId
                });

            return await Task.FromResult(true);
        }
    }
}
