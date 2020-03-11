using AutoMapper;
using DailyPlanner.Dal;
using DailyPlanner.Dto.Chat;
using DailyPlanner.Dto.Chatroom;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public class ChatService : IChatService
    {
        private readonly IRepository<Chatroom> _chatroomRepository;
        private readonly IRepository<ChatroomParticipation> _participationRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;

        public ChatService(
            IRepository<Chatroom> chatroomRepository,
            IRepository<ChatroomParticipation> participactionRepository,
            IUnitOfWork uof,
            IMapper mapper)
        {
            _chatroomRepository = chatroomRepository;
            _participationRepository = participactionRepository;
            _mapper = mapper;
            _uof = uof;
        }

        public async Task<List<ChatroomParticipationDto>> AddParticipantsAsync(IEnumerable<ChatroomParticipationDto> chatroomParticipationDtos)
        {
            foreach (var participation in chatroomParticipationDtos)
            {
                _participationRepository.Upsert(_mapper.Map<ChatroomParticipation>(participation));
            }

            await _uof.CommitAsync();

            var participationIds = chatroomParticipationDtos.Select(e => e.Id).ToList();
            return _mapper.Map<List<ChatroomParticipationDto>>(
                await _participationRepository.GetAsync(e => participationIds.Contains(e.Id), 0, int.MaxValue));
        }

        public async Task<IEnumerable<ChatroomDto>> GetAsync(Guid participantId)
        {
            var participations = await _participationRepository
                .GetAsync(e => e.ParticipantId == participantId, 0, int.MaxValue);

            var chatroomIds = participations.Select(e => e.ChatroomId).ToList();

            var entities = await _chatroomRepository.GetAsync(e => chatroomIds.Contains(e.Id), 0, int.MaxValue);

            var entitiesParticipation = await _participationRepository.GetAsync(e => chatroomIds.Contains(e.ChatroomId), 0, int.MaxValue);

            var chatroomDtos = new List<ChatroomDto>();
            foreach (var entity in entities)
            {
                var chatroomDto = _mapper.Map<ChatroomDto>(entity);
                chatroomDto.ParticipantIds = entitiesParticipation
                    .Where(e => e.ChatroomId == entity.Id)
                    .Select(cp => cp.ParticipantId).ToList();

                chatroomDtos.Add(chatroomDto);
            }

            return chatroomDtos;
        }

        public async Task<IEnumerable<ChatroomDto>> UpsertAsync(IEnumerable<ChatroomDto> chatroomDtos)
        {
            var chatroomIds = chatroomDtos.Select(e => e.Id).ToList();
            var participations = await _participationRepository.GetAsync(e => chatroomIds.Contains(e.ChatroomId), 0, int.MaxValue);

            foreach (var chatroomDto in chatroomDtos)
            {
                var chatroom = _mapper.Map<Chatroom>(chatroomDto);

                _chatroomRepository.Upsert(chatroom);

                foreach (var participantId in chatroomDto.ParticipantIds)
                {
                    if (!participations.Any(e => e.ParticipantId == participantId 
                        && e.ChatroomId == chatroomDto.Id))
                    {
                        _participationRepository.Upsert(
                            new ChatroomParticipation
                            {
                                CreatedAt = DateTimeOffset.UtcNow,
                                Id = Guid.NewGuid(),
                                ChatroomId = chatroomDto.Id,
                                ParticipantId = participantId
                            });
                    }
                }
            }

            await _uof.CommitAsync();

            var chatroomEntityDtos = new List<ChatroomDto>();
            foreach (var dto in chatroomDtos)
            {
                var chatroomDto = _mapper.Map<ChatroomDto>(dto);
                chatroomDto.ParticipantIds = 
                    (await _participationRepository.GetAsync(e => e.ChatroomId == dto.Id, 0, int.MaxValue))
                    .Select(cp => cp.ParticipantId)
                    .ToList();

                chatroomEntityDtos.Add(chatroomDto);
            }

            return chatroomEntityDtos;
        }
    }
}
