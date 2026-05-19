using Campanhas.Service.Microservice.Application.Campaigns.DTOs;
using MediatR;

namespace Campanhas.Service.Microservice.Application.Campaigns.Queries;

public record GetPublicActiveCampaignsQuery : IRequest<IEnumerable<PublicCampaignDto>>;