using AutoMapper;
using DailyPlanner.Dal;
using DailyPlanner.Dto.Reminders;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Reminders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public class RemindersService : IRemindersService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<Reminder> _remindersRepository;
        private readonly IRepository<ReminderSharing> _reminderSharingsRepository;
        private readonly IUnitOfWork _uof;

        public RemindersService(
            IRepository<Reminder> remindersRepository,
            IRepository<ReminderSharing> reminderSharingsRepository,
            IRepository<Tag> tagRepository,
            IUnitOfWork uof,
            IMapper mapper)
        {
            _remindersRepository = remindersRepository;
            _reminderSharingsRepository = reminderSharingsRepository;
            _uof = uof;
            _mapper = mapper;
            _tagRepository = tagRepository;
        }

        public async Task<IEnumerable<ReminderDto>> GetAsync(Guid userId)
        {
            var remindersCreatedByUser = await _remindersRepository
                .GetAsync(
                    e => e.CreatorId == userId,
                    0,
                    int.MaxValue);

            var reminderSharingsForReceiving = await _reminderSharingsRepository.GetAsync(
                es => es.ReceiverId == userId,
                0,
                int.MaxValue);

            var receivingReminderIds = reminderSharingsForReceiving
                .Select(e => e.ReminderId)
                .Distinct();

            var remindersReceivedByUser = await _remindersRepository
                .GetAsync(
                    e => receivingReminderIds.Contains(e.Id),
                    0,
                    int.MaxValue);

            var reminderSummary = remindersCreatedByUser.Union(remindersReceivedByUser);

            var tags = await _tagRepository.GetAsync();

            var reminderSummaryDtos = new List<ReminderDto>();
            foreach (var reminder in reminderSummary)
            {
                var dto = _mapper.Map<ReminderDto>(reminder);
                dto.Tag = tags.First(e => e.Id == reminder.TagId).Description;
                reminderSummaryDtos.Add(dto);
            }

            return reminderSummaryDtos;
        }

        public async Task<IEnumerable<ReminderDto>> UpsertAsync(IEnumerable<ReminderDto> reminderDtos)
        {
            var newlyCreatedReminders = new List<Reminder>();

            var tags = await _tagRepository.GetAsync();

            foreach (var reminderDto in reminderDtos)
            {
                var reminder = _mapper.Map<Reminder>(reminderDto);
                reminder.TagId = tags.First(t => t.Description == reminderDto.Tag).Id;

                newlyCreatedReminders.Add(_remindersRepository.Upsert(reminder));
            }

            await _uof.CommitAsync();

            var dtos = new List<ReminderDto>();
            foreach (var createdReminder in newlyCreatedReminders)
            {
                dtos.Add(
                    _mapper.Map<ReminderDto>(
                        createdReminder,
                        opt => opt.AfterMap(
                            (src, dest) =>
                            {
                                var d = dest as ReminderDto;
                                d.Tag = tags.First(e => e.Id == (src as Reminder).TagId).Description;
                            }
                        ))
                    );
            }

            return dtos;
        }

        public async Task RemoveAsync(IEnumerable<Guid> reminderIds)
        {
            var remindersProhibitedToRemove =
                await _reminderSharingsRepository.GetAsync(
                    es => reminderIds.Contains(es.ReminderId),
                    0,
                    int.MaxValue);

            if (remindersProhibitedToRemove.Any())
            {
                throw new InvalidOperationException("Cannot remove Reminder, it has been shared");
            }

            foreach (var reminderId in reminderIds)
            {
                await _remindersRepository.RemoveAsync(reminderId);
            }

            await _uof.CommitAsync();
        }
    }
}
