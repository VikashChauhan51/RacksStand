
using RacksStand.Api.Core.Domain.Exceptions;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Repositories;
using RacksStand.Models;

namespace RacksStand.Api.Core.Services;
public class PlanService : IPlanService
{
    private readonly IPlanRepository _planRepository;
    private readonly ILogger<PlanService> _logger;

    public PlanService(IPlanRepository planRepository, ILogger<PlanService> logger)
    {
        _planRepository = planRepository ?? throw new ArgumentNullException(nameof(planRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    }

    public async Task<Result<PlanDetailsModel>> GetPlanAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Fetching plan {PlanId}", id);
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The plan ID can't be null.");
            return Result<PlanDetailsModel>.Fail(new ArgumentNullException(nameof(id)));
        }
        var plan = await _planRepository.GetPlanWithFeaturesAsync(PlanId.Of(id), cancellationToken);

        if (plan == null)
        {
            _logger.LogWarning("The plan for ID:{Id} is not found.", id);
            return Result<PlanDetailsModel>.Fail(new EntityNotFoundException(nameof(id)));
        }

        return Result<PlanDetailsModel>.Succ
        (
            new PlanDetailsModel
             (
                 Id: plan.Id.Value,
                 Name: plan.Name,
                 Price: plan.Price.Value,
                 SupportLevel: plan.SupportLevel,
                 Description: plan.Description,
                 PlanFeatures: plan.PlanFeatures
                 .Select(static f => new PlanFeatureDetailsModel
                 (
                     FeatureId: f.FeatureId.Value,
                     Name: f.Feature.Name,
                     Code: f.Feature.Code,
                     Description: f.Feature.Description,
                     Value: f.Value
                 ))
             )
        );
    }
    public async Task<IEnumerable<PlanDetailsModel>> GetPlansAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Fetching plans");

        var plans = await _planRepository.GetPlansWithFeaturesAsync(cancellationToken);

        return plans.Select(static x => new PlanDetailsModel
        (
            Id: x.Id.Value,
            Name: x.Name,
            Price: x.Price.Value,
            SupportLevel: x.SupportLevel,
            Description: x.Description,
            PlanFeatures: x.PlanFeatures
            .Select(static f => new PlanFeatureDetailsModel
            (
                FeatureId: f.FeatureId.Value,
                Name: f.Feature.Name,
                Code: f.Feature.Code,
                Description: f.Feature.Description,
                Value: f.Value
            ))
        ));
    }
}
