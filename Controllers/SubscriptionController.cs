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

    
}