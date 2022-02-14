using ContactApi.Models;
using ContactApi.Service;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace ContactApi.Controller
{

    [ApiController]
    [Route("api/contacts")]
    public class ContactController : ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService) =>
            _contactService = contactService;

        [HttpGet]
        public async Task<List<Contact>> GetContacts() =>
            await _contactService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Contact>> GetContactById(string id)
        {
            var contact = await _contactService.GetAsync(id);

            if (contact is null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(Contact newContact)
        {
            await _contactService.CreateAsync(newContact);

            return CreatedAtAction(nameof(GetContactById), new { id = newContact.Id }, newContact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterContact(string id, Contact alterContact)
        {
            var contact = await _contactService.GetAsync(id);

            if (contact is null)
            {
                return NotFound();
            }

            alterContact.Id = contact.Id;

            await _contactService.UpdateAsync(id, alterContact);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            var contact = await _contactService.GetAsync(id);

            if (contact is null)
            {
                return NotFound();
            }

            await _contactService.DeleteAsync(contact.Id);

            return NoContent();
        }
    }
}

