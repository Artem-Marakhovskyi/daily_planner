using AutoMapper;
using DailyPlanner.Dal;
using DailyPlanner.Dto.Chat;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Calendar;
using DailyPlanner.Entities.Chat;
using DailyPlanner.Entities.Notes;
using DailyPlanner.Entities.Reminders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IUnitOfWork _uof;
        private readonly IRepository<ReminderSharing> _reminderSharingRepository;
        private readonly IRepository<EventSharing> _eventSharingRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<Note> _noteRepository;
        private readonly IRepository<Message> _messageRepository;
        private readonly IRepository<ChatroomParticipation> _participationRepository;
        private readonly IMapper _mapper;

        public MessagesService(
            IRepository<Message> messageRepository,
            IRepository<ChatroomParticipation> participationRepository,
            IRepository<Note> noteRepository,
            IRepository<ReminderSharing> reminderSharingRepository,
            IRepository<Tag> tagRepository,
            IRepository<EventSharing> eventSharingRepository,
            IUnitOfWork uof,
            IMapper mapper)
        {
            _uof = uof;
            _reminderSharingRepository = reminderSharingRepository;
            _eventSharingRepository = eventSharingRepository;
            _tagRepository = tagRepository;
            _noteRepository = noteRepository;
            _messageRepository = messageRepository;
            _participationRepository = participationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MessageDto>> GetAsync(Guid chatroomId)
        {
            var tags = await _tagRepository.GetAsync();

            var messages = (await _messageRepository
                .GetAsync(e => e.ChartoomId == chatroomId, 0, int.MaxValue))
                .ToList();

            var messageDtos = new List<MessageDto>();
            foreach (var message in messages)
            {
                var tempMessage = _mapper.Map<MessageDto>(message);
                tempMessage.Tag = tags.First(e => e.Id == message.TagId).Description;

                messageDtos.Add(tempMessage);
            }

            return messageDtos;
        }

        public async Task<IEnumerable<MessageDto>> UpsertAsync(IEnumerable<MessageDto> messageDtos)
        {
            var newlyCreatedMessages = new List<Message>();
            var tags = await _tagRepository.GetAsync();

            foreach (var messageDto in messageDtos)
            {
                var message = _mapper.Map<Message>(messageDto);
                message.TagId = tags.First(e => e.Description == messageDto.Tag).Id;
                
                newlyCreatedMessages.Add(_messageRepository.Upsert(message));
            }

            await _uof.CommitAsync();

            await ShareAsync(messageDtos);

            await _uof.CommitAsync();

            var dtos = new List<MessageDto>();
            foreach (var createdMessage in newlyCreatedMessages)
            {
                var messageDto = _mapper.Map<MessageDto>(createdMessage);
                messageDto.Tag = tags.First(e => e.Id == createdMessage.TagId).Description;
                dtos.Add(messageDto);
            }

            return dtos;
        }

        private async Task ShareAsync(IEnumerable<MessageDto> messageDtos)
        {
            var chatroomIds = messageDtos.Select(e => e.ChatoomId).ToList();
            var participations = await _participationRepository.GetAsync(e => chatroomIds.Contains(e.ChatroomId), 0, int.MaxValue);
            var receiverIds = participations.Select(e => e.ParticipantId).ToList();

            var dbTasks = new List<Task>();
            foreach (var message in messageDtos)
            {
                dbTasks.Add(ShareAsync(message, receiverIds.Except(new Guid[] { message.SenderId })));
            }

            await Task.WhenAll(dbTasks);
        }

        private Task ShareAsync(MessageDto message, IEnumerable<Guid> receiverIds)
        {
            if (message.Type == MessageDtoType.Text)
            {
                // nothing to do
            }
            else if (message.Type == MessageDtoType.Event)
            {
                return ShareEventAsync(message, receiverIds);
            }
            else if (message.Type == MessageDtoType.Note)
            {
                return ShareNoteAsync(message, receiverIds);
            }
            else if (message.Type == MessageDtoType.Reminder)
            {
                return ShareReminderAsync(message, receiverIds);
            }
            else if (message.Type == MessageDtoType.Contact)
            {
                // nothing to do
            }

            return Task.CompletedTask;
        }

        private Task ShareReminderAsync(MessageDto message, IEnumerable<Guid> receiverIds)
        {
            foreach (var receiverId in receiverIds)
            {
                _reminderSharingRepository.Upsert(
                    new ReminderSharing
                    {
                         SenderId = message.SenderId,
                         ReceiverId = receiverId,
                         Id = Guid.NewGuid(),
                         CreatedAt = DateTimeOffset.UtcNow,
                         ReminderId = message.ReferenceId.Value,
                    });
            }

            return Task.CompletedTask;
        }

        private async Task ShareNoteAsync(MessageDto message, IEnumerable<Guid> receiverIds)
        {
            var referencedNote 
                = (await _noteRepository
                    .GetAsync(e => e.Id == message.ReferenceId, 0, int.MaxValue))
                    .First();

            foreach (var receiverId in receiverIds)
            {
                _noteRepository.Upsert(
                    new Note
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTimeOffset.UtcNow,
                        CreatorId = message.SenderId,
                        Description = referencedNote.Description,
                        TagId = referencedNote.TagId,
                        Title = referencedNote.Title,
                    });
            }
        }

        private Task ShareEventAsync(MessageDto message, IEnumerable<Guid> receiverIds)
        {
            foreach (var receiverId in receiverIds)
            {
                _eventSharingRepository.Upsert(
                    new EventSharing
                    {
                        CreatorId = message.SenderId,
                        ReceiverId = receiverId,
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTimeOffset.UtcNow,
                        EventId = message.ReferenceId.Value
                    });
            }

            return Task.CompletedTask;
        }
    }
}
