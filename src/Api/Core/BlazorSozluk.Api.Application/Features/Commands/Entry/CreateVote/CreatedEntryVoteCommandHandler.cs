using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Models.RequstModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.CreateVote
{
    public class CreatedEntryVoteCommandHandler : IRequestHandler<CreatedEntryVoteCommand, bool>
    {
        public async Task<bool> Handle(CreatedEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:SozlukConstants.VoteExcahngeName,
                exchangeType:SozlukConstants.DefaultExchanceType,
                queueName: SozlukConstants.CreateEntryVoteQueueName,
                obj: new CreateEntryVoteEvent()
                {
                    EntryId = request.EntryId,
                    CreatedBy = request.CreatedBy,
                    VoteType=request.VoteType,
                });
            return await Task.FromResult(true);
        }
    }
}
