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
    public class ScheduleUserStatusUpdateService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ScheduleUserStatusUpdateService> _logger;
        private Timer _timer;

        public ScheduleUserStatusUpdateService(IServiceProvider serviceProvider, ILogger<ScheduleUserStatusUpdateService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var now = DateTime.Now;
            var scheduledTime = new DateTime(now.Year, now.Month, now.Day, 22, 00, 0);
            //var scheduledTime = new DateTime(now.Year, now.Month, now.Day, 17, 37, 0);
            if (now > scheduledTime)
            {
                scheduledTime = scheduledTime.AddDays(1);
            }
            var initialDelay = scheduledTime - now;
            _timer = new Timer(async _ => await UpdateScheduleUserStatusAsync(), null, initialDelay, TimeSpan.FromDays(1));
            return Task.CompletedTask;

        }
        private async Task UpdateScheduleUserStatusAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                try
                {
                    var scheduleUserRepo = scope.ServiceProvider.GetRequiredService<IScheduleUserRepo>();
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    var scheduleUserToUpdate = await scheduleUserRepo.FindAsync(v => v.AssignTime <= DateTime.Now && v.Status == ScheduleUserStatusEnum.Assigned.ToString(),
                        int.MaxValue, 1
                    );

                    foreach (var scheduleUser in scheduleUserToUpdate)
                    {
                        scheduleUser.UpdateStatusBackGroundWoker(ScheduleUserStatusEnum.Expired.ToString());
                        await scheduleUserRepo.UpdateAsync(scheduleUser);
                    }
                    await unitOfWork.CommitAsync();

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating visit statuses");
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
