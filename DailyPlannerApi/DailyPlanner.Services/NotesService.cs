﻿using AutoMapper;
using DailyPlanner.Dal;
using DailyPlanner.Dto.Notes;
using DailyPlanner.Entities;
using DailyPlanner.Entities.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyPlanner.Services
{
    public class NotesService : INotesService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<Note> _notesRepository;
        private readonly IUnitOfWork _uof;

        public NotesService(
            IRepository<Note> notesRepository,
            IRepository<Tag> tagRepository,
            IUnitOfWork uof,
            IMapper mapper)
        {
            _mapper = mapper;
            _tagRepository = tagRepository;
            _notesRepository = notesRepository;
            _uof = uof;
        }

        public async Task<IEnumerable<NoteDto>> GetAsync(Guid creatorId)
        {
            var notes = await _notesRepository.GetAsync(n => n.CreatorId == creatorId, 0, int.MaxValue);
            var tags = await _tagRepository.GetAsync();

            return notes.Select(
                e => 
                {
                    var noteDto = _mapper.Map<NoteDto>(e);
                    noteDto.Tag = tags.First(t => e.TagId == t.Id).Description;

                    return noteDto;
                })
                .ToList();
        }

        public async Task RemoveAsync(IEnumerable<Guid> noteIds)
        {
            foreach (var noteId in noteIds)
            {
                await _notesRepository.RemoveAsync(noteId);
            }

            await _uof.CommitAsync();
        }

        public async Task<IEnumerable<NoteDto>> UpsertAsync(IEnumerable<NoteDto> noteDtos)
        {
            var newlyCreatedNotes = new List<Note>();

            var tags = await _tagRepository.GetAsync();

            foreach (var noteDto in noteDtos)
            {
                var note = _mapper.Map<Note>(noteDto);
                note.TagId = tags.First(t => t.Description == noteDto.Tag).Id;
                
                newlyCreatedNotes.Add(_notesRepository.Upsert(note));
            }

            await _uof.CommitAsync();

            var dtos = new List<NoteDto>();
            foreach (var createdNote in newlyCreatedNotes)
            {
                dtos.Add(
                    _mapper.Map<NoteDto>(
                        createdNote,
                        opt => opt.AfterMap(
                            (src, dest) =>
                            {
                                var d = dest as NoteDto;
                                d.Tag = tags.First(e => e.Id == (src as Note).TagId).Description;
                            }
                        ))
                    );
            }

            return dtos;
        }
    }
}
