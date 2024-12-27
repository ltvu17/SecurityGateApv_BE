using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SecurityGateApv.Domain.Enums;
using SecurityGateApv.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Infras.BackgroundWorker
{
    public class VisitStatusUpdaterService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<VisitStatusUpdaterService> _logger;
        private Timer _timer;

        public VisitStatusUpdaterService(IServiceProvider serviceProvider, ILogger<VisitStatusUpdaterService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var now = DateTime.Now;
            var scheduledTime = new DateTime(now.Year, now.Month, now.Day, 21, 00, 0);

            if (now > scheduledTime)
            {
                scheduledTime = scheduledTime.AddDays(1);
            }

            var initialDelay = scheduledTime - now;
            _timer = new Timer(async _ => await UpdateVisitStatusesAsync(), null, initialDelay, TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }
        //protected override Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    var now = DateTime.Now;
        //    var scheduledTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0).AddMinutes(10 - (now.Minute % 10));

        //    if (now > scheduledTime)
        //    {
        //        scheduledTime = scheduledTime.AddMinutes(10);
        //    }

        //    var initialDelay = scheduledTime - now;
        //    _timer = new Timer(async _ => await UpdateVisitStatusesAsync(), null, initialDelay, TimeSpan.FromMinutes(10));

        //    return Task.CompletedTask;
        //}

        private async Task UpdateVisitStatusesAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var visitRepo = scope.ServiceProvider.GetRequiredService<IVisitRepo>();
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    var visitsToUpdate = await visitRepo.FindAsync(v => v.ExpectedEndTime <= DateTime.Now && v.VisitStatus == VisitStatusEnum.Active.ToString(),
                        int.MaxValue, 1
                    );

                    foreach (var visit in visitsToUpdate)
                    {
                        visit.UpdateStatusBackGroundWoker(VisitStatusEnum.Inactive.ToString());
                        await visitRepo.UpdateAsync(visit);
                    }

                    await unitOfWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating visit statuses in the database.");
                    throw; // Re-throw the exception to be caught by the outer try-catch block
                }
            }
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
