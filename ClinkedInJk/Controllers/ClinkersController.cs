﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ClinkedIn.DataAccess;
using ClinkedIn.Models;

namespace ClinkedIn.Controllers
{
    [Route("api/clinkers")]
    [ApiController]
    public class ClinkersController : ControllerBase
    {
        ClinkerRepository _repository = new ClinkerRepository();

        [HttpPost]
        public IActionResult AddClinker(Clinker clinkerToAdd)
        {
            _repository.AddClinker(clinkerToAdd);
            return Created($"Added Clinker {clinkerToAdd.Name}", clinkerToAdd);
        }

        [HttpPost("{id}/services")]
        public IActionResult AddService(int id, Service serviceToAdd)
        {
            var clinkerToUpdate = _repository.GetById(id);
            if (!clinkerToUpdate.Services.Any(s => s.Title == serviceToAdd.Title))
            {
                _repository.AddClinkerService(id, serviceToAdd);
            }
            else
            {
                return BadRequest("Service already exists");
            }

            var updatedClinker = _repository.GetById(id);
            return Ok(updatedClinker);
        }

        [HttpGet("interests/{interest}")]
        public IActionResult GetClinkersByInterest(string interest)
        {
            var interestedClinkers = _repository.GetClinkersByInterest(interest);
            return Ok(interestedClinkers);
        }

        [HttpPost("{id}/friends")]
        public IActionResult AddFriend(int id, Clinker friendToAdd)
        {
            var clinkerToUpdate = _repository.GetById(id);
            if (!clinkerToUpdate.Friends.Any(c => c.Name == friendToAdd.Name))
            {
                _repository.AddClinkerFriend(id, friendToAdd);
            }
            else
            {
                return BadRequest($"Already friends with {friendToAdd.Name}");
            }

            var updatedClinker = _repository.GetById(id);
            return Ok(updatedClinker);
        }

        [HttpPost("{id}/enemies")]
        public IActionResult AddEnemies(int id, Clinker enemyToAdd)
        {
            var clinkerToUpdate = _repository.GetById(id);
            if (!clinkerToUpdate.Enemies.Any(c => c.Name == enemyToAdd.Name))
            {
                _repository.AddClinkerEnemy(id, enemyToAdd);
            }
            else
            {
                return BadRequest($"Already enemies with {enemyToAdd.Name}.");
            }

            var updatedClinker = _repository.GetById(id);
            return Ok(updatedClinker);
        }

        [HttpGet("{id}/sentence")]
        public IActionResult ClinkerSentence(int id)
        {
            var clinkerSentence = _repository.SentenceCountdown(id);
            return Ok(clinkerSentence);
        }
    }
}