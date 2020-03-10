using AutoMapper;
using DailyPlanner.Dal;
using DailyPlanner.Dto.Calendars;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public class EventsService : IEventsService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<Event> _eventsRepository;
        private readonly IRepository<EventSharing> _eventSharingsRepository;
        private readonly IUnitOfWork _uof;

        public EventsService(
            IRepository<Event> eventsRepository,
            IRepository<EventSharing> eventSharingsRepository,
            IRepository<Tag> tagRepository,
            IUnitOfWork uof,
            IMapper mapper)
        {
            _eventsRepository = eventsRepository;
            _eventSharingsRepository = eventSharingsRepository;
            _uof = uof;
            _mapper = mapper;
            _tagRepository = tagRepository;
        }

        public async Task<IEnumerable<EventDto>> GetAsync(Guid userId)
        {
            var eventsCreatedByUser = await _eventsRepository
                .GetAsync(
                    e => e.CreatorId == userId,
                    0,
                    int.MaxValue);

            var eventSharingsForReceiving = await _eventSharingsRepository.GetAsync(
                es => es.ReceiverId == userId,
                0,
                int.MaxValue);

            var receivingEventIds = eventSharingsForReceiving
                .Select(e => e.EventId)
                .Distinct();

            var eventsReceivedByUser = await _eventsRepository
                .GetAsync(
                    e => receivingEventIds.Contains(e.Id),
                    0,
                    int.MaxValue);

            var eventSummary = eventsCreatedByUser.Union(eventsReceivedByUser);

            var tags = await _tagRepository.GetAsync();

            var eventSummaryDtos = new List<EventDto>();
            foreach (var evt in eventSummary)
            {
                var dto = _mapper.Map<EventDto>(evt);
                dto.Tag = tags.First(e => e.Id == evt.TagId).Description;
                eventSummaryDtos.Add(dto);
            }

            return eventSummaryDtos;
        }

        public async Task<IEnumerable<EventDto>> UpsertAsync(IEnumerable<EventDto> eventDtos)
        {
            var newlyCreatedEvents = new List<Event>();

            var tags = await _tagRepository.GetAsync();

            foreach (var eventDto in eventDtos)
            {
                var note = _mapper.Map<Event>(eventDto);
                note.TagId = tags.First(t => t.Description == eventDto.Tag).Id;

                newlyCreatedEvents.Add(_eventsRepository.Upsert(note));
            }

            await _uof.CommitAsync();

            var dtos = new List<EventDto>();
            foreach (var createdNote in newlyCreatedEvents)
            {
                dtos.Add(
                    _mapper.Map<EventDto>(
                        createdNote,
                        opt => opt.AfterMap(
                            (src, dest) =>
                            {
                                var d = dest as EventDto;
                                d.Tag = tags.First(e => e.Id == (src as Event).TagId).Description;
                            }
                        ))
                    );
            }

            return dtos;
        }

        public async Task RemoveAsync(IEnumerable<Guid> eventIds)
        {
            var eventsProhibitedToRemove =
                await _eventSharingsRepository.GetAsync(
                    es => eventIds.Contains(es.EventId),
                    0,
                    int.MaxValue);

            if (eventsProhibitedToRemove.Any())
            {
                throw new InvalidOperationException("Cannot remove event, it has been shared");
            }

            foreach (var eventId in eventIds)
            {
                await _eventsRepository.RemoveAsync(eventId);
            }

            await _uof.CommitAsync();
        }
    }
}
