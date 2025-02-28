using System;
using Microsoft.AspNetCore.Mvc;
using SubMan.Services;
using SubMan.Models;

namespace SubMan.Controllers;

[Controller]
[Route("api/[controller]")]
public class SubscriptionController: Controller {

    private readonly MongoDBService _mongoDBService;

    public SubscriptionController(MongoDBService mongoDBService) {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Subscription>> Get() {}

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Subscription subscription) {}

    [HttpPut("{id}")]
    public async  Task<IActionResult> Update(string id, [FromBody] Subscription subscription) {}  

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {}
}